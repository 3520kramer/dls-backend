using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.ActiveTimer;

namespace SkoleProtokolAPI.Comparers
{
    public static class AttendanceCodeComparer
    {


        #region Methods

        public static bool IsCodeValid(ConcurrentQueue<ActiveAttendanceCode> activeAttendanceCodes, string codeToCompare)
        {
            bool codeIsValid = false;

            if (activeAttendanceCodes.Count > 0)
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
            else
            {
                codeIsValid = true;
            }

            

            return codeIsValid;
        }

        #endregion

    }
}
