using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    public class AttendanceDTO
    {

        #region Properties
        /// <summary>
        /// 
        /// Date of the lesson.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// The class, attendance is related to.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Is true if the student attended class, false if they did not.
        /// </summary>
        public bool Attended { get; set; }

        #endregion


        #region Constructor

        public AttendanceDTO(DBAttendance attendance)
        {
            Date = attendance.Date;
            Subject = attendance.Subject;
            Attended = attendance.Attended;
        }

        #endregion

    }
}
