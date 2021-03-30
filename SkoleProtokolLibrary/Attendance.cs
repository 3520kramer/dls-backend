using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary
{
    public class Attendance
    {
        #region Properties

        public DateTime Date { get; set; }

        public string Class { get; set; }

        public bool Attended { get; set; }


        #endregion
    }
}
