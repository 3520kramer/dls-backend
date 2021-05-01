using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolAPI.ActiveTimer;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolAPI.Services
{
    /// <summary>
    /// Interacts with the roll-call mongoDB users collection
    /// </summary>
    public class RollCallUsersService
    {

        #region InstanceFields

        private readonly IMongoCollection<DBUser> _users;//Collection of users from the mongoDB.

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a instance on RollCallUsersService and
        /// retrieves the roll-call database from mongoDB and
        /// extracts the user collection based on the IRollCallDatabaseSettings object.
        /// </summary>
        /// <param name="settings">Settings for connecting to a mongoDB, is injected automatically</param>
        public RollCallUsersService(IRollCallDatabaseSettings settings)
        {
            var client = new MongoClient(Environment.GetEnvironmentVariable(settings.ConnectionString));
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<DBUser>(settings.UsersCollection);
        }

        #endregion

        #region Methods

        //public List<User> GetAll() =>
        //    _users.Find(user => true).ToList();

        /// <summary>
        /// Gets a list of subjects for a teacher and
        /// a list of the classes that the teacher has in the first subject of the subject list
        /// </summary>
        /// <param name="teacherId">The id of the teacher to find subjects and classes for</param>
        /// <returns>a tuple containing two lists of string</returns>
        public (List<string> subjects, List<string> classes) GetSubjectsAndClasses(string teacherId)
        {
            List<string> subjects = new List<string>();
            List<string> classes = new List<string>();

            DBUser teacher = FindUser(teacherId);

            for (int index = 0; index < teacher?.Subjects.Count; index++)
            {
                if (index == 0)
                {
                    classes = teacher.Subjects[index].Classes;
                }
                subjects.Add(teacher.Subjects[index].Name);
            }

            return (subjects, classes);
        }

        /// <summary>
        /// Gets a teacher's classes for a specific subject
        /// </summary>
        /// <param name="teacherId">The id of the teacher</param>
        /// <param name="subject">Name of the subject</param>
        /// <returns>List of strings</returns>
        public List<string> GetSpecificClasses(string teacherId, string subject)
        {
            List<string> classes = new List<string>();

            DBUser teacher = FindUser(teacherId);

            if (teacher?.Subjects != null)
                foreach (var dbSubject in teacher.Subjects)
                {
                    if (string.Equals(dbSubject.Name.ToLower(), subject.ToLower()))
                    {
                        classes = dbSubject.Classes;
                        break;
                    }
                }

            return classes;
        }

        public async Task PrepareStudentsForAttendance(ActiveAttendanceCode activeAttendanceCode)
        {
            List<DBUser> users = GetAllUsers();

            List<DBUser> students = new List<DBUser>();

            foreach (var user in users)
            {
                if (user.Role.ToLower() != "student")
                {
                    continue;
                }

                foreach (var subject in user.Subjects)
                {
                    if (!string.Equals(subject.Name.ToLower(), activeAttendanceCode.Subject.ToLower()))
                    {
                        continue;
                    }

                    if (activeAttendanceCode.Classes.Contains(subject.Classes[0].ToLower()))
                    {
                        students.Add(user);
                    }
                }
            }

            foreach (var student in students)
            {
                student.AttendanceLog.Add(new DBAttendance(){Subject = activeAttendanceCode.Subject, Date = activeAttendanceCode.Duration.Timestamp, Attended = false});
                var filter = Builders<DBUser>.Filter.Eq(u => u.Id, student.Id);
                await _users.ReplaceOneAsync(filter, student);
            }
        }


        public async Task<string> RegisterAttendance(string studentId, ActiveAttendanceCode activeAttendanceCode)
        {
            DBUser user = FindUser(studentId);
            //Check the student's class
            bool studentIsAssignedToValidClass = activeAttendanceCode.Classes.Contains(user?.Subjects.Find(s => s.Name == activeAttendanceCode.Subject)?.Classes[0].ToLower());

            if (!studentIsAssignedToValidClass)
            {
                return $"Code is not valid for your class";
            }

            if (user?.AttendanceLog != null)
                foreach (DBAttendance attendance in user.AttendanceLog)
                {
                    if (attendance.Date == activeAttendanceCode.Duration.Timestamp &&
                        attendance.Subject.ToLower() == activeAttendanceCode.Subject.ToLower())
                    {
                        if (attendance.Attended)
                        {
                            return "Attendance has already been registered. Can't use the same code again";
                        }
                        attendance.Attended = true;
                    }
                }

            

            if (activeAttendanceCode.IsNumberOfStudentsEnabled && activeAttendanceCode.NumberOfStudents < 1)
            {
                return "Code has been used up";
            }

            if (activeAttendanceCode.IsNumberOfStudentsEnabled && activeAttendanceCode.NumberOfStudents > 0)
            {
                activeAttendanceCode.NumberOfStudents--;
            }

            var filter = Builders<DBUser>.Filter.Eq(u => u.Id, studentId);
            await _users.ReplaceOneAsync(filter, user);
            return "Attendance registered";
        }

        /// <summary>
        /// Gets a list of students that correspond to the lists of subjects and classes
        /// </summary>
        /// <param name="subjects">List of subjects to search for</param>
        /// <param name="classes">List of classes to search for</param>
        /// <returns>List of DBUsers</returns>
        public List<DBUser> GetStudentStatisticData(List<string> subjects, List<string> classes)
        {
            List<DBUser> students = new List<DBUser>();
            List<DBUser> allUsers = GetAllUsers();
            
            foreach (DBUser user in allUsers)
            {
                int subjectIndex = -1;

                if (user.Role.ToLower() != "student")
                {
                    continue;
                }

                for (int index = 0; index < user.Subjects.Count; index++)
                {
                    if (subjects.Contains(user.Subjects[index].Name.ToLower()))
                    {
                        subjectIndex = index;
                        break;
                    }
                }

                if (subjectIndex == -1)
                {
                    continue;
                }

                foreach (var @class in user.Subjects[subjectIndex].Classes)
                {
                    if (classes.Contains(@class.ToLower()))
                    {
                        students.Add(user);
                        break;
                    }
                }
                
            }

            return students;
        }

        #endregion

        #region HelpMethods

        /// <summary>
        /// Finds and returns one user from the list of users by id.
        /// </summary>
        /// <param name="userId">Id of the user to be found</param>
        /// <returns>User</returns>
        private DBUser FindUser(string userId)
        {
            DBUser user;
            //Throws exception if user doesn't exist
            try
            {
                user = GetAllUsers().First(user => user.Id == userId);
            }
            catch (InvalidOperationException exception)
            {
                user = null;
            }
            return user;
        }

        /// <summary>
        /// Converts the IMongoCollection of Users to a list.
        /// </summary>
        /// <returns>List of Users</returns>
        private List<DBUser> GetAllUsers()
        {
            return _users.Find(user => true).ToList();
        }

        #endregion
    }
}
