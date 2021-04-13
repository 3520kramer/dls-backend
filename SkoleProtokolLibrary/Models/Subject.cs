using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// The subject connected to an attendanceCode
    /// </summary>
    public class Subject
    {

        #region Properties

        /// <summary>
        /// The id of the subject
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The title of the subject
        /// </summary>
        public string Title { get; set; }

        #endregion

    }
}
