using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.DBModels;
using SkoleProtokolLibrary.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleProtokolAPI.Controllers
{
    /// <summary>
    /// Api controller for RollCall
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RollCallController : ControllerBase
    {
        #region InstanceFields

        private readonly RollCallUsersService _usersService;
        private readonly RollCallModulesService _modulesService;

        #endregion

        #region Constructor

        public RollCallController(RollCallUsersService usersService, RollCallModulesService modulesService)
        {
            _usersService = usersService;
            _modulesService = modulesService;
        }

        #endregion



        

        /// <summary>
        /// Gets the subjects and the classes for the first of the subjects connected to the teacher.
        /// Also gets the modules/time intervals that a teacher can select to take roll call for.
        /// </summary>
        /// <param name="teacherId">Id of the teacher starting a roll call</param>
        /// <returns>StartRollCallDTO</returns>
        // GET api/<RollCallController>/5
        [HttpGet]
        [Route("InitialInfo")]
        public StartRollCallDTO GetInitialRollCallInfo([FromQuery] string teacherId)
        {
            (List<string> subjects, List<string> classes)
                rollCallTuple = _usersService.GetSubjectsAndClasses(teacherId);

            List<ModuleDTO> modules = new List<ModuleDTO>();

            _modulesService.GetModules().ForEach(module => modules.Add(new ModuleDTO(module)));

            //foreach (DBModule module in _modulesService.GetModules())
            //{
            //    modules.Add(new ModuleDTO(module));
            //}

            return new StartRollCallDTO(rollCallTuple.subjects, rollCallTuple.classes, modules);
        }

        /// <summary>
        /// This endpoint is called when the selected subject is changed.
        /// It responds with the classes associated with a specific teacher and subject.
        /// </summary>
        /// <remarks>It should only be called after the initialinfo endpoint has been called</remarks>
        /// <param name="teacherId">Id of the teacher</param>
        /// <param name="subject">Name of the subject</param>
        /// <returns>List of strings</returns>
        //GET: api/<RollCallController>
        [HttpGet]
        [Route("Classes")]
        public IEnumerable<string> GetClasses([FromQuery] string teacherId, [FromQuery] string subject)
        {
            return _usersService.GetSpecificClasses(teacherId, subject);
        }

        // POST api/<RollCallController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RollCallController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RollCallController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
