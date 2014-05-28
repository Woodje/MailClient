using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    public partial class LoginForm : Form
    {
        // Declaring the variable for the database.
        private Database dbMailClient;

        // Create a boolean variable to control when a login is accepted.
        private bool loginAccepted = false;

        public LoginForm()
        {
            // Call the initializing of the components for the form.
            InitializeComponent();

            // Check if autologin has been checked for a user in the database.
            CheckAutoLogin();
        }

        private void CheckAutoLogin()
        {
            // Instantiate the database.
            dbMailClient = new Database();

            // Go through all the user records in the database.
            foreach (Database.UserInfo value in dbMailClient.readUserInfo())
            {
                // Check if a record contains an autologin set to true.
                if (value.autoLogin == "true")
                {
                    // Fill out the controls on the form with values from the database.
                    textBoxMailAddress.Text = value.userMail;
                    textBoxPassword.Text = value.password;
                    checkBoxSaveCredentials.Checked = true;

                    // Break out of the loop because a match is found.
                    break;
                }
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Check if text is typed into both textboxes.
            if (textBoxMailAddress.Text != "" && textBoxPassword.Text != "")
            {
                // Go through all the user records in the database.
                foreach (Database.UserInfo value in dbMailClient.readUserInfo())
                {
                    // Check if the entered information matches a record in the database.
                    if (textBoxMailAddress.Text.ToUpper() == value.userMail.ToUpper() && textBoxPassword.Text == value.password)
                    {
                        // Transfer the two textboxes values to the other form.
                        ComponentChanges.changeTextBoxText((TextBox)Application.OpenForms["MailClientForm"].Controls["textBoxMail"], textBoxMailAddress.Text);
                        ComponentChanges.changeTextBoxText((TextBox)Application.OpenForms["MailClientForm"].Controls["textBoxPassword"], textBoxPassword.Text);

                        // Update the autologin in the database for this usermail and password.
                        dbMailClient.UpdateAutoLogin(textBoxMailAddress.Text, textBoxPassword.Text, checkBoxSaveCredentials.Checked);

                        // Announce that a login is accepted.
                        loginAccepted = true;
                        
                        // Change the label on the other form to show that we are ready to connect.
                        ComponentChanges.changeLabelForeColor((Label)Application.OpenForms["MailClientForm"].Controls["labelStatus"], Color.Blue);

                        // Break out of the loop because a match is found.
                        break;
                    }
                }

                // Check if a login has been accepted, if not then create a record for the user.
                if (!loginAccepted)
                {
                    // Create a new user record in the database.
                    dbMailClient.CreateUserMail(textBoxMailAddress.Text, textBoxPassword.Text, checkBoxSaveCredentials.Checked);

                    // Transfer the two textboxes values to the other form.
                    ComponentChanges.changeTextBoxText((TextBox)Application.OpenForms["MailClientForm"].Controls["textBoxMail"], textBoxMailAddress.Text);
                    ComponentChanges.changeTextBoxText((TextBox)Application.OpenForms["MailClientForm"].Controls["textBoxPassword"], textBoxPassword.Text);

                    // Announce that a login is accepted.
                    loginAccepted = true;

                    // Change the label on the other form to show that we are ready to connect.
                    ComponentChanges.changeLabelForeColor((Label)Application.OpenForms["MailClientForm"].Controls["labelStatus"], Color.Blue);
                }
            }
            else
                // Show a messagebox that tells the user what is missing.
                MessageBox.Show("Please fill out \nboth textboxes!", "Missing values!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // Check if a login has been accepted and close the form if true.
            if (loginAccepted)
                Close();
        }  
        
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Check if the other form is open and close it if this form is closed.
            // Also check if a login has been accepted.
            if (Application.OpenForms["MailClientForm"] != null && !loginAccepted)
                ComponentChanges.closeForm((Form)Application.OpenForms["MailClientForm"]);
        }
    }   
}
