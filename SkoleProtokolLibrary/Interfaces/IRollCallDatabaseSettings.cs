using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.Interfaces
{
    public interface IRollCallDatabaseSettings
    {

        #region Properties

        string CollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }


        #endregion

    }
}
