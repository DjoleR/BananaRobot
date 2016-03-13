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
        private TemperatureSensor ts;
        private RobotAI ai;
		private CommunicationService comm;

        public MainPage()
        {
            this.InitializeComponent();

            r = new Robot();
            ts = new TemperatureSensor();

            ai = new PredictBot(400, 400);
			comm = new CommunicationService (r.getId (), "Banana Super Robot");

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();
            r.Start();
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            if (!timer.IsEnabled)
            {
                timer.Start();
                r.Start();
            }
            //r.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            r.Stop();
            timer.Stop();
        }

        private void Timer_Tick(object sender, object e)
        {
            string tempStr = ts.gimmeTemperature().ToString();
            this.tempTextBlock.Text = "Current tmp: " + tempStr;
            int temp = Int32.Parse(tempStr);
            AIWorld.Direction aiDir = ai.MakeMove(temp);
            MoveWithDir(aiDir);
			this.messageTextBlock.Text = comm.SendMessage(tempStr, ai.GetX(), ai.GetY());

        }

        private void MoveWithDir(AIWorld.Direction dir)
        {
            switch (dir)
            {
                case AIWorld.Direction.DOWN:
                    r.Move(-1, 0);
                    break;
                case AIWorld.Direction.UP:
                    r.Move(1, 0);
                    break;
                case AIWorld.Direction.LEFT:
                    r.Move(0, -1);
                    break;
                case AIWorld.Direction.RIGHT:
                    r.Move(0, 1);
                    break;
            }
        }

    }
}
