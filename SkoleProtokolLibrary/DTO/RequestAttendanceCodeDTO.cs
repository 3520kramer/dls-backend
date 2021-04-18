using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Contains all the information needed to request an attendanceCode
    /// </summary>
    public class RequestAttendanceCodeDTO
    {

        #region Properties

        public string TeacherId { get; set; }

        /// <summary>
        /// The subject the code belongs to.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// The classes that can use the code.
        /// </summary>
        public List<string> Classes { get; set; }

        /// <summary>
        /// List of modules attendance will be taken for.
        /// </summary>
        public List<ModuleDTO> Modules { get; set; }


        public CoordinatesDTO Coordinates { get; set; }

        /// <summary>
        /// The max number of students the code can be used by.
        /// </summary>
        public int NumberOfStudents { get; set; }


        public CodeDurationDTO Duration { get; set; }

        #endregion







    }
}
