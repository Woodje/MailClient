using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailClient
{
    class MailDatabase
    {
        // Declaring the variables needed the use of SQLite.
        private SQLiteConnection dbConnection;
        private SQLiteCommand dbCommand;
        private SQLiteDataReader dbQuery;

        // Declaring and initializing the strings needed for the name of the database and its password.
        private string  dbName = "MailClient.db",
                        dbPassword = "MailClientPassword";

        public MailDatabase()
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

        public int ReadMailCount(string userMail)
        {
            // Declare a variable for the result integer.
            int result = 0;

            // Open the database.
            dbConnection.Open();

            // Retrieve all records from the table called "mails" for the specified usermail.
            dbCommand.CommandText = "SELECT * FROM mails WHERE address='" + userMail + "';";

            // Execute the newly created command.
            dbQuery = dbCommand.ExecuteReader();

            // Read the retrieved query.
            while (dbQuery.Read())
                // Increment the result with one for each row.
                result++;

            // Close the query-reader again.
            dbQuery.Close();

            // Close the database again. 
            dbConnection.Close();

            // Return the result.
            return result;
        }

        public void InsertMail(string userMail, string rawMessage)
        {
            // Open the database.
            dbConnection.Open();

            // Insert a mailaddress and password into the table called "mailaddresses".
            dbCommand.CommandText = "INSERT INTO mails VALUES ('" + userMail + "', '" + rawMessage + "');";

            // Execute the newly created command.
            dbCommand.ExecuteNonQuery();

            // Close the database again. 
            dbConnection.Close();
        }

        public Message ReadMail(string userMail, int rowNumber)
        {
            // Declare a variable for the converted results.
            Message result;

            // Open the database.
            dbConnection.Open();

            // Retrieve all records from the table called "mails" for the specified usermail.
            dbCommand.CommandText = "SELECT * FROM mails WHERE address='" + userMail + "';";

            // Execute the newly created command.
            dbQuery = dbCommand.ExecuteReader();

            // Read the retrieved query, and convert the result to bytes from the current string.
            while (dbQuery.Read())
            {
                // Check if the current row is the one specified.
                if (dbQuery.StepCount == rowNumber)
                {
                    // Convert the result to bytes and then to a message and put this into the message variable.
                    result = new Message(Convert.FromBase64String(dbQuery["rawmessage"].ToString()));

                    // Close the query-reader again.
                    dbQuery.Close();

                    // Close the database again. 
                    dbConnection.Close();

                    // Break out of the loop by returning the converted mathing result.
                    return result;
                }
            }
                
            // Close the query-reader again.
            dbQuery.Close();

            // Close the database again. 
            dbConnection.Close();

            // Return a null if nothing is found.
            return null;
        }
    }
}
