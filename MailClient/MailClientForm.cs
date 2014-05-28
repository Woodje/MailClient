using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Threading;

namespace MailClient
{
    public partial class MailClientForm : Form
    {
        // Declaring the variable for the LoginForm.
        private LoginForm loginForm;

        // Declaring the variable for the SettingsForm.
        private SettingsForm settingsForm;

        // Declaring the variable for the connection to the server.
        private ConnectToServer connectToServer;

        // Declare and initialize the timeoutCounter for the connection to the server.
        private int timeOutCounter = 0;

        public MailClientForm()
        {
            // Call the initializing of the components for the form.
            InitializeComponent();
        }

        private void MailClientForm_Shown(object sender, EventArgs e)
        {
            // Give the webbrowser a grayish background after is is shown.
            webBrowserView.DocumentText = "<body bgcolor='#FAFAFA'>";

            // Instantiate the loginform.
            loginForm = new LoginForm();

            // Show the loginform as a dialog, so the user cannot access the other form.
            loginForm.ShowDialog();
        }

        private void StartConnection()
        {
            // Start the connection to the server.
            connectToServer = new ConnectToServer(labelStatus, textBoxMail.Text, textBoxPassword.Text, progressBarStatus);

            //string rawMessage = Convert.ToBase64String(connectToServer.client.GetMessage(1).RawMessage);

            //webBrowser1.DocumentText = rawMessage;

            //OpenPop.Mime.Message ko = new OpenPop.Mime.Message(Convert.FromBase64String(rawMessage));

            //webBrowser1.DocumentText = ko.FindFirstHtmlVersion().GetBodyAsText();

            testing();
        }

        private void labelStatus_ForeColorChanged(object sender, EventArgs e)
        {
            // Check if the labels texts color is blue. (Meaning: ready to start the connection)
            if (labelStatus.ForeColor == Color.Blue)
            {
                // Create a Thread delegate which matches the function: "StartConnection".
                Thread threadMailClient = new Thread(StartConnection);

                // Tell the thread to run in the background so that is stops when the main thread is stopped.
                threadMailClient.IsBackground = true;

                // Start the thread.
                threadMailClient.Start();
            }
            // Check if the labels texts color is red. (Meaning: error message)
            else if (labelStatus.ForeColor == Color.Red)
            {
                // Check if it is the user credentials that fails.
                if (labelStatus.Text.Contains("user credentials"))
                {
                    // Instantiate the loginform with a new instance.
                    loginForm = new LoginForm();

                    // Show the loginform as a dialog, so the user cannot access the other form.
                    loginForm.ShowDialog();
                }
                // Check if something else is wrong.
                else if (labelStatus.Text != "Disconnected")
                {
                    // Instantiate the settingsform, assuming that these are wrong.
                    settingsForm = new SettingsForm();

                    // Show the settingsform as a dialog, so the user cannot access the other form.
                    settingsForm.ShowDialog();
                }
            }
            // Check if the labels texts color is yellow, and that the text says "Connected". (Meaning: connection is about to timeout)
            else if (labelStatus.ForeColor == Color.Yellow && labelStatus.Text == "Connected")
            {
                // Send a NOOP (NoOperation) command to the server, keeping the connection active.
                connectToServer.KeepConnectionActive();

                // Reset the timeout counter again.
                timeOutCounter = 0;
            }
        }

        private void timerWaiting_Tick(object sender, EventArgs e)
        {
            // Check if the progressbars value is at its max value and if the status labels texts color is some sort of blue.
            if (progressBarStatus.Value == 100 && labelStatus.ForeColor == Color.Blue || progressBarStatus.Value == 100 && labelStatus.ForeColor == Color.CornflowerBlue)
                // Set the progressbars value to zero.
                progressBarStatus.Value = 0;

            // Check if the status labels texts color is some sort of blue.
            if (labelStatus.ForeColor == Color.Blue || labelStatus.ForeColor == Color.CornflowerBlue)
                // Increment the current progressbars value with one.
                progressBarStatus.Value++;

            // Check the value of the timeout counter while also incrementing it with one for every milisecond.
            // Also make sure that the status labels forecolor is not read and that its text is saying "Connected".
            if (timeOutCounter++ >= 150 && labelStatus.ForeColor != Color.Red && labelStatus.Text == "Connected")
                // Change the status labels texts color to yellow, stating that the connection is about to timeout.
                labelStatus.ForeColor = Color.Yellow;
        }

        private void listBoxMails_SelectedIndexChanged(object sender, EventArgs e)
        {
            webBrowserView.DocumentText = connectToServer.client.GetMessage(listBoxMails.Items.Count - listBoxMails.SelectedIndex).FindFirstHtmlVersion().GetBodyAsText();
        }

        private void testing()
        {
            // Check if the status is still "Connected".
            if (labelStatus.Text == "Connected")
            {
                ComponentChanges.changeLabelForeColor(labelStatus, Color.CornflowerBlue);

                // Output that current action to the status label.
                ComponentChanges.changeLabelText(labelStatus, "Retrieving mails");

                // Retrieve the amount of mails from the current mail server.
                int mailCount = connectToServer.client.GetMessageCount();

                // Go through every mail from the top and down.
                for (int i = mailCount; i >= 1; i--)
                {
                    // Retrieve and input the messages from the current mail server into the listbox.
                    ComponentChanges.changeListBoxItems(listBoxMails, connectToServer.client.GetMessageHeaders(i).From.Address);
                }

                // Set the output back to "Connected".
                ComponentChanges.changeLabelText(labelStatus, "Connected");
                ComponentChanges.changeLabelForeColor(labelStatus, Color.Green);
                // Announce through the status progressbar that we are done with retrieving mails.
                ComponentChanges.changeProgressBarValue(progressBarStatus, 100);
            }
        }
    }
}
