using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SkoleProtokolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {

        #region InstanceFields

        private readonly RollCallUsersService _usersService;

        #endregion

        #region Constructor

        public StatisticsController(RollCallUsersService usersService)
        {
            _usersService = usersService;
        }

        #endregion

        // GET: api/<StatisticsController>?subject=Development%20of%20Large%20Systems&subject=Test&class=w2&class=w1
        [HttpGet]
        [Authorize]
        public IEnumerable<UserDTO> GetStudentStatistics([FromQuery(Name = "Subject")] List<string> subjects, [FromQuery(Name = "Class")] List<string> classes)
        {
            List<UserDTO> students = new List<UserDTO>();
            

            _usersService.GetStudentStatisticData(ConvertListOfStringsToLowercase(subjects), ConvertListOfStringsToLowercase(classes)).ForEach(student => students.Add(new UserDTO(student)));

            return students;
        }

        #region HelpMethods

        /// <summary>
        /// Converts a list of strings to lowercase and returns the list
        /// </summary>
        /// <param name="strings">The list of strings</param>
        /// <returns>List of strings</returns>
        private List<string> ConvertListOfStringsToLowercase(List<string> strings)
        {
            for (int index = 0; index < strings.Count; index++)
            {
                strings[index] = strings[index].ToLower();
            }

            return strings;
        }

        #endregion


    }
}
