using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    public class Database
    {   
        // Declaring the variables needed the use of SQLite.
        private SQLiteConnection dbConnection;
        private SQLiteCommand command;
        private SQLiteDataReader query;

        // Declaring and initializing the strings needed for the name of the database and its password.
        private string  dbName = "MailClient.db",
                        dbPassword = "MailClientPassword";

        // Create a struct that can hold usermails, passwords, server, port and ssl.
        public struct UserInfo 
        {
           public string userMail, password, server, ssl, autoLogin;
           public int port;
        };

        public Database()
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

                // Create a table called "mailaddresses" with five coulumns called "address", "password", "server", "port" and "ssl".
                command.CommandText = "CREATE TABLE mailaddresses (address VARCHAR(50), password VARCHAR(25), server VARCHAR(50), port INT, ssl VARCHAR(5), autologin VARCHAR(5));";

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
            command.CommandText = "INSERT INTO mailaddresses VALUES ('" + userMail + "', '" + password + "', 'NULL', 0, 'false', '" + autoLogin.ToString().ToLower() + "');";

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

        public void UpdateSettings(string userMail, string password, string server, int port, bool ssl)
        {
            // Open the database.
            dbConnection.Open();

            // Insert a mailaddress and password into the table called "mailaddresses".
            command.CommandText =   "UPDATE mailaddresses SET server='" + server + "', port=" + port + ", ssl='" + ssl + "'"
                                  + "WHERE address='" + userMail + "' AND password='" + password + "';";

            // Execute the newly created command.
            command.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public List<UserInfo> readUserInfo()
        {
            // Create a list for the usermails.
            List<UserInfo> userMailsAndPasswords = new List<UserInfo>();

            // Open the database.
            dbConnection.Open();

            // Retrieve all records from the table called "mailaddresses".
            command.CommandText = "SELECT * FROM mailaddresses;";

            // Execute the newly created command.
            query = command.ExecuteReader();

            // Read the retrieved query, and write the results to the newly created list.
            while (query.Read())
                userMailsAndPasswords.Add(new UserInfo {    userMail = query["address"].ToString(),
                                                            password = query["password"].ToString(),
                                                            server = query["server"].ToString(),
                                                            port =  (int)query["port"],
                                                            ssl  = query["ssl"].ToString(),
                                                            autoLogin = query["autologin"].ToString()});

            // Close the query-reader again.
            query.Close();

            // Close the database again. 
            dbConnection.Close();

            return userMailsAndPasswords;
        }
    }
}
