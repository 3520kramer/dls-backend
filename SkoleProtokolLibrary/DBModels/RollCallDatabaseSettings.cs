using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.Interfaces;

namespace SkoleProtokolLibrary.DBModels
{
    /// <summary>
    /// Contains information required to connect to a mongoDB.
    /// Gets injected with the information from the RollCallDatabaseSettings section in the appsettings.json file.
    /// </summary>
    public class RollCallDatabaseSettings : IRollCallDatabaseSettings
    {
        #region Properties
        /// <summary>
        /// Name of the mongoDB UsersCollection.
        /// </summary>
        public string UsersCollection { get; set; }
        /// <summary>
        /// Name of the mongoDB classesCollection
        /// </summary>
        public string ClassesCollection { get; set; }
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
