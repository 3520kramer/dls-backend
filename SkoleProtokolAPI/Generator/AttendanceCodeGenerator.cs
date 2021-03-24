using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoleProtokolAPI.Generator
{
    /// <summary>
    /// The purpose of this class is to generate attendance codes
    /// </summary>
    public static class AttendanceCodeGenerator
    {

        #region Methods

        /// <summary>
        /// Generates a six character code, the code will contain capital letters and numbers.
        /// </summary>
        /// <example>2f3a1c</example>
        /// <returns>A string containing the generated code</returns>
        public static string GenerateAttendanceCode()
        {
            //The code is generated partly by the NewGuid method which gets turned into a string and remove the "-"
            //The string gets shortened down to 6 characters.
            string attendanceCode = Guid.NewGuid().ToString("N").Substring(0, 6);

            return attendanceCode;
        }

        #endregion
    }
}
