using System;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.Models
{
    public class Attendance
    {
        #region Properties

        [BsonElement("date")]
        public DateTime Date { get; set; }

        [BsonElement("class")]
        public string Class { get; set; }

        [BsonElement("attended")]
        public bool Attended { get; set; }


        #endregion
    }
}
