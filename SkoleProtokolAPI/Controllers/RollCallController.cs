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



        // GET: api/<RollCallController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<RollCallController>/5
        [HttpGet]
        public StartRollCallDTO Get([FromQuery] string teacherId)
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
