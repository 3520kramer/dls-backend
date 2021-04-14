using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// Entity model for a Subject.
    /// </summary>
    public class DBSubject
    {

        #region Properties

        /// <summary>
        /// Name of the subject
        /// </summary>
        [BsonElement("name")]
        public string Name { get; set; }

        /// <summary>
        /// Classes that have the given subject
        /// </summary>
        [BsonElement("classes")]
        public List<string> Classes { get; set; }

        #endregion



    }
}
