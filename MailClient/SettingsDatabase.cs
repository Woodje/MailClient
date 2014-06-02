using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    class SettingsDatabase
    {
        // Declaring the variables needed the use of SQLite.
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;
        private SQLiteDataReader dbQuery;

        // Declaring and initializing the strings needed for the name of the database and its password.
        private string  dbName = "MailClient.db",
                        dbPassword = "MailClientPassword";

        // Create a struct that can hold usermails, passwords, server, port and ssl.
        public struct UserSettings
        {
            public string userMail, password, receiveServer, receiveSSL, sendServer, sendSSL;
            public int receivePort, sendPort;
        };

        public SettingsDatabase()
        {
            // Initialize the database from the constructor.
            InitDatabase();
        }

        private void InitDatabase()
        {
            // Create the connection string needed.
            dbConnection = new SQLiteConnection("Data Source =" + dbName + "; Version = 3; Password =" + dbPassword + ";");

            // Associate the connection string with an SQLiteCommand.
            dbCommand = new SQLiteCommand(dbConnection);

            // Close the database again.
            dbConnection.Close();
        }

        public void UpdateSettings(string userMail, string password, string receiveServer, int receivePort, bool receiveSSL, string sendServer, int sendPort, bool sendSSL)
        {
            // Open the database.
            dbConnection.Open();

            // Insert a mailaddress and password into the table called "mailaddresses".
            dbCommand.CommandText = "UPDATE mailaddresses SET receiveserver='" + receiveServer + "', receiveport=" + receivePort + ", receivessl='" + receiveSSL + "', "
                                  + "sendserver='" + sendServer + "', sendport=" + sendPort + ", sendssl='" + sendSSL + "'"
                                  + "WHERE address='" + userMail + "' AND password='" + password + "';";

            // Execute the newly created command.
            dbCommand.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public List<UserSettings> ReadUserSettings()
        {
            // Create a list for the user settings.
            List<UserSettings> listUserSettings = new List<UserSettings>();

            // Open the database.
            dbConnection.Open();

            // Retrieve all records from the table called "mailaddresses".
            dbCommand.CommandText = "SELECT * FROM mailaddresses;";

            // Execute the newly created command.
            dbQuery = dbCommand.ExecuteReader();

            // Read the retrieved query, and write the results to the newly created list.
            while (dbQuery.Read())
                listUserSettings.Add(new UserSettings
                {
                    userMail = dbQuery["address"].ToString(),
                    password = dbQuery["password"].ToString(),
                    receiveServer = dbQuery["receiveserver"].ToString(),
                    receivePort = (int)dbQuery["receiveport"],
                    receiveSSL = dbQuery["receivessl"].ToString(),
                    sendServer = dbQuery["sendserver"].ToString(),
                    sendPort = (int)dbQuery["sendport"],
                    sendSSL = dbQuery["sendssl"].ToString(),
                });

            // Close the query-reader again.
            dbQuery.Close();

            // Close the database again. 
            dbConnection.Close();

            // Return the created list.
            return listUserSettings;
        }
    }
}
