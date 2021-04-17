using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DTO;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// Contains data for a single module of a lesson
    /// </summary>
    public class Module
    {
        #region Properities

        /// <summary>
        /// The id of a module
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The time interval for a module
        /// </summary>
        public Timespan Timespan { get; set; }

        #endregion

        #region Constructor

        public Module(ModuleDTO module)
        {
            Id = module.Id;
            Timespan = new Timespan(module.Timespan);
        }

        #endregion

    }
}
