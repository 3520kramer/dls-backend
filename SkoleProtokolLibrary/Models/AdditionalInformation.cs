using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// Additional information associated with an ActiveAttendanceCode, such as coordinates and
    /// a limit on how many students that can use the code.
    /// </summary>
    public class AdditionalInformation
    {

        #region Properties

        /// <summary>
        /// Latitude of the location authorized for roll call
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the location authorized for roll call
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Number of students the code will be active for
        /// </summary>
        public int NumberOfStudents { get; set; }

        #endregion

    }
}
