using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.Interfaces;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// Contains information required to connect to a mongoDB.
    /// Gets injected with the information from the RollCallDatabaseSettings section in the appsettings.json file.
    /// </summary>
    public class RollCallDatabaseSettings : IRollCallDatabaseSettings
    {
        #region Properties
        /// <summary>
        /// Name of the mongoDB collection.
        /// </summary>
        public string CollectionName { get; set; }
        /// <summary>
        /// Connectionstring for the mongoDB.
        /// </summary>
        public string ConnectionString { get; set; }
        /// <summary>
        /// Name of the database on mongoDB.
        /// </summary>
        public string DatabaseName { get; set; }

        #endregion


    }
}
