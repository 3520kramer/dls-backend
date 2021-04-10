using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.Interfaces
{
    /// <summary>
    /// Defines required fields for a RollCallDatabaseSettings object.
    /// </summary>
    public interface IRollCallDatabaseSettings
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
        string ConnectionString { get; set; }
        /// <summary>
        /// Name of the database on mongoDB.
        /// </summary>
        string DatabaseName { get; set; }


        #endregion

    }
}
