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
        // Declaring the variable for the database.
        private Database dbMailClient;

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
            dbMailClient = new Database();

            foreach (Database.UserInfo value in dbMailClient.readUserInfo())
            {
                // Find the record which matches the usermail and password.
                if (Application.OpenForms["MailClientForm"].Controls["textBoxMail"].Text == value.userMail && Application.OpenForms["MailClientForm"].Controls["textBoxPassword"].Text == value.password)
                {
                    // Write the retrieved values to the textboxes.
                    textBoxServer.Text = value.server;
                    textBoxPort.Text = value.port.ToString();
                    textBoxSSL.Text = value.ssl;

                    // Break out of the loop because a match is found.
                    break;
                }
            }
        }

        private void SettingsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Change the label on the other form to show that we are ready to connect.
            ComponentChanges.changeLabelForeColor((Label)Application.OpenForms["MailClientForm"].Controls["labelStatus"], Color.Blue);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Check if text is typed into both textboxes.
            if (textBoxServer.Text != "" && textBoxPort.Text != "" && textBoxSSL.Text != "")
            {
                // Declare a variable for validation use.
                int i;

                // Validate if value is 'NULL'.
                if (textBoxServer.Text == "NULL")
                {
                    MessageBox.Show("(Server:) may not be 'NULL'");
                }
                // Validate if the typed value is an integer.
                else if (!int.TryParse(textBoxPort.Text, out i))
                {
                    MessageBox.Show("(Port:) needs to be a number!");
                }
                // Validate if the typed value is of true or false.
                else if (!(textBoxSSL.Text.ToUpper() == "FALSE" || textBoxSSL.Text.ToUpper() == "TRUE"))
                {
                    MessageBox.Show("(SSL:) needs to be either 'true' or 'false'");
                }
                else
                {
                    // Update the settings for the current usermail and password.
                    dbMailClient.UpdateSettings(Application.OpenForms["MailClientForm"].Controls["textBoxMail"].Text,
                                                Application.OpenForms["MailClientForm"].Controls["textBoxPassword"].Text,
                                                textBoxServer.Text, int.Parse(textBoxPort.Text), bool.Parse(textBoxSSL.Text));

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
