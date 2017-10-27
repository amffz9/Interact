using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

// This version implements collision detection.  It has issues though.
// One of those issues is that balls can appear to capture each other.
// A better alogorithm is needed, but this is a good starting point for 
// discussing collisions.


namespace Interact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BallManager ballManager;
        Clock masterClock;
        Int32 numBalls = 20;

        public MainWindow()
        {
            masterClock = new Clock();

            InitializeComponent();

            DoRateChange(30);

            masterClock.Tick += new Clock.ClockTickHandler(HandleMasterClockTick);
            
            ballManager = new BallManager(field, masterClock);
            ballManager.CreateRandomBalls(numBalls);
            DoPlay();
        }

        private void Pause_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoPause();
        }

        private void Play_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DoPlay();
        }

        private void DoPause()
        {
            masterClock.Pause();
            Play.IsChecked = false;
            Pause.IsChecked = true;
        }

        private void DoPlay()
        {
            masterClock.Start();
            Play.IsChecked = true;
            Pause.IsChecked = false;
        }

        private void FPS_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;

            if (menuItem == FPS5) DoRateChange(5);
            else if (menuItem == FPS10) DoRateChange(10);
            else if (menuItem == FPS15) DoRateChange(15);
            else if (menuItem == FPS30) DoRateChange(30);
            else if (menuItem == FPS60) DoRateChange(60);
            else if (menuItem == FPS80) DoRateChange(80);
            else if (menuItem == FPS100) DoRateChange(100);   
        }

        private void DoRateChange(Int32 rate)
        {
            masterClock.Rate = rate;

            fps_slider.Value = rate;
            rateLabel.Content = rate + " fps";

            UncheckFPSMenuItems();

            switch (rate)
            {
                case 5:
                    FPS5.IsChecked = true;
                    break;
                case 10:
                    FPS10.IsChecked = true;
                    break;
                case 15:
                    FPS15.IsChecked = true;
                    break;
                case 30:
                    FPS30.IsChecked = true;
                    break;
                case 60:
                    FPS60.IsChecked = true;
                    break;
                case 80:
                    FPS80.IsChecked = true;
                    break;
                case 100:
                    FPS100.IsChecked = true;
                    break;
            }
        }

        private void UncheckFPSMenuItems()
        {
            FPS5.IsChecked = false;
            FPS10.IsChecked = false;
            FPS15.IsChecked = false;
            FPS30.IsChecked = false;
            FPS60.IsChecked = false;
            FPS80.IsChecked = false;
            FPS100.IsChecked = false;
        }

        private void fps_slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ballManager == null) return;
            Slider fpsSlider = sender as Slider;
            DoRateChange((Int32)fpsSlider.Value);
        }

        private void HandleMasterClockTick(Int32 tickCount)
        {
            frameCountLabel.Content = tickCount + " frames (ticks)";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                DoPause();
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                DoPlay();
            }

            if (e.Key == Key.Up)
            {
                Int32 rate = masterClock.Rate;
                rate++;
                if (rate > 100) rate = 100;
                DoRateChange(rate);
            }

            if (e.Key == Key.Down)
            {
                Int32 rate = masterClock.Rate;
                rate--;
                if (rate < 1) rate = 1;
                DoRateChange(rate);
            }
        }
    }
}
