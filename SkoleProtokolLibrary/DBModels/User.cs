using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// Entity model of a user for communication with mongoDB
    /// </summary>
    public class User
    {

        #region Properties

        /// <summary>
        /// Id in the database
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// First name of the user
        /// </summary>
        [BsonElement("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user
        /// </summary>
        [BsonElement("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Name of the course the user is mainly associated with(Might be deleted).
        /// </summary>
        [BsonElement("course")]
        public string Course { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        /// <summary>
        /// A list of subjects and classes related to the subjects
        /// </summary>
        [BsonElement("subjects")]
        public List<DBSubject> Subjects { get; set; }

        /// <summary>
        /// A list of lessons and whether they were attended.
        /// </summary>
        [BsonElement("attendance")]
        public List<Attendance> AttendanceLog { get; set; }

        /// <summary>
        /// Role of the user.
        /// </summary>
        [BsonElement("role")]
        public string Role { get; set; }

        #endregion


    }
}
