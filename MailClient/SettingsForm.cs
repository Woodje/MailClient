using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class SettingsForm : Form
    {
        // Declaring the variable for the settingsDatabase.
        private SettingsDatabase settingsDatabase;

        // Create a boolean variable to control when the settings is accepted.
        private bool settingsAccepted = false;

        public SettingsForm()
        {
            // Call the initializing of the components for the form.
            InitializeComponent();

            // Retrieve the current setting from the database.
            ReadSettings();
        }

        private void ReadSettings()
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Instantiate the database.
            settingsDatabase = new SettingsDatabase();

            // Go through all the retrieved settings from the database.
            foreach (SettingsDatabase.UserSettings value in settingsDatabase.ReadUserSettings())
            {
                // Find the record which matches the usermail and password.
                if (Application.OpenForms["MailClientForm"].Controls["textBoxMail"].Text == value.userMail && Application.OpenForms["MailClientForm"].Controls["textBoxPassword"].Text == value.password)
                {
                    // Write the retrieved values to the textboxes.
                    textBoxReceiveServer.Text = value.receiveServer;
                    textBoxReceivePort.Text = value.receivePort.ToString();
                    textBoxReceiveSSL.Text = value.receiveSSL;
                    textBoxSendServer.Text = value.sendServer;
                    textBoxSendPort.Text = value.sendPort.ToString();
                    textBoxSendSSL.Text = value.sendSSL;

                    // Break out of the loop because a match is found.
                    break;
                }
            }
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Change the program status to state that we are ready to connect again.
            MailClientForm.SetProgramStatus(MailClientForm.ProgramStatus.SettingAccepted);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Check if text is typed into both textboxes.
            if (textBoxReceiveServer.Text != "" && textBoxReceivePort.Text != "" && textBoxReceiveSSL.Text != "" && textBoxSendServer.Text != "" && textBoxSendPort.Text != "" && textBoxSendSSL.Text != "")
            {
                // Declare a variable for validation use.
                int i;

                // Validate if value is 'NULL'.
                if (textBoxReceiveServer.Text == "NULL" || textBoxSendServer.Text == "NULL")
                {
                    MessageBox.Show("(Server:) may not be 'NULL'");
                }
                // Validate if the typed value is an integer.
                else if (!int.TryParse(textBoxReceivePort.Text, out i) || !int.TryParse(textBoxSendPort.Text, out i))
                {
                    MessageBox.Show("(Port:) needs to be a number!");
                }
                // Validate if the typed value is of true or false.
                else if (!(textBoxReceiveSSL.Text.ToUpper() == "FALSE" || textBoxReceiveSSL.Text.ToUpper() == "TRUE") || !(textBoxSendSSL.Text.ToUpper() == "FALSE" || textBoxSendSSL.Text.ToUpper() == "TRUE"))
                {
                    MessageBox.Show("(SSL:) needs to be either 'true' or 'false'");
                }
                else
                {
                    // Update the settings for the current usermail and password.
                    settingsDatabase.UpdateSettings(Application.OpenForms["MailClientForm"].Controls["textBoxMail"].Text,
                                                Application.OpenForms["MailClientForm"].Controls["textBoxPassword"].Text,
                                                textBoxReceiveServer.Text, int.Parse(textBoxReceivePort.Text), bool.Parse(textBoxReceiveSSL.Text),
                                                textBoxSendServer.Text, int.Parse(textBoxSendPort.Text), bool.Parse(textBoxSendSSL.Text));

                    // Announce that the settings is accepted.
                    settingsAccepted = true;
                }
            }
            else
                // Show a messagebox that tells the user what is missing.
                MessageBox.Show("Please fill out \nall three textboxes!", "Missing values!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // Check if the settings has been accepted and close the form if true.
            if (settingsAccepted)
                Close();
        }
    }
}
