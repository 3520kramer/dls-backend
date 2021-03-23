using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkoleProtokolAPI.Generator
{
    public static class AttendanceCodeGenerator
    {






        #region Methods
        //Generates a six character code, the code will contain capital letters and numbers.
        public static string GenerateAttendanceCode()
        {
            //int id = Guid.NewGuid().ToString().GetHashCode();

            string attendanceCode = Guid.NewGuid().ToString("N").Substring(0, 6); //Should we avoid duplications? or can we fix it with timestamps?

            return attendanceCode;
        }

        #endregion
    }
}
