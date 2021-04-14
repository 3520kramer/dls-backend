using System;
using System.Collections.Generic;
using System.Text;

namespace SkoleProtokolLibrary.DTO
{
    public class StartRollCallDTO
    {

        #region Properties

        public List<string> Subjects { get; set; }

        public List<string> Classes { get; set; }

        public List<ModuleDTO> Modules { get; set; }

        #endregion


        #region Constructor

        public StartRollCallDTO(List<string> subjects, List<string> classes, List<ModuleDTO> modules)
        {
            Subjects = subjects;
            Classes = subjects;
            Modules = modules;
        }

        #endregion

    }
}
