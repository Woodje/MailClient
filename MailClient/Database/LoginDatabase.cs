using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace MailClient
{
    class LoginDatabase
    {
        // Declaring the variables needed the use of SQLite.
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;
        private SQLiteDataReader dbQuery;

        // Declaring and initializing the strings needed for the name of the database and its password.
        private string  dbName = "MailClient.db",
                        dbPassword = "MailClientPassword";

        // Create a struct that can hold usermails, passwords, and autologin.
        public struct UserInfo
        {
            public string userMail, password, autoLogin;
        };
      
        public LoginDatabase()
        {
            // Initialize the database from the constructor.
            InitDatabase();
        }

        private void InitDatabase()
        {
            // Create the connection string needed.
            dbConnection = new SQLiteConnection("Data Source =" + dbName + ";" + "Version = 3;");

            // Associate the connection string with an SQLiteCommand.
            dbCommand = new SQLiteCommand(dbConnection);

            // Check if the database-file exists.
            if (!File.Exists(dbName))
            {
                // Create the database-file.
                SQLiteConnection.CreateFile(dbName);

                // Open the newly created database.
                dbConnection.Open();

                // Create a table called "mailaddresses" with nine columns.
                dbCommand.CommandText =   "CREATE TABLE mailaddresses (address TEXT, password TEXT, receiveserver TEXT, receiveport INT, receivessl TEXT,"
                                      + "sendserver TEXT, sendport INT, sendssl TEXT, autologin TEXT);";

                // Execute the newly created command.
                dbCommand.ExecuteNonQuery();

                // Create a table called "mails" with two columns called "address" and "rawmessage".
                dbCommand.CommandText = "CREATE TABLE mails (address TEXT, rawmessage TEXT);";

                // Execute the newly created command.
                dbCommand.ExecuteNonQuery();

                // Give the database a simple password.
                dbConnection.ChangePassword(dbPassword);

                // Close the database again. 
                dbConnection.Close();
            }

            // Add the password to the connection string.
            dbCommand.Connection.ConnectionString += "Password =" + dbPassword + ";";

            // Close the database again.
            dbConnection.Close();
        }

        private void ClearAutoLogin()
        {
            // Open the database.
            dbConnection.Open();

            // Make sure that any record with autologin set to true is set to false.
            dbCommand.CommandText = "UPDATE mailaddresses SET autologin='false' WHERE autologin='true';";

            // Execute the newly created command.
            dbCommand.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public void CreateUserMail(string userMail, string password, bool autoLogin)
        {
            // Check if an autologin is checked.
            if (autoLogin)
            {
                // Clear the database for any autologin set to true.
                ClearAutoLogin();
            }

            // Open the database.
            dbConnection.Open();

            // Make sure that no other record has the specified usermail in the database.
            dbCommand.CommandText = "DELETE FROM mailaddresses WHERE '" + userMail + "';";

            // Execute the newly created comand.
            dbCommand.ExecuteNonQuery();

            // Insert a mailaddress and password into the table called "mailaddresses".
            dbCommand.CommandText =   "INSERT INTO mailaddresses VALUES ('" + userMail + "', '" + password + "', 'NULL', 0, 'false', '"
                                  + "NULL', 0, 'false', '" + autoLogin.ToString().ToLower() + "');";

            // Execute the newly created command.
            dbCommand.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public void UpdateAutoLogin(string userMail, string password, bool autoLogin)
        {
            // Clear the database for any autologin set to true.
            ClearAutoLogin();

            // Open the database.
            dbConnection.Open();

            // Insert a mailaddress and password into the table called "mailaddresses".
            dbCommand.CommandText =   "UPDATE mailaddresses SET autologin='" + autoLogin.ToString().ToLower() + "'"
                                  + "WHERE address='" + userMail + "' AND password='" + password + "';";

            // Execute the newly created command.
            dbCommand.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public List<UserInfo> ReadUserInfo()
        {
            // Create a list for the user info.
            List<UserInfo> listUserInfo = new List<UserInfo>();

            // Open the database.
            dbConnection.Open();

            // Retrieve all records from the table called "mailaddresses".
            dbCommand.CommandText = "SELECT * FROM mailaddresses;";

            // Execute the newly created command.
            dbQuery = dbCommand.ExecuteReader();

            // Read the retrieved query, and write the results to the newly created list.
            while (dbQuery.Read())
                listUserInfo.Add(new UserInfo
                {
                    userMail = dbQuery["address"].ToString(),
                    password = dbQuery["password"].ToString(),
                    autoLogin = dbQuery["autologin"].ToString()
                });

            // Close the query-reader again.
            dbQuery.Close();

            // Close the database again. 
            dbConnection.Close();

            // Return the created list.
            return listUserInfo;
        }
    }
}
