using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    /// <summary>
    /// This is the body of a HTTP-response to a teacher's request for initial information to start a RollCall
    /// </summary>
    public class StartRollCallDTO
    {

        #region Properties

        /// <summary>
        /// The teacher's subjects
        /// </summary>
        public List<string> Subjects { get; set; }

        /// <summary>
        /// This is only the classes belonging to the first subject of the list of subjects.
        /// </summary>
        public List<string> Classes { get; set; }

        /// <summary>
        /// Modules is the time segments of the lesson the teacher wants to register attendance for.
        /// </summary>
        public List<ModuleDTO> Modules { get; set; }

        #endregion


        #region Constructor

        public StartRollCallDTO(List<string> subjects, List<string> classes, List<ModuleDTO> modules)
        {
            Subjects = subjects;
            Classes = classes;
            Modules = modules;
        }

        #endregion

    }
}
