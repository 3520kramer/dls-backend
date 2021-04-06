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
        /// if no match or the queue doesn't contain any codes then it return false.
        /// </summary>
        /// <param name="activeAttendanceCodes">A ConcurrentQueue that contains activeAttendanceCodes</param>
        /// <param name="codeToCompare">The attendanceCode that a match will be searched for</param>
        /// <returns>bool</returns>
        public static bool CheckCodeValidity(ConcurrentQueue<ActiveAttendanceCode> activeAttendanceCodes, string codeToCompare)
        {
            return CodeExists(activeAttendanceCodes, codeToCompare);
        }

        /// <summary>
        /// Checks the uniqueness of an attendanceCode
        /// If a match is found the method returns false,
        /// if no match or the queue doesn't contain any codes then it return true.
        /// </summary>
        /// <param name="activeAttendanceCodes">A ConcurrentQueue that contains activeAttendanceCodes</param>
        /// <param name="codeToCheck">The attendanceCode that will have its uniqueness tested</param>
        /// <returns>bool</returns>
        public static bool CheckCodeUniqueness(ConcurrentQueue<ActiveAttendanceCode> activeAttendanceCodes, string codeToCheck)
        {
            return !CodeExists(activeAttendanceCodes, codeToCheck);
        }

        #endregion


        #region HelpMethod
        /// <summary>
        /// Checks all attendanceCode entries in a ConcurrentQueue for a match.
        /// If a match is found the method returns true,
        /// if no match or the queue doesn't contain any codes then it return false
        /// </summary>
        /// <param name="activeAttendanceCodes">A ConcurrentQueue that contains activeAttendanceCodes</param>
        /// <param name="codeToCompare">The attendanceCode that a match will be searched for</param>
        /// <returns>bool</returns>
        private static bool CodeExists(ConcurrentQueue<ActiveAttendanceCode> activeAttendanceCodes, string codeToCompare)
        {
            bool codeExists = false;

            if (activeAttendanceCodes.Count > 0)//False is automatically returned if there are no entries
            {
                foreach (ActiveAttendanceCode code in activeAttendanceCodes)
                {
                    if (String.Equals(code.AttendanceCode, codeToCompare))
                    {
                        codeExists = true;
                        break;
                    }
                }
            }

            return codeExists;
        }

        #endregion

    }
}
