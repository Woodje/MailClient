using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    class ConnectToServer
    {
        // Declaring the variable for the Pop3Client.
        public Pop3Client client;

        // Declaring the variable for the status Label.
        private Label status;

        // Declaring the variables for the usermail and the password.
        private string userMail, password;

        // Declaring the variable for the status progressbar.
        private ProgressBar statusProgressBar;

        // Declaring the variable for the Database.
        Database dbMailClient;

        public ConnectToServer(Label status, string userMail, string password, ProgressBar statusProgressBar)
        {
            // Make the provided label the status label.
            this.status = status;

            // Initialize the provided usermail and password.
            this.userMail = userMail;
            this.password = password;

            // Make the provided progressbar the status progressbar.
            this.statusProgressBar = statusProgressBar;

            try
            {
                // Initialize the connection to the server from this custom constructor.
                InitConnectionToServer();
            }
            catch (Exception e)
            {
                // Output the error to the user.
                ComponentChanges.changeLabelText(status, e.Message);

                // Change the outputted texts color to Red.
                ComponentChanges.changeLabelForeColor(status, Color.Red);

                // Announce through the status progressbar that we are done with trying to establish the connection.
                ComponentChanges.changeProgressBarValue(statusProgressBar, 100);
            }
        }

        private void InitConnectionToServer()
        {
            // Instantiating the client.
            client = new Pop3Client();

            // Instantiate the database.
            dbMailClient = new Database();

            // Output that connection to the server is being made.
            ComponentChanges.changeLabelText(status, "Connecting to server");

            // Go through all the user records in the database.
            foreach (Database.UserInfo value in dbMailClient.readUserInfo())
            {
                // Check if the record in the database matches the usermail and password.
                if (userMail == value.userMail && password == value.password)
                {
                    // Start connection to the server with the values from the database.
                    client.Connect(value.server, value.port, bool.Parse(value.ssl));

                    // Break out of the loop because a match is found.
                    break;
                }
            }

            // Check if the Pop3Client is connected.
            if (client.Connected)
            {
                // Output that connection to the server is established.
                ComponentChanges.changeLabelText(status, "Connected to server");

                // Change the outputted texts color to green.
                ComponentChanges.changeLabelForeColor(status, Color.Green);
            }
            else
            {
                // Output that connection to the server is not established.
                ComponentChanges.changeLabelText(status, "Disconnected");

                // Change the outputted texts color to Red.
                ComponentChanges.changeLabelForeColor(status, Color.Red);
            }

            // Authenticate the user.
            client.Authenticate(userMail, password);

            // Output that autentication is accepted.
            ComponentChanges.changeLabelText(status, "Connected");

            // Announce that we are done with the establishment of the connection.
            ComponentChanges.changeProgressBarValue(statusProgressBar, 100);
        }

        public void KeepConnectionActive()
        {
            try
            {
                // Send a NOOP (NoOperation) command to the server, keeping the connection active.
                client.NoOperation();

                // Change the color back to green again.
                ComponentChanges.changeLabelForeColor(status, Color.Green);
            }
            catch (Exception e)
            {
                // Output the error to the user.
                ComponentChanges.changeLabelText(status, e.Message);

                // Change the outputted texts color to Red.
                ComponentChanges.changeLabelForeColor(status, Color.Red);

                // Announce through the status progressbar that we are done with trying to establish the connection.
                ComponentChanges.changeProgressBarValue(statusProgressBar, 100);
            }
        }
    }
}
