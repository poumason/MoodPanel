using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace App1
{
    public sealed partial class MoodPanel : UserControl
    {
        public delegate void MoodValueChangedEvent(Object sender, Int32 value);
        public event MoodValueChangedEvent MoodValueChanged = null;
        private Int32 oriMoodValue = 15;
        private double oriX = 42;
        private double oriY = 42;

        public MoodPanel()
        {
            this.InitializeComponent();
        }

        public Point GetTargetPoint()
        {
            return new Point(oriX, oriY);
        }

        private void CalcuteMood(Point point)
        {
            Double Y = 0;
            Double X = 0;
            Double offset = 0;
            Double offsetPi = 0;

            if (point.X < 150)
            {
                if (point.Y < 150)
                {
                    // LT
                    X = 150 - point.Y;
                    Y = 150 - point.X;
                    offset = 270;
                    offsetPi = Math.PI * 1.5;
                }
                else
                {
                    // LB
                    X = 150 - point.X;
                    Y = point.Y - 150;
                    offset = 180;
                    offsetPi = Math.PI;
                }
            }
            else
            {
                if (point.Y < 150)
                {
                    // RT
                    X = (point.X - 150);
                    Y = 150 - point.Y;
                }
                else
                {
                    // RB
                    X = point.Y - 150;
                    Y = (point.X - 150);
                    offset = 90;
                    offsetPi = Math.PI / 2;
                }
            }

            Double rad = Math.Atan(Y / X); // 弧度
            Double deg = (rad / (Math.PI / 180)); // 角度

            Double angle = offset + (90 - deg);

            Double cos = Math.Cos(rad - offsetPi);
            Double sin = Math.Sin(rad - offsetPi);
            oriX = 150 + (125 * cos);
            oriY = 150 - (125 * sin);

            Canvas.SetLeft(btnMood, oriX - 20);
            Canvas.SetTop(btnMood, oriY - 20);

            Int32 newMoodValue = GetMoodValue(angle);
            if (newMoodValue != oriMoodValue)
            {
                oriMoodValue = newMoodValue;
                if (MoodValueChanged != null)
                {
                    MoodValueChanged(this, oriMoodValue);
                }
            }
        }

        private Int32 GetMoodValue(Double angle)
        {
            Int32 resMoodValue = 0;

            if (angle > 337.5)
            {
                resMoodValue = 16;
            }
            else if (angle > 315)
            {
                resMoodValue = 15;
            }
            else if (angle > 292.5)
            {
                resMoodValue = 14;
            }
            else if (angle > 270)
            {
                resMoodValue = 13;
            }
            else if (angle > 247.5)
            {
                resMoodValue = 12;
            }
            else if (angle > 225)
            {
                resMoodValue = 11;
            }
            else if (angle > 202.5)
            {
                resMoodValue = 10;
            }
            else if (angle > 180)
            {
                resMoodValue = 9;
            }
            else if (angle > 157.5)
            {
                resMoodValue = 8;
            }
            else if (angle > 135)
            {
                resMoodValue = 7;
            }
            else if (angle > 112.5)
            {
                resMoodValue = 6;
            }
            else if (angle > 90)
            {
                resMoodValue = 5;
            }
            else if (angle > 67.5)
            {
                resMoodValue = 4;
            }
            else if (angle > 45)
            {
                resMoodValue = 3;
            }
            else if (angle > 22.5)
            {
                resMoodValue = 2;
            }
            else
            {
                resMoodValue = 1;
            }

            return resMoodValue;
        }

        private void OnEllipsePointerMoved(object sender, PointerRoutedEventArgs e)
        {
            Point point = e.GetCurrentPoint((UIElement)sender).Position;
            CalcuteMood(point);
        }

        private void OnEllipsePointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Point point = e.GetCurrentPoint((UIElement)sender).Position;
            CalcuteMood(point);
        }
    }
}
