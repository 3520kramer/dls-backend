using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.DTO;
using SkoleProtokolLibrary.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleProtokolAPI.Controllers
{
    /// <summary>
    /// The API controller that handles Course information
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {

        #region InstanceFields

        private RollCallClassesService _classesService;

        #endregion

        #region Constructor

        public CoursesController(RollCallClassesService rollCallClassesService)
        {
            _classesService = rollCallClassesService;
        }

        #endregion


        // GET: api/<CoursesController>?teacherid=value
        /// <summary>
        /// An endpoint for retrieving courses related to a specific teacher
        /// </summary>
        /// <param name="teacherId">Id of the teacher</param>
        /// <returns>A collection of CourseDTOs</returns>
        [HttpGet]
        public IEnumerable<CourseDTO> Get([FromQuery] string teacherId)
        {
            return _classesService.GetCoursesByTeacherId(teacherId);
        }

        // GET api/<CoursesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CoursesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CoursesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CoursesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
