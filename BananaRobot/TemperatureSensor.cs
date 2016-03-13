using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Spi;
using Windows.Devices.Enumeration;
using Windows.Devices.Adc;

namespace BananaRobot
{
    class TemperatureSensor
    {

        enum AdcDevice { NONE, MCP3002, MCP3208, MCP3008 };

        private const Int32 SPI_CHIP_SELECT_LINE = 0;
        private const string SPI_CONTROLLER_NAME = "SPI0";
        private SpiDevice SpiDisplay;
        byte[] readBuffer = new byte[3]; /*this is defined to hold the output data*/
        byte[] writeBuffer = new byte[3] { 0x68, 0x00, 0x00 };
        private readonly byte[] MCP3008_CONFIG = { 0x01, 0x80 }; /* 00000001 10000000 channel configuration data for the MCP3008 */
        private AdcDevice ADC_DEVICE = AdcDevice.MCP3008;
        public TemperatureSensor()
        {
            InitSPI();
        }
        private async void InitSPI()
        { 
            try
            {
                var settings = new SpiConnectionSettings(SPI_CHIP_SELECT_LINE);
                settings.ClockFrequency = 500000;// 10000000;
                settings.Mode = SpiMode.Mode0; //Mode3;

                string spiAqs = SpiDevice.GetDeviceSelector(SPI_CONTROLLER_NAME);
                var deviceInfo = await DeviceInformation.FindAllAsync(spiAqs);
                SpiDisplay = await SpiDevice.FromIdAsync(deviceInfo[0].Id, settings);
            }
            catch (Exception ex)
            {
                throw new Exception("SPI Initialization Failed", ex);
            }

        }

        public string gimmeTemperature()
        {
            SpiDisplay.TransferFullDuplex(writeBuffer, readBuffer);
            switch (ADC_DEVICE)
            {
                case AdcDevice.MCP3008:
                    writeBuffer[0] = MCP3008_CONFIG[0];
                    writeBuffer[1] = MCP3008_CONFIG[1];
                    break;
            }
            int res = convertToInt(readBuffer);
            return res.ToString();

        }

        public int convertToInt(byte[] data)
        {
            int result = 0;
            switch (ADC_DEVICE)
            {
                case AdcDevice.MCP3008:
                    result = data[1] & 0x03;
                    result <<= 8;
                    result += data[2];
                    break;
            }
            return result;
        }
    }
}
