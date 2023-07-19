using System;
using System.Collections.Generic;
using System.Text;

namespace LostLegend.Graphics.GUI.Interactions
{
    class ButtonSignalEvent
    {
        public string Action;
        public string Subject;

        public ButtonSignalEvent(string action = "none", string subject = "none")
        {
            Action = action;
            Subject = subject;
        }
    }
}
