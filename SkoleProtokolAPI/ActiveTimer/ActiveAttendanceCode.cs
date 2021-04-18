using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using SkoleProtokolLibrary.DTO;
using SkoleProtokolLibrary.Models;

namespace SkoleProtokolAPI.ActiveTimer
{
    /// <summary>
    /// Registers and holds one active attendance code and related information,
    /// when the code expires it is automatically removed and deleted
    /// </summary>
    public class ActiveAttendanceCode
    {

        #region InstanceFields

        private readonly ConcurrentQueue<ActiveAttendanceCode> _queue; 
        private readonly Timer _timer = new Timer(); //The timer keeps track of remaining active time for the code
        private readonly string _attendanceCode;
        private readonly string _teacherId;
        private readonly string _subject;
        private readonly List<string> _classes;
        private readonly List<Module> _modules = new List<Module>();
        private readonly Coordinates _coordinates;
        private int _numberOfStudents;
        private readonly CodeDuration _duration;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the active attendance code
        /// </summary>
        public string AttendanceCode
        {
            get { return _attendanceCode; }
        }

        // The following properties are used to get the information that is related to the code
        // Such as the subject the code is active for.

        public string TeacherId
        {
            get { return _teacherId; }
        }

        
        public string Subject
        {
            get { return _subject; }
        }

        public List<string> Classes
        {
            get { return _classes; }
        }

        public List<Module> Modules
        {
            get { return _modules; }
        }

        public Coordinates Coordinates
        {
            get { return _coordinates; }
        }

        public int NumberOfStudents
        {
            get { return _numberOfStudents; }
            set { _numberOfStudents = value; }
        }

        public CodeDuration Duration
        {
            get { return _duration; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of ActiveAttendanceCode and sets its lifetime to 10 min.
        /// </summary>
        /// <param name="queue">An external ConcurrentQueue used to store the active codes</param>
        /// <param name="attendanceCode">The active code to be stored in the provided ConcurrentQueue</param>
        /// <param name="request">Information used to request the attendanceCode</param>
        public ActiveAttendanceCode(ConcurrentQueue<ActiveAttendanceCode> queue, string attendanceCode, RequestAttendanceCodeDTO request)
        {
            _queue = queue;
            _attendanceCode = attendanceCode;
            _teacherId = request.TeacherId;
            _subject = request.Subject;
            _classes = request.Classes;
            request.Modules.ForEach(module => _modules.Add(new Module(module)));
            if (request.Coordinates != null)
            {
                _coordinates = new Coordinates(request.Coordinates);
            }
            _numberOfStudents = request.NumberOfStudents;
            _duration = new CodeDuration(request.Duration);

            queue.Enqueue(this);

            
            _timer.Interval = _duration.Minutes * 60000;//Converts the duration from minutes to milliseconds and sets the timer.
            //DeleteFromActiveCodes is called when the timer expires
            _timer.Elapsed += new ElapsedEventHandler(DeleteFromActiveCodes);
            _timer.Start();
        }

        #endregion

        #region Methods
        /// <summary>
        /// Removes and deletes expired attendance codes from the ConcurrentQueue.
        /// </summary>
        /// <param name="sender">The object that raised the event</param>
        /// <param name="e">The event data</param>
        private void DeleteFromActiveCodes(object sender, EventArgs e)
        {
            //This is necessary to remove the ActiveAttendanceCode object from the ConcurrentQueue,
            //and will be deleted once the method ends.
            ActiveAttendanceCode toRemove;
            _queue.TryDequeue(out toRemove);
        }

        #endregion

    }
}
