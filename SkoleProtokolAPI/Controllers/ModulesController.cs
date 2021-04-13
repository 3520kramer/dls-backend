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
    public class ModulesController : ControllerBase
    {

        #region InstanceFields

        private readonly RollCallModulesService _modulesService;

        #endregion


        #region Constructor

        public ModulesController(RollCallModulesService modulesService)
        {
            _modulesService = modulesService;
        }

        #endregion


        // GET: api/<ModulesController>
        [HttpGet]
        public IEnumerable<ModuleDTO> Get()
        {
            List<ModuleDTO> modules = new List<ModuleDTO>();

            foreach (DBModule dbModule in _modulesService.GetModules())
            {
                modules.Add(new ModuleDTO(dbModule));
            }

            return modules;
        }

        // GET api/<ModulesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ModulesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ModulesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModulesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
