using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Wrap the starting sequence into a try/catch, so that the program doesn't throw an exception.
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MailClientForm());
            }
            catch (Exception e)
            {
                // Output the error message to the user in a messagebox.
                MessageBox.Show(e.Message);
            }
        }
    }
}
