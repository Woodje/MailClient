using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MailClient
{
    class ComponentChanges
    {
        private static void replaceLabelText(Label label, string text)
        {
            // Change the labels text.
            label.Text = text;
        }

        private static void replaceLabelForeColor(Label label, Color color)
        {
            // Change the labels texts color.
            label.ForeColor = color;
        }

        private static void replaceTextBoxText(TextBox textBox, string text)
        {
            // Change the textbox's text.
            textBox.Text = text;
        }

        private static void replaceProgressBarValue(ProgressBar progressBar, int value)
        {
            // Change the progressbars value.
            progressBar.Value = value;
        }

        private static void addItemsToListBox(ListBox listBox, string text)
        {
            // Add an item to the listbox.
            listBox.Items.Add(text);
        }

        private static void closeThisForm(Form form)
        {
            // Close the form.
            form.Close();
        }

        public static void changeLabelText(Label label, string text)
        {
            // Check if the label needs to be invoked.
            if (label.InvokeRequired)
                // Invoke the label control with an appropiate delegate.
                label.Invoke(new Action<Label, string>(replaceLabelText), label, text);
            else
                // Directly change the labels text.
                label.Text = text;
        }

        public static void changeLabelForeColor(Label label, Color color)
        {
            // Check if the label needs to be invoked.
            if (label.InvokeRequired)
                // Invoke the label control with an appropiate delegate.
                label.Invoke(new Action<Label, Color>(replaceLabelForeColor), label, color);
            else
                // Directly change the labels texts color.
                label.ForeColor = color;
        }

        public static void changeTextBoxText(TextBox textBox, string text)
        {
            // Check if the textbox needs to be invoked.
            if (textBox.InvokeRequired)
                // Invoke the textbox control with an appropiate delegate.
                textBox.Invoke(new Action<TextBox, string>(replaceTextBoxText), textBox, text);
            else
                // Directly change the textbox's text.
                textBox.Text = text;
        }

        public static void changeProgressBarValue(ProgressBar progressBar, int value)
        {
            // Check if the progressbar needs to be invoked.
            if (progressBar.InvokeRequired)
                // Invoke the progressbar control with an appropiate delegate.
                progressBar.Invoke(new Action<ProgressBar, int>(replaceProgressBarValue), progressBar, value);
            else
                // Directly change the progressbars value.
                progressBar.Value = value;
        }

        public static void changeListBoxItems(ListBox listBox, string text)
        {
            // Check if the listbox need to be invoked.
            if (listBox.InvokeRequired)
                // Invoke the listbox control with the appropiate delegate.
                listBox.Invoke(new Action<ListBox, string>(addItemsToListBox), listBox, text);
            else
                // Directly add the item to the listbox.
                listBox.Items.Add(text);
        }

        public static void closeForm(Form form)
        {
            // Check if the form need to be invoked.
            if (form.InvokeRequired)
                // Invoke the form with the appropiate delegate.
                form.Invoke(new Action<Form>(closeThisForm), form);
            else
                // Directly close the form.
                form.Close();
        }
    }
}
