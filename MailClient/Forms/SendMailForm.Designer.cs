namespace MailClient
{
    partial class SendMailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelFrom = new System.Windows.Forms.Label();
            this.textBoxFrom = new System.Windows.Forms.TextBox();
            this.labelTo = new System.Windows.Forms.Label();
            this.textBoxTo = new System.Windows.Forms.TextBox();
            this.labelSubject = new System.Windows.Forms.Label();
            this.textBoxSubject = new System.Windows.Forms.TextBox();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.checkBoxEncrypted = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFrom.Location = new System.Drawing.Point(9, 10);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(65, 24);
            this.labelFrom.TabIndex = 3;
            this.labelFrom.Text = "From:";
            // 
            // textBoxFrom
            // 
            this.textBoxFrom.Enabled = false;
            this.textBoxFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxFrom.Location = new System.Drawing.Point(101, 12);
            this.textBoxFrom.Name = "textBoxFrom";
            this.textBoxFrom.Size = new System.Drawing.Size(294, 22);
            this.textBoxFrom.TabIndex = 2;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTo.Location = new System.Drawing.Point(9, 38);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(41, 24);
            this.labelTo.TabIndex = 5;
            this.labelTo.Text = "To:";
            // 
            // textBoxTo
            // 
            this.textBoxTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxTo.Location = new System.Drawing.Point(101, 40);
            this.textBoxTo.Name = "textBoxTo";
            this.textBoxTo.Size = new System.Drawing.Size(294, 22);
            this.textBoxTo.TabIndex = 4;
            // 
            // labelSubject
            // 
            this.labelSubject.AutoSize = true;
            this.labelSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSubject.Location = new System.Drawing.Point(9, 66);
            this.labelSubject.Name = "labelSubject";
            this.labelSubject.Size = new System.Drawing.Size(86, 24);
            this.labelSubject.TabIndex = 7;
            this.labelSubject.Text = "Subject:";
            // 
            // textBoxSubject
            // 
            this.textBoxSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSubject.Location = new System.Drawing.Point(101, 68);
            this.textBoxSubject.Name = "textBoxSubject";
            this.textBoxSubject.Size = new System.Drawing.Size(294, 22);
            this.textBoxSubject.TabIndex = 6;
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Location = new System.Drawing.Point(13, 96);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(481, 441);
            this.textBoxMessage.TabIndex = 8;
            // 
            // buttonSend
            // 
            this.buttonSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSend.Location = new System.Drawing.Point(401, 11);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(93, 51);
            this.buttonSend.TabIndex = 9;
            this.buttonSend.Text = "SEND";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // checkBoxEncrypted
            // 
            this.checkBoxEncrypted.AutoSize = true;
            this.checkBoxEncrypted.Location = new System.Drawing.Point(402, 66);
            this.checkBoxEncrypted.Name = "checkBoxEncrypted";
            this.checkBoxEncrypted.Size = new System.Drawing.Size(94, 21);
            this.checkBoxEncrypted.TabIndex = 10;
            this.checkBoxEncrypted.Text = "Encrypted";
            this.checkBoxEncrypted.UseVisualStyleBackColor = true;
            this.checkBoxEncrypted.CheckedChanged += new System.EventHandler(this.checkBoxEncrypted_CheckedChanged);
            // 
            // SendMailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 549);
            this.Controls.Add(this.checkBoxEncrypted);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.labelSubject);
            this.Controls.Add(this.textBoxSubject);
            this.Controls.Add(this.labelTo);
            this.Controls.Add(this.textBoxTo);
            this.Controls.Add(this.labelFrom);
            this.Controls.Add(this.textBoxFrom);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SendMailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SendMailForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.TextBox textBoxFrom;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.TextBox textBoxTo;
        private System.Windows.Forms.Label labelSubject;
        private System.Windows.Forms.TextBox textBoxSubject;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.CheckBox checkBoxEncrypted;
    }
}