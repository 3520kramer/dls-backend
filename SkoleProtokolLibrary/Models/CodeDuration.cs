using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DTO;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// Contains information about the lifetime of the code
    /// </summary>
    public class CodeDuration
    {

        #region Properties

        /// <summary>
        /// Duration of the generated code's validity
        /// </summary>
        public int Minutes { get; set; }

        /// <summary>
        /// The time the code was requested
        /// </summary>
        public DateTime Timestamp { get; set; }

        #endregion

        #region Constructor

        public CodeDuration(CodeDurationDTO codeDuration)
        {
            Minutes = codeDuration.Minutes;
            Timestamp = new DateTime(codeDuration.Timestamp.Year, codeDuration.Timestamp.Month, codeDuration.Timestamp.Day, codeDuration.Timestamp.Hour, codeDuration.Timestamp.Minute, codeDuration.Timestamp.Second, codeDuration.Timestamp.Millisecond, DateTimeKind.Utc);
        }

        #endregion


    }
}
