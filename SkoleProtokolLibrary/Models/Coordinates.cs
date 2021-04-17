using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DTO;

namespace SkoleProtokolLibrary.Models
{
    /// <summary>
    /// Contains information about the coordinates the code is usable for.
    /// </summary>
    public class Coordinates
    {

        #region Properties

        /// <summary>
        /// Latitude of the location authorized for roll call
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of the location authorized for roll call
        /// </summary>
        public double Longitude { get; set; }

        #endregion

        #region Constructor

        public Coordinates(CoordinatesDTO coordinates)
        {
            Latitude = coordinates.Latitude;
            Longitude = coordinates.Longitude;
        }


        #endregion

    }
}
