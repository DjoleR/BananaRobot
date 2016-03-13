using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaRobot
{
    class Robot
    {
        private const int LED_PIN_UP = 26;
        private const int LED_PIN_DOWN = 19;
        private const int LED_PIN_LEFT = 16;
        private const int LED_PIN_RIGHT = 20;
        private const int LED_PIN_CENTRAL = 21;
        private Dictionary<string, Pin> Pins = new Dictionary<string, Pin>();
        private bool isValid = false;
        private bool isOn = false;
        private Random rand = new Random();
        private static int maxID = 0;
        private int ID;

        public Robot()
        {
            isOn = true;
            InitPins();
            this.ID = Robot.maxID + 1;
            Robot.maxID++;
        }

        public int getId()
        {
            return this.ID;
        }

        private void Reset()
        {
            foreach (KeyValuePair<string, Pin> entry in Pins)
            {
                if (entry.Key.CompareTo("central") != 0)
                    entry.Value.setIsOn(false);
                else
                    entry.Value.setIsOn(true);

                entry.Value.Update();
            }
        }

        private void InitPins()
        {
            Pins["up"] = new Pin(LED_PIN_UP);
            Pins["down"] = new Pin(LED_PIN_DOWN);
            Pins["left"] = new Pin(LED_PIN_LEFT);
            Pins["right"] = new Pin(LED_PIN_RIGHT);
            Pins["central"] = new Pin(LED_PIN_CENTRAL);

            foreach (KeyValuePair<string, Pin> entry in Pins)
            {
                if (!entry.Value.getIsValid())
                {
                    // TODO add logs so that there is more information on what went wrong.
                    this.isValid = false;
                    return;
                }
            }
            this.isValid = true;
        }

        internal void Stop()
        {
            this.isOn = false;
            Reset();
        }

        public void Start()
        {
            isOn = true;
            Reset();

            Pins["central"].setIsOn(false);
            Pins["central"].Update();

            if (!this.isValid)
                return;


            Move(1,-1);
        }

        public void Move(int dx, int dy)
        {
            int i;
            bool dx_move = false, dy_move = false;
            List<int> movement = new List<int>() { -1, 1, 1, -1, 0, 1, 1, 1, -1, 0, 1, -1 };

            i = rand.Next(0, movement.Count());
            dx = movement[i];
            switch (dx)
            {
                case 1:
                    {
                        Pins["right"].setIsOn(true);
                        Pins["left"].setIsOn(false);

                        dx_move = true;

                        break;
                    }
                case -1:
                    {
                        Pins["left"].setIsOn(true);
                        Pins["right"].setIsOn(false);

                        dx_move = true;

                        break;
                    }
                case 0:
                    {
                        Pins["right"].setIsOn(false);
                        Pins["left"].setIsOn(false);

                        dx_move = true;

                        break;
                    }
                default:
                    break;
            }

            i = rand.Next(0, movement.Count());
            dy = movement[i];
            switch (dy)
            {
                case 1:
                    {
                        Pins["down"].setIsOn(true);
                        Pins["up"].setIsOn(false);

                        dy_move = true;

                        break;
                    }
                case -1:
                    {
                        Pins["up"].setIsOn(true);
                        Pins["down"].setIsOn(false);

                        dy_move = true;

                        break;
                    }
                case 0:
                    {
                        Pins["down"].setIsOn(false);
                        Pins["up"].setIsOn(false);

                        dy_move = true;

                        break;
                    }
                default:
                    break;
            }

            if (dx_move)
            {
                Pins["right"].Update();
                Pins["left"].Update();
            }
            if (dy_move)
            {
                Pins["down"].Update();
                Pins["up"].Update();
            }
        }
    }
}
