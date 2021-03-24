using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace SkoleProtokolAPI.ActiveTimer
{
    /// <summary>
    /// Registers and holds one active attendance code, when the code expires it is automatically removed and deleted
    /// </summary>
    public class ActiveAttendanceCode
    {

        #region InstanceFields

        private readonly ConcurrentQueue<ActiveAttendanceCode> _queue; 
        private Timer _timer; //The timer keeps track of remaining active time for the code
        private readonly string _attendanceCode;

        #endregion

        #region Properties

        /// <summary>
        /// Returns the active attendance code
        /// </summary>
        public string AttendanceCode
        {
            get { return _attendanceCode; }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of ActiveAttendanceCode and sets its lifetime to 10 min.
        /// </summary>
        /// <param name="queue">An external ConcurrentQueue used to store the active codes</param>
        /// <param name="attendanceCode">The active code to be stored in the provided ConcurrentQueue</param>
        public ActiveAttendanceCode(ConcurrentQueue<ActiveAttendanceCode> queue, string attendanceCode)
        {
            _queue = queue;
            _attendanceCode = attendanceCode;

            queue.Enqueue(this);

            _timer = new Timer();
            _timer.Interval = 600000;//sets timer to 10 min.
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
