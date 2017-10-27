using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Controls;


// Point
// http://msdn.microsoft.com/en-us/library/s9b80s6c(v=vs.110).aspx

namespace Interact
{
    abstract class Sprite
    {
        protected Point position;
        protected Int32 radius;
        protected Canvas field;
        protected Clock masterClock;

        public Sprite(Int32 radius, Canvas field, Clock masterClock)
        {
            this.radius = radius;
            this.field = field;
            this.masterClock = masterClock;
        }

        public Int32 Radius
        {
            get
            {
                return radius;
            }
        }

        public Point Position
        {
            get
            {
                return position;
            }
        }

        protected Point CalculateTopLeft()
        {
            Point tl = new Point();
            tl.X = position.X - radius;
            tl.Y = position.Y - radius;
            return tl;
        }

        public abstract void Place(Point position, Int32 dY, Int32 dX);

    }
}
