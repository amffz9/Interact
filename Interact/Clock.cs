using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Interact
{
    class Clock
    {
        protected Int32 numTicks;
        protected String state;
        protected Int32 ticksPerSecond;

        System.Windows.Forms.Timer timer;

        public delegate void ClockTickHandler(Int32 tickNum);
        public event ClockTickHandler Tick;

        public Clock(Int32 ticksPerSecond = 30)
        {
            numTicks = 0;
            state = "stopped";
            this.ticksPerSecond = ticksPerSecond;

            timer = new System.Windows.Forms.Timer();
            SetInterval();
            timer.Tick += new EventHandler(TickHandler);
        }

        protected void TickHandler(Object obj, EventArgs e)
        {
            numTicks++;

            if (Tick != null)
            {
                Tick(numTicks);
            }
        }

        public void Start()
        {
            state = "running";
            timer.Start();
        }

        public void Pause()
        {
            state = "paused";
            timer.Stop();
        }

        public void Stop()
        {
            state = "stopped";
            timer.Stop();
            numTicks = 0;
        }

        public Int32 Rate
        {
            get
            {
                return ticksPerSecond;
            }
            set
            {
                // resolution is 1msec, so ticks per second cannot exceed 1000
                if (value <= 0 || value > 1000) return;
                ticksPerSecond = value;
                SetInterval();
            }
        }

        protected void SetInterval()
        {
            timer.Interval = 1000 / ticksPerSecond;
        }

    }
}
