using System;
using System.Collections.Generic;
using System.Text;
using SkoleProtokolLibrary.DBModels;

namespace SkoleProtokolLibrary.DTO
{
    public class SubjectDTO
    {

        #region Properties

        /// <summary>
        /// Name of the subject
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Classes that have the given subject
        /// </summary>
        public List<string> Classes { get; set; }

        #endregion


        #region Constructor

        public SubjectDTO(DBSubject subject)
        {
            Name = subject.Name;
            Classes = subject.Classes;
        }

        #endregion
    }
}
