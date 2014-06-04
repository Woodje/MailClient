using System;
using System.Net.Mail;
using System.Windows.Forms;

namespace MailClient
{
    public partial class SendMailForm : Form
    {
        // Create a boolean variable to control when a mailmessage is accepted.
        private bool mailMessageAccepted = false;

        public SendMailForm()
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Call the initializing of the components for the form.
            InitializeComponent();

            // Fill out the textboxfrom with the users mail.
            textBoxFrom.Text = Application.OpenForms["MailClientForm"].Controls["textBoxMail"].Text;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            // Check if the other form is open, if it is not then close this one.
            if (Application.OpenForms["MailClientForm"] == null)
                Close();

            // Check if text is typed into both textboxes.
            if (textBoxTo.Text != "" && textBoxSubject.Text != "" && textBoxMessage.Text != "")
            {
                ConnectToServer.AddMailToSendingList(new MailMessage(textBoxFrom.Text, textBoxTo.Text, textBoxSubject.Text, textBoxMessage.Text));

                // Announce that a mailmessage is accepted.
                mailMessageAccepted = true;
            }
            else
                // Show a messagebox that tells the user what is missing.
                MessageBox.Show("Please fill out \nall textboxes!", "Missing values!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            // Check if a mailmessage has been accepted and close the form if true.
            if (mailMessageAccepted)
                Close();
        }

        private void checkBoxEncrypted_CheckedChanged(object sender, EventArgs e)
        {
            // Declare and instantiate the encryptiondecryption.
            EncryptionDecryption stringEncrypterDecrypter = new EncryptionDecryption();
            
            // Check if the checkbox is checked.
            if (checkBoxEncrypted.Checked)
            {
                //// Generate a new key for encoding.
                byte[] key = stringEncrypterDecrypter.Key();

                // Encrypt the message.
                textBoxMessage.Text = stringEncrypterDecrypter.EncryptString(textBoxMessage.Text, key);

                // Put the key into the subject textbox.
                textBoxSubject.Text = Convert.ToBase64String(key);
            }
            else
                // Decrypt the message.
                textBoxMessage.Text = stringEncrypterDecrypter.DecryptString(textBoxMessage.Text, Convert.FromBase64String(textBoxSubject.Text));
        }
    }
}
