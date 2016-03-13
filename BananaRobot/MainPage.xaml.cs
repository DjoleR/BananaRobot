using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace BananaRobot
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Robot r;
        private DispatcherTimer timer;
        private TemperatureSensor ts = new TemperatureSensor();
        public MainPage()
        {
            this.InitializeComponent();

            r = new Robot();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            r.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            r.Stop();
        }

        private void Timer_Tick(object sender, object e)
        {
            this.tempTextBlock.Text = "Current tmp: " + ts.gimmeTemperature().ToString();
        }

    }
}
