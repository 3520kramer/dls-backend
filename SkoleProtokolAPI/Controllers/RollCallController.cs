using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SkoleProtokolAPI.ActiveTimer;
using SkoleProtokolAPI.Comparers;
using SkoleProtokolAPI.Generator;
using SkoleProtokolAPI.Services;
using SkoleProtokolLibrary.DBModels;
using SkoleProtokolLibrary.DTO;
using SkoleProtokolLibrary.Models;

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

        private static readonly ConcurrentQueue<ActiveAttendanceCode> _activeAttendanceCodes = new ConcurrentQueue<ActiveAttendanceCode>();

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



        // GET: api/<RollCallController>
        /// <summary>
        /// Starts the process of generating a unique code, stores the generated code in memory for a given amount of time.
        /// Also stores additional information related to the generated code.
        /// Returns the generated code in a HTTP-response 
        /// </summary>
        /// <param name="request">The additional information stored with the code</param>
        /// <returns>The newly generated attendanceCode as a string</returns>
        [HttpPost]
        [Route("RequestCode")]
        public string GenerateAttendanceCode([FromBody] RequestAttendanceCodeDTO request)
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
            ActiveAttendanceCode activateCode = new ActiveAttendanceCode(_activeAttendanceCodes, attendanceCode, request);
            return activateCode.AttendanceCode;
        }

        [HttpPost]
        [Route("RegisterAttendance")]
        public string RegisterAttendance([FromBody] RegisterAttendanceDTO registerAttendanceDto)
        {
            ActiveAttendanceCode activecode = null;
            foreach (ActiveAttendanceCode activeAttendanceCode in _activeAttendanceCodes)
            {
                if (string.Equals(activeAttendanceCode.AttendanceCode, registerAttendanceDto.AttendanceCode))
                {
                    activecode = activeAttendanceCode;
                    break;
                }
                else
                {
                    return "Invalid Code";
                }
            }

            if (activecode?.Coordinates != null)
            {
                if (registerAttendanceDto.Coordinates == null)
                {
                    return "Coordinates required";
                }

                if (!CompareCoordinates(activecode.Coordinates, new Coordinates(registerAttendanceDto.Coordinates)))
                {
                    return "Invalid Coordinates";
                }
            }

            string responseMessage = _usersService.RegisterAttendance(registerAttendanceDto.Student_Id, activecode);
            return responseMessage;
        }



        //// POST api/<RollCallController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<RollCallController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<RollCallController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}

        #region HelpMethods

        private bool CompareCoordinates(Coordinates expectedCoordinates, Coordinates actualCoordinates)
        {
            if (Math.Abs(expectedCoordinates.Longitude - actualCoordinates.Longitude) > 0.000001)
            {
                return false;
            }

            if (Math.Abs(expectedCoordinates.Latitude - actualCoordinates.Latitude) > 0.000001)
            {
                return false;
            }

            return true;
        }

        #endregion

    }
}
