using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace MailClient
{
    public partial class MailClientForm : Form
    {
        // Declaring the variable for the LoginForm.
        private LoginForm loginForm;

        // Declaring the variable for the SettingsForm.
        private SettingsForm settingsForm;

        // Declaring the variable for the SendMailForm.
        private SendMailForm sendMailForm;

        // Declaring the variable for the connection to the server.
        private ConnectToServer connectToServer;

        // Declaring the variable for the mailDatabase.
        private MailDatabase mailDatabase;

        // Declare and initialize the timeoutCounter for the connection to the server.
        private int timeOutCounter = 0;

        // Declare the variable for the latest recieved mails date.
        private DateTime newestMailDateTime;

        // Declare the variable needed for showing the programs status.
        private static Label statusLabel;

        // Declare the variable that holds the currently showed mailmessage.
        private OpenPop.Mime.Message mailMessage;

        // Create an enumeration that can hold the programs status.
        public enum ProgramStatus
        {
            // This is the succes status, and means that everything is okay.
            Connected,

            // States that connection to the server is made.
            ConnectedToServer,
            
            // This simply means that we are not connected to anything.
            Disconnected,
            
            // As the names states, there is an error.
            Error,
            
            // Loading means that an operation is doing something at the moment.
            Loading,

            // Means that the loginform has accepted the typed credentials.
            LoginAccepted,

            // Means that the settingsform has accepted the specified settings.
            SettingAccepted,

            // States that the connection is about to time out.
            TimeOut
        };

        // Instantiating the programs status.
        private static ProgramStatus programStatus = new ProgramStatus();

        public MailClientForm()
        {
            // Start the programs status with the disconnected state.
            programStatus = ProgramStatus.Disconnected;

            // Call the initializing of the components for the form.
            InitializeComponent();
            
            // Making our static label equal.
            statusLabel = labelStatus;
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
            connectToServer = new ConnectToServer(listBoxMails, labelStatus, textBoxMail.Text, textBoxPassword.Text, progressBarStatus);

            // Populate the listbox with mails.
            PopulateListBox();
        }

        private void PopulateListBox()
        {
            // Instantiate the database.
            mailDatabase = new MailDatabase();

            // Instantiate the date for the latest recieved mail so that it is not null.
            newestMailDateTime = new DateTime();

            // Make sure that we do not duplicate mails in the listbox.
            ComponentChanges.ClearItemFromListBox(listBoxMails);

            // Declare and initialize a variable for the amount of mails in the database.
            int dbMailCount = mailDatabase.ReadMailCount(textBoxMail.Text);

            // Count through the amount of mails that are in the database, if any.
            for (int i = 1; i <= dbMailCount; i++)
            {
                // Retrieve the messages headers from the database and put them into the listbox.
                ComponentChanges.AddItemToListBox(listBoxMails, mailDatabase.ReadMail(textBoxMail.Text, i).Headers.From.Address);
            }

            // Check if any mails was found in the database.
            if (dbMailCount > 0)
            {
                // Retrieve the datetime for the latest mail.
                newestMailDateTime = mailDatabase.ReadMail(textBoxMail.Text, dbMailCount).Headers.DateSent;
            }
            else
                // Retrieve all the mails from the server if nothing is in the database.
                connectToServer.RetrieveAllMailsFromServer(listBoxMails);
        }

        private void labelStatus_ForeColorChanged(object sender, EventArgs e)
        {
            // Check if the programs status is either login- or settingsaccepted.
            if (programStatus == ProgramStatus.LoginAccepted || programStatus == ProgramStatus.SettingAccepted)
            {
                // Create a thread delegate which matches the function: "StartConnection".
                Thread threadMailClient = new Thread(StartConnection);

                // Tell the thread to run in the background so that is stops when the main thread is stopped.
                threadMailClient.IsBackground = true;

                // Start the thread.
                threadMailClient.Start();
            }
            // Check if the programs status is in error.
            else if (programStatus == ProgramStatus.Error)
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
            // Check if the programs status is at timeout.
            else if (programStatus == ProgramStatus.TimeOut)
            {
                // Declare and initialize a variable for the amount of mails in the database.
                int dbMailCount = mailDatabase.ReadMailCount(textBoxMail.Text);

                // Retrieve the datetime for the latest mail.
                newestMailDateTime = mailDatabase.ReadMail(textBoxMail.Text, dbMailCount).Headers.DateSent;

                // Check if any new mails is present on the server.
                connectToServer.RefreshConnection(newestMailDateTime);

                // Reset the timeout counter again.
                timeOutCounter = 0;
            }

            // Make sure that the timeoutcounter is reset once in a while.
            if (programStatus != ProgramStatus.TimeOut)
                // Reset the timeout counter again.
                timeOutCounter = 0;
        }

        private void timerWaiting_Tick(object sender, EventArgs e)
        {
            // Check if the progressbars value is at its max value and if the status labels texts color is in blue state.
            if (progressBarStatus.Value == 100 && labelStatus.ForeColor == Color.Blue)
                // Set the progressbars value to zero.
                progressBarStatus.Value = 0;

            // Check if the status labels texts color is in blue state.
            if (labelStatus.ForeColor == Color.Blue)
                // Increment the current progressbars value with one.
                progressBarStatus.Value++;

            // Check the value of the timeout counter while also incrementing it with one for every tick.
            // Also make sure that the programs status is in connected state.
            if (timeOutCounter++ >= 250 && programStatus == ProgramStatus.Connected)
                // Change the programs status to state that the connection has timed out.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.TimeOut);
        }

        private void listBoxMails_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if the value should be ignored, in this case if it is below zero.
            if (listBoxMails.SelectedIndex < 0)
                return;

            // Retrieve the selected mail from the database.
            mailMessage = mailDatabase.ReadMail(textBoxMail.Text, listBoxMails.Items.Count - listBoxMails.SelectedIndex);

            // Check if an html format is present.
            if (mailMessage.FindFirstHtmlVersion() != null)
                // Show the html format in the webbrowser.
                webBrowserView.DocumentText = mailMessage.FindFirstHtmlVersion().GetBodyAsText();  
            // Check if an text format is present.
            else if (mailMessage.FindFirstPlainTextVersion() != null)
                // Show the text format in the webbrowser.
                webBrowserView.DocumentText = mailMessage.FindFirstPlainTextVersion().GetBodyAsText();
            else
                // If nothing is found then output that something is wrong.
                webBrowserView.DocumentText = "Problems showing this mail, Contact Simon :-)";
        }

        public static void SetProgramStatus(ProgramStatus programStatus)
        {
            // Change the programs status to the specified status value.
            MailClientForm.programStatus = programStatus;

            // Change the labels texts color according to the programs status.
            // This is done not only for some effects on the form but also to trigger an event.
            if (programStatus == ProgramStatus.LoginAccepted || programStatus == ProgramStatus.SettingAccepted)
                statusLabel.ForeColor = Color.CornflowerBlue;
            else if (programStatus == ProgramStatus.Error || programStatus == ProgramStatus.Disconnected)
                statusLabel.ForeColor = Color.Red;
            else if (programStatus == ProgramStatus.ConnectedToServer || programStatus == ProgramStatus.Connected)
                statusLabel.ForeColor = Color.Green;
            else if (programStatus == ProgramStatus.TimeOut)
                statusLabel.ForeColor = Color.Yellow;
            else if (programStatus == ProgramStatus.Loading)
                statusLabel.ForeColor = Color.Blue;
        }

        private void webBrowserView_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            // Check if the html message contains an embedded image by looking for the "cid" somewhere in the html format.
            if (webBrowserView.DocumentText.Contains("cid"))
            {
                // Go through each element that has the tagname "img".
                for (int i = 0; i < webBrowserView.Document.Body.GetElementsByTagName("img").Count; i++)
                {
                            // Retrieve the mediatype of the attachment.
                    string  mediaType = mailMessage.FindAllAttachments()[i].ContentType.MediaType,
                            // Convert the attachment to a string for the html format.
                            convertedValue = Convert.ToBase64String(mailMessage.FindAllAttachments()[i].Body);

                    // Change the attribute of the where the image is to be shown.
                    webBrowserView.Document.Body.GetElementsByTagName("img")[i].SetAttribute("src", "data:" + mediaType + ";base64," + convertedValue);
                }
            }
        }

        private void buttonNewMail_Click(object sender, EventArgs e)
        {
            // Instantiate the sendmailform.
            sendMailForm = new SendMailForm();

            // Show the sendmailform as a dialog, so the user cannot access the other form.
            sendMailForm.ShowDialog();
        }
    }
}
