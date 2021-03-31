using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.Interfaces;

namespace SkoleProtokolLibrary.Models
{
    public class RollCallDatabaseSettings : IRollCallDatabaseSettings
    {
        #region Properties

        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }

        #endregion


    }
}
