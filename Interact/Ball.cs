using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Interact
{
    class Ball : Sprite
    {
        System.Windows.Media.Brush brush;
        Ellipse ellipse;
        Int32 dY;
        Int32 dX;

        public Ball(System.Windows.Media.Brush brush, Int32 radius, Canvas field, Clock masterClock) : base(radius, field, masterClock)
        {
            this.brush = brush;
            ellipse = new Ellipse();
            ellipse.Width = ellipse.Height = radius * 2;
            ellipse.Stroke = System.Windows.Media.Brushes.Black;
            ellipse.Fill = this.brush;
        }

        public override void Place(Point position, Int32 dY, Int32 dX)
        {
            this.position = position;
            this.dY = dY;
            this.dX = dX;

            field.Children.Add(ellipse);
            UpdateCanvasPosition();

            masterClock.Tick += new Clock.ClockTickHandler(Update);
        }

        protected void Update(Int32 tickNum)
        {

            position.X += dX;
            position.Y += dY;

            if ((position.Y + radius) > field.ActualHeight) { dY = -dY; position.Y = (Int32)field.ActualHeight - radius; }
            if ((position.Y - radius) < 0) { dY = -dY; position.Y = radius; }
            if ((position.X + radius) > field.ActualWidth) { dX = -dX; position.X = (Int32)field.ActualWidth - radius; }
            if ((position.X - radius) < 0) { dX = -dX; position.X = radius; }

            UpdateCanvasPosition();
        }

        protected void UpdateCanvasPosition()
        {
            Point tl = CalculateTopLeft();
            ellipse.SetValue(Canvas.LeftProperty, (double)tl.X);
            ellipse.SetValue(Canvas.TopProperty, (double)tl.Y);
        }

        public void HandleCollision(Collision collision)
        {
            // If collision occurs, reverse the direction of the ball.
            dY = -dY;
            dX = -dX;
        }
    }
}
