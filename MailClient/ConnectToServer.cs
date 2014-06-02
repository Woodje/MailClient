using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    class ConnectToServer
    {
        // Declaring the variable for the Pop3Client.
        private Pop3Client client;

        // Declaring the variable for the status Label.
        private Label status;

        // Declaring the variable for the listbox representing mails.
        private ListBox listBoxMails;

        // Declaring the variables for the usermail and the password.
        private string userMail, password;

        // Declaring the variable for the status progressbar.
        private ProgressBar statusProgressBar;

        // Declaring the variable for the settingsDatabase.
        private SettingsDatabase settingsDatabase;

        // Declaring the variable for the mailDatabase.
        private MailDatabase mailDatabase;

        // Declaring the variable for checking for new mail or not.
        private bool checkForNewMail = false;

        // Declaring and instantiating the variable that holds the pending emails.
        private static List<MailMessage> listEmailsPending = new List<MailMessage>();

        // Declaring the variable for the latest mails datetime.
        private DateTime newestMailDateTime;

        public ConnectToServer(ListBox listBoxMails, Label status, string userMail, string password, ProgressBar statusProgressBar)
        {
            // Make the provided label the status label.
            this.status = status;

            // Initialize the provided usermail and password.
            this.userMail = userMail;
            this.password = password;

            // Fetch the provided listbox.
            this.listBoxMails = listBoxMails;

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
                ComponentChanges.ReplaceLabelText(status, e.Message);

                // Set the programs status to be having an error.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Error);

                // Announce through the status progressbar that we are done with trying to establish the connection.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
        }

        private void InitConnectionToServer()
        {
            // Instantiating the client.
            client = new Pop3Client();

            // Instantiate the database.
            settingsDatabase = new SettingsDatabase();

            // Change the programs status to be in loading state.
            MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Loading);

            // Output that connection to the server is being made.
            ComponentChanges.ReplaceLabelText(status, "Connecting to server");

            // Go through all the user records in the database.
            foreach (SettingsDatabase.UserSettings value in settingsDatabase.ReadUserSettings())
            {
                // Check if the record in the database matches the usermail and password.
                if (userMail == value.userMail && password == value.password)
                {
                    // Start connection to the server with the values from the database.
                    client.Connect(value.receiveServer, value.receivePort, bool.Parse(value.receiveSSL));

                    // Break out of the loop because a match is found.
                    break;
                }
            }

            // Check if the Pop3Client is connected.
            if (client.Connected)
            {
                // Output that connection to the server is established.
                ComponentChanges.ReplaceLabelText(status, "Connected to server");

                // Set the programs status to be connected to the mail server.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.ConnectedToServer);

                // Announce that we are done with the establishment of the connection.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
            else
            {
                // Output that connection to the server is not established.
                ComponentChanges.ReplaceLabelText(status, "Disconnected");

                // Set the programs status to be disconnected.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Disconnected);

                // Announce that we are done with the establishment of the connection.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }

            // Authenticate the user.
            client.Authenticate(userMail, password);

            // Output that autentication is accepted.
            ComponentChanges.ReplaceLabelText(status, "Connected");

            // Set the programs status to be connected.
            MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Connected);

            // Announce that we are done with the establishment of the connection.
            ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);

            // Check if it is time to check for new mail on the server.
            if (checkForNewMail)
                // Check if any new mails is present.
                CheckForNewMail(listBoxMails);

            // Check if any mails is pending for sending.
            if (listEmailsPending.Count > 0)
                // Send the email from the pending list.
                SendPendingEmails();
        }

        public void RefreshConnection(DateTime newestMailDateTime)
        {
            // Fecth the latest mails datetime.
            this.newestMailDateTime = newestMailDateTime;

            // Announce that we are ready to check for new mails.
            checkForNewMail = true;

            // Make the thread delegate which matches the function: "InitConnectionToServer".
            Thread threadConnectionToServer = new Thread(InitConnectionToServer);

            // Tell the thread to run in the background so that is stops when the main thread is stopped.
            threadConnectionToServer.IsBackground = true;

            // Start the thread.
            threadConnectionToServer.Start();
        }

        private void CheckForNewMail(ListBox listBoxMails)
        {
            try
            {
                // Declare and initialize the variables for the amount of new mails, and all mails on the server.
                int newMailCount = 0,
                    mailCount = client.GetMessageCount();

                // Instantiate the database.
                mailDatabase = new MailDatabase();

                // Declare a variable that can hold a mailmessage.
                OpenPop.Mime.Message mailMessage;

                // Go through all of the mails on the mail server.
                for (int i = mailCount; i >= 1; i--)
                    // Check if the current mail is newer than the latest recieved mail.
                    if (Convert.ToDateTime(client.GetMessageHeaders(i).Date).CompareTo(newestMailDateTime) == 1)
                        // For every new mail on the server, increment our variable.
                        newMailCount++;
                    else
                        // Break out of the loop if no "more" new mail is found.
                        break;

                // Check if any new mails were found.
                if (newMailCount > 0)
                    // Go through only the new mails.
                    for (int i = mailCount - newMailCount; i <= mailCount; i++)
                    {
                        // Initialize the mailmessage variable with a mail from the server.
                        mailMessage = client.GetMessage(i);

                        // Add the rawmessage to the database in a string format from bytes.
                        mailDatabase.InsertMail(userMail, Convert.ToBase64String(mailMessage.RawMessage));

                        // Add the "From"-header to the listbox as a representation of the mail.
                        ComponentChanges.AddItemToListBox(listBoxMails, mailMessage.Headers.From.Address);
                    }
            }
            catch (Exception e)
            {
                // Output the error to the user.
                ComponentChanges.ReplaceLabelText(status, e.Message);

                // Set the programs status to be in error.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Error);
            }
        }

        public void RetrieveAllMailsFromServer(ListBox listBoxMails, MailDatabase mailDatabase)
        {
            try
            {
                // Check if a connection to the server is made, if not then return.
                if (!client.Connected)
                    return;

                // Change the programs status to be in loading state.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Loading);

                // Output that current action to the status label.
                ComponentChanges.ReplaceLabelText(status, "Retrieving mails from server");

                // Declare and initialize a variable for the amount of mails on the server.
                int mailCount = client.GetMessageCount();

                // Declare a variable that can hold a mailmessage.
                OpenPop.Mime.Message mailMessage;

                // Go through all of the mails on the mail server.
                for (int i = 1; i <= mailCount; i++)
                {
                    // Check if the message contains a messageid, if it doesn't its most likely a spam mail and we are ignoring it.
                    if (client.GetMessageHeaders(i).MessageId != null)
                    {
                        // Initialize the mailmessage variable with a mail from the server.
                        mailMessage = client.GetMessage(i);

                        // Add the rawmessage to the database in a string format from bytes.
                        mailDatabase.InsertMail(userMail, Convert.ToBase64String(mailMessage.RawMessage));

                        // Add the "From"-header to the listbox as a representation of the mail.
                        ComponentChanges.AddItemToListBox(listBoxMails, mailMessage.Headers.From.Address);
                    }
                }

                // Set the output back to "Connected".
                ComponentChanges.ReplaceLabelText(status, "Connected");

                // Set the programs status to be in connected state again.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Connected);

                // Announce through the status progressbar that we are done with retrieving mails.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
            catch (Exception e)
            {
                // Output the error to the user.
                ComponentChanges.ReplaceLabelText(status, e.Message);

                // Set the programs state to be in error.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Error);

                // Announce through the status progressbar that we are done with trying to retrieve mails.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
        }

        public static void AddMailToSendingList(MailMessage mailMessage)
        {
            // Add the provided mail to the list with pending mails.
            listEmailsPending.Add(mailMessage);
        }

        private void SendPendingEmails()
        {
            try
            {
                // Change the programs status to be in loading state.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Loading);

                // Output that mails are being send.
                ComponentChanges.ReplaceLabelText(status, "Sending mails");

                // Go through all the user records in the database.
                foreach (SettingsDatabase.UserSettings value in settingsDatabase.ReadUserSettings())
                {
                    // Check if the record in the database matches the usermail and password.
                    if (userMail == value.userMail && password == value.password)
                    {
                        // Start connecting the smtpclient with the settings from the database.
                        SmtpClient sendingClient = new SmtpClient(value.sendServer, value.sendPort);

                        // Use the SSL-setting from the database.
                        sendingClient.EnableSsl = bool.Parse(value.sendSSL);

                        // Identify the usermail towards the sending server.
                        sendingClient.Credentials = new NetworkCredential(userMail, password);

                        // Go through the list with pending mails.
                        foreach (MailMessage pendingMail in listEmailsPending)
                            // Send the mail.
                            sendingClient.Send(pendingMail);

                        // After the pending mails have been sent, the the list is cleared again.
                        listEmailsPending.Clear();

                        // Break out of the loop because a match is found.
                        break;
                    }
                }
                // Change the programs status to be in connected state.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Connected);

                // Output that connection to the server again made.
                ComponentChanges.ReplaceLabelText(status, "Connected");

                // Announce through the status progressbar that we are done with trying to establish the connection.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
            catch (Exception e)
            {
                // Output the error to the user.
                ComponentChanges.ReplaceLabelText(status, e.Message);

                // Set the programs status to be having an error.
                MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.Error);

                // Announce through the status progressbar that we are done with trying to establish the connection.
                ComponentChanges.ReplaceProgressBarValue(statusProgressBar, 100);
            }
        }
    }
}
