using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BananaRobot
{
    class CommunicationService
    {
		private const string username = "tmbAdmin";
		private const string pwd = "Imagine123";
		private const string serverURL = "tcp:tmbserver.database.windows.net,1433";
		private const string dbName = "TempMonitorBot_SQL_DB";
		private const string tableName = "RobotTempJournal";

		private int id;
		private string name;
		private SqlConnection connection;

		public CommunicationService(int robotID, string robotName)
		{
			id = robotID;
			name = robotName;
			connection = new SqlConnection(
				"user id="+username+";" + 
				"password="+pwd+";" +
				"server="+serverURL+";" + 
				"Trusted_Connection=true;" + 
				"database="+dbName+"; " + 
				"connection timeout=30");

			//try {
				connection.Open();
			/*}
			catch (Exception e) {
				// nothing, but there is an error
				throw e;
			}*/
		}

        public string SendMessage(string temperature, int posX, int posY)
        {
			var timestamp = DateTime.Now.ToString ();

			SqlCommand cmd = new SqlCommand (
				"INSERT INTO RobotTempJournal VALUES("+
				id.ToString()+","+
				name+","+
				temperature+","+
				posX+","+
				posY+","+
				timestamp+")", connection);

			cmd.ExecuteNonQuery ();	
        }
    }
}
