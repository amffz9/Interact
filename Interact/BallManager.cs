using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

// Image brush:
// http://www.c-sharpcorner.com/uploadfile/mahesh/using-imagebrush-in-wpf/

namespace Interact
{
    class BallManager
    {
        List<Ball> balls;
        Canvas field;
        Clock masterClock;
        Random rand;

        public BallManager(Canvas field, Clock masterClock)
        {
            this.field = field;
            this.masterClock = masterClock;

            rand = new Random();

            balls = new List<Ball>();
        }

        public void CreateRandomBalls(Int32 numBalls)
        {
            Ball ball;

            //System.Windows.Media.Brush[] brushes = new System.Windows.Media.Brush[] 
            //{
            //    System.Windows.Media.Brushes.Red,
            //    System.Windows.Media.Brushes.Green,
            //    System.Windows.Media.Brushes.Blue,
            //    System.Windows.Media.Brushes.Yellow,
            //    System.Windows.Media.Brushes.Orange,
            //    System.Windows.Media.Brushes.Purple
            //};

            ImageBrush imgBrush1 = new ImageBrush();
            imgBrush1.ImageSource = new BitmapImage(new Uri(@"images\dinosaurcreature.png", UriKind.Relative));
            ImageBrush imgBrush2 = new ImageBrush();
            imgBrush2.ImageSource = new BitmapImage(new Uri(@"images\fishcreatures.png", UriKind.Relative));
            ImageBrush imgBrush3 = new ImageBrush();
            imgBrush3.ImageSource = new BitmapImage(new Uri(@"images\hairyblobcreature.png", UriKind.Relative));
            ImageBrush imgBrush4 = new ImageBrush();
            imgBrush4.ImageSource = new BitmapImage(new Uri(@"images\mohawkcreature.png", UriKind.Relative));
            ImageBrush imgBrush5 = new ImageBrush();
            imgBrush5.ImageSource = new BitmapImage(new Uri(@"images\roundfuzzycreature.png", UriKind.Relative));

            ImageBrush[] brushes = new ImageBrush[]
            {
                imgBrush1,
                imgBrush2,
                imgBrush3,
                imgBrush4,
                imgBrush5
            };

            for (Int32 i = 0; i < numBalls; i++)
            {
                Int32 dX = rand.Next(1, 15);
                Int32 dY = rand.Next(1, 15);
                Int32 x = rand.Next(0, 600);
                Int32 y = rand.Next(0, 600);
                Int32 radius = rand.Next(5, 50);
                ImageBrush brush = brushes[rand.Next(0, brushes.Length)];

                ball = new Ball(brush, radius, field, masterClock);
                ball.Place(new System.Drawing.Point(x, y), dX, dY);
                balls.Add(ball);
            }

            masterClock.Tick += new Clock.ClockTickHandler(DetectCollisions);
        }

        public void DetectCollisions(Int32 numTicks)
        {
            // This version compares each ball to all the other balls.
            // The first ball is compared to the balls after it in the list.
            // The second ball is compared to the balls after it in the list.
            // Etc. until the end of the list is reached.

            Int32 numBalls = balls.Count;

            for (Int32 i = 0; i < numBalls; i++)
            {
                Ball ball1 = balls[i];
                for (Int32 j = i + 1; j < numBalls; j++)
                {
                    Ball ball2 = balls[j];
                    Collision collision = CheckForCollision(ball1, ball2);
                    if (collision != null)
                    {
                        ball1.HandleCollision(collision);
                        ball2.HandleCollision(collision);
                    }
                }
            }
        }

        protected Collision CheckForCollision(Ball ball1, Ball ball2)
        {

            // Use pythogorean theorem to calculate distance between centers of spheres
            // and compare to combined radii of spheres.
            // If distance between centers is less than equal to combined radii then collision
            // has occurred.
            // No need to take square root.  Squared values can be compared.
            // Given a^2 + b^2 = c^2 to get distance between centers, c^2 can be compared to combinedRadii^2
            // Rather than comparing c to combinedRadii.  Square roots are computationally expensive.

            Int32 combinedRadii = ball1.Radius + ball2.Radius;

            Double deltaX = ball1.Position.X - ball2.Position.X;
            Double deltaY = ball1.Position.Y - ball2.Position.Y;

            if ((Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2)) <= Math.Pow(combinedRadii, 2))
            {
                return new Collision();
            }
            else
            {
                return null;
            }
        }
    }
}

