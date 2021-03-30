using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary
{
    public class User
    {

        #region Properties

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("classes")]
        public List<string> Classes { get; set; }

        [BsonElement("attendance")]
        public List<Attendance> AttendanceLog { get; set; }

        [BsonElement("role")]
        public string Role { get; set; }

        #endregion


    }
}
