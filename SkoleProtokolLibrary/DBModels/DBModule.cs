using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// A module that is a part of a lesson.
    /// </summary>
    public class DBModule
    {


        #region Properities

        /// <summary>
        /// The id of a module
        /// </summary>
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        /// <summary>
        /// The time interval for a module
        /// </summary>
        [BsonElement("timespan")]
        public DBTimespan Timespan { get; set; }

        #endregion


    }
}
