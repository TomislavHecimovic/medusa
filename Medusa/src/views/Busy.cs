using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medusa.Views
{
    public class Busy
    {
        public enum BusyType
        {
            /// <summary>
            /// When there is no way to determine the duration of the operation
            /// </summary>
            INDETERMINATE,
            /// <summary>
            /// When the duration is known in advance and a progress bar can be shown
            /// </summary>
            PROGRESS_BAR,
            /// <summary>
            /// The app is not doing anything in the background
            /// </summary>
            NONE
        }
    }
}
