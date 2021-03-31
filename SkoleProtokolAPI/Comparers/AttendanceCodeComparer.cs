using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.ActiveTimer;

namespace SkoleProtokolAPI.Comparers
{
    /// <summary>
    /// A static class meant to contain all methods for comparing attendanceCodes
    /// </summary>
    public static class AttendanceCodeComparer
    {


        #region Methods
        /// <summary>
        /// Checks all attendanceCode entries in a ConcurrentQueue for a match.
        /// It is assumed that all attendanceCodes are unique.
        /// If a match is found the method returns true,
        /// if no match or the queue doesn't contain any codes then it return false
        /// </summary>
        /// <param name="activeAttendanceCodes">A ConcurrentQueue that contains activeAttendanceCodes</param>
        /// <param name="codeToCompare">The attendanceCode that a match will be searched for</param>
        /// <returns>bool</returns>
        public static bool CheckCodeValidity(ConcurrentQueue<ActiveAttendanceCode> activeAttendanceCodes, string codeToCompare)
        {
            bool codeIsValid = false;

            if (activeAttendanceCodes.Count > 0)//False is automatically returned if there are no entries
            {
                foreach (ActiveAttendanceCode code in activeAttendanceCodes)
                {
                    if (String.Equals(code.AttendanceCode, codeToCompare))
                    {
                        codeIsValid = true;
                        break;
                    }
                }
            }
            
            return codeIsValid;
        }

        #endregion

    }
}
