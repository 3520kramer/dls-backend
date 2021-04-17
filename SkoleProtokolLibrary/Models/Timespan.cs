using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DTO;

namespace SkoleProtokolLibrary.Models
{
    public class Timespan
    {

        #region Properties

        /// <summary>
        /// Start time for a module
        /// </summary>
        public string Start { get; set; }

        /// <summary>
        /// End time for a module
        /// </summary>
        public string End { get; set; }

        #endregion

        #region Constructor

        public Timespan(TimespanDTO timespan)
        {
            Start = timespan.Start;
            End = timespan.End;
        }

        #endregion


    }
}
