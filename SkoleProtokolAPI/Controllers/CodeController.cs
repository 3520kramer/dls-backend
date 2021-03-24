using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.Generator;

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
        // GET: api/<CodeController>
        /// <summary>
        /// Starts the process of code generation and returns the generated code in a HTTP-response 
        /// </summary>
        /// <returns>The newly generated attendanceCode as a string</returns>
        [HttpGet]
        public string GenerateAttendanceCode()
        {
            string attendanceCode = AttendanceCodeGenerator.GenerateAttendanceCode();
            return attendanceCode;
        }

        // GET api/<CodeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CodeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CodeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CodeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
