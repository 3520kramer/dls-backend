using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    public class ModuleDTO
    {

        #region Properities

        /// <summary>
        /// The id of a module
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The time interval for a module
        /// </summary>
        public TimespanDTO Timespan { get; set; }

        #endregion

        #region Constructor


        public ModuleDTO(DBModule module)
        {
            Id = module.Id;
            Timespan = new TimespanDTO(module.Timespan);
        }

        #endregion

    }
}
