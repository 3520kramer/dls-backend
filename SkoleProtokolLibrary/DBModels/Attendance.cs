using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// Entity model for an Attendance object. Holds information regarding a students attendance for a lesson.
    /// </summary>
    public class Attendance
    {
        #region Properties
        /// <summary>
        /// 
        /// Date of the lesson.
        /// </summary>
        [BsonElement("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The class, attendance is related to.
        /// </summary>
        [BsonElement("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Is true if the student attended class, false if they did not.
        /// </summary>
        [BsonElement("attended")]
        public bool Attended { get; set; }


        #endregion
    }
}
