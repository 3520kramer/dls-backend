using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Contains necessary information for registering a student's attendance
    /// </summary>
    public class RegisterAttendanceDTO
    {

        #region Properties

        /// <summary>
        /// Id of the student
        /// </summary>
        public string Student_Id { get; set; }

        /// <summary>
        /// The AttendanceCode used to register attendance
        /// </summary>
        public string AttendanceCode { get; set; }

        /// <summary>
        /// The student's coordinates
        /// </summary>
        public CoordinatesDTO Coordinates { get; set; }


        #endregion



    }
}
