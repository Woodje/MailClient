using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace MailClient
{
    class ComponentChanges
    {
        public static void ReplaceLabelText(Label label, string text)
        {
            // Check if the label needs to be invoked.
            if (label.InvokeRequired)
                // Invoke the label control with an appropiate delegate.
                label.Invoke(new Action<Label, string>(ReplaceLabelText), label, text);
            else
                // Directly change the labels text.
                label.Text = text;
        }

        public static void ReplaceLabelForeColor(Label label, Color color)
        {
            // Check if the label needs to be invoked.
            if (label.InvokeRequired)
                // Invoke the label control with an appropiate delegate.
                label.Invoke(new Action<Label, Color>(ReplaceLabelForeColor), label, color);
            else
                // Directly change the labels texts color.
                label.ForeColor = color;
        }

        public static void ReplaceTextBoxText(TextBox textBox, string text)
        {
            // Check if the textbox needs to be invoked.
            if (textBox.InvokeRequired)
                // Invoke the textbox control with an appropiate delegate.
                textBox.Invoke(new Action<TextBox, string>(ReplaceTextBoxText), textBox, text);
            else
                // Directly change the textbox's text.
                textBox.Text = text;
        }

        public static void ReplaceProgressBarValue(ProgressBar progressBar, int value)
        {
            // Check if the progressbar needs to be invoked.
            if (progressBar.InvokeRequired)
                // Invoke the progressbar control with an appropiate delegate.
                progressBar.Invoke(new Action<ProgressBar, int>(ReplaceProgressBarValue), progressBar, value);
            else
                // Directly change the progressbars value.
                progressBar.Value = value;
        }

        public static void AddItemToListBox(ListBox listBox, string text)
        {
            // Check if the listbox need to be invoked.
            if (listBox.InvokeRequired)
                // Invoke the listbox control with the appropiate delegate.
                listBox.Invoke(new Action<ListBox, string>(AddItemToListBox), listBox, text);
            else
            {
                // Declare and instantiate a list for the listboxes item in the form of strings.
                List<string> listListBox = new List<string>();

                // Add the newest string to the newly created list, so that this is on top.
                listListBox.Add(text);

                // Add all of the current items to the list.
                foreach (string item in listBox.Items)
                    listListBox.Add(item);

                // Clear the listbox of all of its items.
                listBox.Items.Clear();

                // Add the newly created list to the listbox.
                foreach (string item in listListBox)
                    listBox.Items.Add(item);
            }
        }

        public static void ClearItemFromListBox(ListBox listBox)
        {
            // Check if the listbox need to be invoked.
            if (listBox.InvokeRequired)
                // Invoke the listbox control with the appropiate delegate.
                listBox.Invoke(new Action<ListBox>(ClearItemFromListBox), listBox);
            else
                // Directly clear the listbox.
                listBox.Items.Clear();
        }

        public static void CloseForm(Form form)
        {
            // Check if the form need to be invoked.
            if (form.InvokeRequired)
                // Invoke the form with the appropiate delegate.
                form.Invoke(new Action<Form>(CloseForm), form);
            else
                // Directly close the form.
                form.Close();
        }
    }
}
