using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;

namespace BananaRobot
{
    class Pin
    {
        private int LED_PIN { get; set; }
        private GpioPin pin;
        private bool isOn;
        private bool isValid = false;

        public Pin(int LedPin)
        {
            this.LED_PIN = LedPin;
            this.isOn = true;

            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                pin = null;
                this.isValid = false;
                return;
            }

            pin = gpio.OpenPin(LED_PIN);
            pin.Write(GpioPinValue.Low);
            pin.SetDriveMode(GpioPinDriveMode.Output);

            this.isValid = true;
        }

        public void Update()
        {
            if (pin.Read() == GpioPinValue.High && isOn)
                pin.Write(GpioPinValue.Low);
            else if (pin.Read() == GpioPinValue.Low && !isOn)
                pin.Write(GpioPinValue.High);
        }

        public void setIsOn(bool isOn)
        {
            this.isOn = isOn;
        }

        public bool getIsOn()
        {
            return this.isOn;
        }

        public bool getIsValid()
        {
            return this.isValid;
        }
    }
}
