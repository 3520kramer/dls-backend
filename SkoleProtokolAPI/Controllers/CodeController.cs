using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.Generator;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleProtokolAPI.Controllers
{
    [Route("api/Code")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        // GET: api/<CodeController>
        [HttpGet]
        public string Get()
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
