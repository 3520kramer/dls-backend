using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolAPI.ActiveTimer;
using SkoleProtokolAPI.Comparers;
using SkoleProtokolAPI.Generator;
using SkoleProtokolLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleProtokolAPI.Controllers
{
    /// <summary>
    /// The api controller that handles Attendance codes
    /// </summary>
    [Route("api/AttendanceCode")]
    [ApiController]
    public class CodeController : ControllerBase
    {

        #region InstanceFields

        private static readonly ConcurrentQueue<ActiveAttendanceCode> _activeAttendanceCodes = new ConcurrentQueue<ActiveAttendanceCode>();
        private readonly IMongoCollection<User> _userCollection;
        #endregion


        // GET: api/<CodeController>
        /// <summary>
        /// Starts the process of generating a unique code, stores the generated code in memory for 10 min.
        /// Returns the generated code in a HTTP-response 
        /// </summary>
        /// <returns>The newly generated attendanceCode as a string</returns>
        [HttpGet]
        public string GenerateAttendanceCode()
        {
            bool isCodeUnique = false;
            string attendanceCode = "";
            while (!isCodeUnique)//Ensures the uniqueness of a generated code
            {
                attendanceCode = AttendanceCodeGenerator.GenerateAttendanceCode();//Generates the code
                if (AttendanceCodeComparer.CheckCodeUniqueness(_activeAttendanceCodes, attendanceCode))
                {
                    isCodeUnique = true;
                }
            }
            //Stores the code in memory
            ActiveAttendanceCode activateCode = new ActiveAttendanceCode(_activeAttendanceCodes, attendanceCode);
            return attendanceCode;
        }

        //// GET api/<CodeController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<CodeController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CodeController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CodeController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
