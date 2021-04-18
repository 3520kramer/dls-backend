using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// Coordinate option for attendance.
    /// Contains the teachers coordinates
    /// </summary>
    /// <remarks>Coordinates should be the coordinates of the school</remarks>
    public class CoordinatesDTO
    {

        #region Properties


        public double Latitude { get; set; }


        public double Longitude { get; set; }



        #endregion
    }
}
