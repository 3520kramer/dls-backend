using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolLibrary.DTO;
using SkoleProtokolLibrary.Interfaces;
using SkoleProtokolLibrary.Models;

namespace SkoleProtokolAPI.Services
{
    /// <summary>
    /// Interacts with the roll-call mongoDB classes collection
    /// </summary>
    public class RollCallClassesService
    {

        #region InstanceFields

        private readonly IMongoCollection<SchoolClass> _classes;//Collection of classes from the mongoDB.

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a instance on RollCallClassesService and
        /// retrieves the roll-call database from mongoDB and
        /// extracts the classes collection based on the IRollCallDatabaseSettings object.
        /// </summary>
        /// <param name="settings">Settings for connecting to a mongoDB, is injected automatically</param>
        public RollCallClassesService(IRollCallDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _classes = database.GetCollection<SchoolClass>(settings.ClassesCollection);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Gets course(s) related to a specific teacher
        /// </summary>
        /// <param name="teacherId">The id of the teacher</param>
        /// <returns>A list of CourseDTOs</returns>
        public List<CourseDTO> GetCoursesByTeacherId(string teacherId)
        {
            List<CourseDTO> courses = new List<CourseDTO>();

            // Converts the classes collection to a list of SchoolClasses and finds the SchoolClasses related to a specific teacher
            foreach (var schoolClass in _classes.Find(s => true).ToList().FindAll(sc => sc.Teachers.Contains(teacherId)))
            {
                if (!courses.Exists(c => c.Title == schoolClass.Course))
                {
                    CourseDTO tempCourseDto = new CourseDTO(){Title = schoolClass.Course};
                    courses.Add(tempCourseDto);
                }
            }

            return courses;
        }


        #endregion
    }
}
