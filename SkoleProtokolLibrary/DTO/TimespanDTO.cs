using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    public class TimespanDTO
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

        public TimespanDTO(DBTimespan timespan)
        {
            Start = timespan.Start;
            End = timespan.End;
        }

        #endregion

    }
}
