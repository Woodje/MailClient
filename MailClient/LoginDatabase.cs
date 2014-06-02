using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    class LoginDatabase
    {
        // Declaring the variables needed the use of SQLite.
        private SQLiteConnection dbConnection;
        private SQLiteCommand command;
        private SQLiteDataReader query;

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
            command = new SQLiteCommand(dbConnection);

            // Check if the database-file exists.
            if (!File.Exists(dbName))
            {
                // Create the database-file.
                SQLiteConnection.CreateFile(dbName);

                // Open the newly created database.
                dbConnection.Open();

                // Create a table called "mailaddresses" with nine columns.
                command.CommandText =   "CREATE TABLE mailaddresses (address TEXT, password TEXT, receiveserver TEXT, receiveport INT, receivessl TEXT,"
                                      + "sendserver TEXT, sendport INT, sendssl TEXT, autologin TEXT);";

                // Execute the newly created command.
                command.ExecuteNonQuery();

                // Create a table called "mails" with two columns called "address" and "rawmessage".
                command.CommandText = "CREATE TABLE mails (address TEXT, rawmessage TEXT);";

                // Execute the newly created command.
                command.ExecuteNonQuery();

                // Give the database a simple password.
                dbConnection.ChangePassword(dbPassword);

                // Close the database again. 
                dbConnection.Close();
            }

            // Add the password to the connection string.
            command.Connection.ConnectionString += "Password =" + dbPassword + ";";

            // Close the database again.
            dbConnection.Close();
        }

        private void ClearAutoLogin()
        {
            // Open the database.
            dbConnection.Open();

            // Make sure that any record with autologin set to true is set to false.
            command.CommandText = "UPDATE mailaddresses SET autologin='false' WHERE autologin='true';";

            // Execute the newly created command.
            command.ExecuteNonQuery();

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
            command.CommandText = "DELETE FROM mailaddresses WHERE '" + userMail + "';";

            // Execute the newly created comand.
            command.ExecuteNonQuery();

            // Insert a mailaddress and password into the table called "mailaddresses".
            command.CommandText =   "INSERT INTO mailaddresses VALUES ('" + userMail + "', '" + password + "', 'NULL', 0, 'false', '"
                                  + "NULL', 0, 'false', '" + autoLogin.ToString().ToLower() + "');";

            // Execute the newly created command.
            command.ExecuteNonQuery();

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
            command.CommandText =   "UPDATE mailaddresses SET autologin='" + autoLogin.ToString().ToLower() + "'"
                                  + "WHERE address='" + userMail + "' AND password='" + password + "';";

            // Execute the newly created command.
            command.ExecuteNonQuery();

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
            command.CommandText = "SELECT * FROM mailaddresses;";

            // Execute the newly created command.
            query = command.ExecuteReader();

            // Read the retrieved query, and write the results to the newly created list.
            while (query.Read())
                listUserInfo.Add(new UserInfo
                {
                    userMail = query["address"].ToString(),
                    password = query["password"].ToString(),
                    autoLogin = query["autologin"].ToString()
                });

            // Close the query-reader again.
            query.Close();

            // Close the database again. 
            dbConnection.Close();

            // Return the created list.
            return listUserInfo;
        }
    }
}
