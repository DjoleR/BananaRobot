using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BananaRobot
{
    class CommunicationService
    {
        
        public static string PrepareAndSendMessage(int ID, string name, string temperature, int posX, int posY, string timestamp)
        {
            return ID.ToString() + '|' + name + '|' + temperature + '|' + posX + '|' + posY + '|' + timestamp;
        }
    }
}
