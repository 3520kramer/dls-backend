using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Holds data for one User
    /// </summary>
    public class UserDTO
    {

        #region Properties

        /// <summary>
        /// Id in the database
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Name of the course the user is mainly associated with(Might be deleted).
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// A list of subjects and classes related to the subjects
        /// </summary>
        public List<SubjectDTO> Subjects { get; set; }

        /// <summary>
        /// A list of lessons and whether they were attended.
        /// </summary>
        public List<AttendanceDTO> AttendanceLog { get; set; }

        /// <summary>
        /// Role of the user.
        /// </summary>
        public string Role { get; set; }

        #endregion

        #region Constructor

        public UserDTO(DBUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Course = user.Course;
            Email = user.Email;
            Subjects = new List<SubjectDTO>();
            foreach (DBSubject subject in user.Subjects)
            {
                Subjects.Add(new SubjectDTO(subject));
            }

            AttendanceLog = new List<AttendanceDTO>();
            foreach (DBAttendance attendance in user.AttendanceLog)
            {
                AttendanceLog.Add(new AttendanceDTO(attendance));
            }

            Role = user.Role;
        }

        #endregion

    }
}
