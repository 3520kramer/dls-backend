using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// Entity model of a class for communication with mongoDB
    /// </summary>
    public class DBSchoolClass
    {

        #region Properties

        /// <summary>
        /// Id in the database
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// Name of the class
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Name of the course
        /// </summary>
        [BsonElement("course")]
        public string Course { get; set; }

        /// <summary>
        /// Teachers assigned to the class
        /// </summary>
        [BsonElement("teachers")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> Teachers { get; set; }

        #endregion


    }
}
