using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// A time interval for a module
    /// </summary>
    public class DBTimespan
    {

        #region Properties

        /// <summary>
        /// Start time for a module
        /// </summary>
        [BsonElement("start")]
        public string Start { get; set; }

        /// <summary>
        /// End time for a module
        /// </summary>
        [BsonElement("end")]
        public string End { get; set; }

        #endregion

    }
}
