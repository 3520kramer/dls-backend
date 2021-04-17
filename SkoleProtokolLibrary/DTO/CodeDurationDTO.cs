using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Contains information about the lifetime of the code
    /// </summary>
    /// <remarks>Always a part of the RequestAttendanceCodeDTO</remarks>
    public class CodeDurationDTO
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


    }
}
