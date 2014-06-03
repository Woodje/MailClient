namespace MailClient
{
    partial class SettingsForm
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
            this.textBoxReceiveServer = new System.Windows.Forms.TextBox();
            this.labelRecieveServer = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.textBoxReceivePort = new System.Windows.Forms.TextBox();
            this.labelSSL = new System.Windows.Forms.Label();
            this.textBoxReceiveSSL = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSendSSL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxSendPort = new System.Windows.Forms.TextBox();
            this.labelSendServer = new System.Windows.Forms.Label();
            this.textBoxSendServer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxReceiveServer
            // 
            this.textBoxReceiveServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceiveServer.Location = new System.Drawing.Point(194, 10);
            this.textBoxReceiveServer.Name = "textBoxReceiveServer";
            this.textBoxReceiveServer.Size = new System.Drawing.Size(258, 22);
            this.textBoxReceiveServer.TabIndex = 0;
            // 
            // labelRecieveServer
            // 
            this.labelRecieveServer.AutoSize = true;
            this.labelRecieveServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelRecieveServer.Location = new System.Drawing.Point(12, 9);
            this.labelRecieveServer.Name = "labelRecieveServer";
            this.labelRecieveServer.Size = new System.Drawing.Size(176, 24);
            this.labelRecieveServer.TabIndex = 2;
            this.labelRecieveServer.Text = "Recieving Server:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPort.Location = new System.Drawing.Point(458, 9);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(53, 24);
            this.labelPort.TabIndex = 4;
            this.labelPort.Text = "Port:";
            // 
            // textBoxReceivePort
            // 
            this.textBoxReceivePort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceivePort.Location = new System.Drawing.Point(517, 10);
            this.textBoxReceivePort.Name = "textBoxReceivePort";
            this.textBoxReceivePort.Size = new System.Drawing.Size(59, 22);
            this.textBoxReceivePort.TabIndex = 3;
            // 
            // labelSSL
            // 
            this.labelSSL.AutoSize = true;
            this.labelSSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSSL.Location = new System.Drawing.Point(582, 9);
            this.labelSSL.Name = "labelSSL";
            this.labelSSL.Size = new System.Drawing.Size(53, 24);
            this.labelSSL.TabIndex = 6;
            this.labelSSL.Text = "SSL:";
            // 
            // textBoxReceiveSSL
            // 
            this.textBoxReceiveSSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxReceiveSSL.Location = new System.Drawing.Point(641, 10);
            this.textBoxReceiveSSL.Name = "textBoxReceiveSSL";
            this.textBoxReceiveSSL.Size = new System.Drawing.Size(59, 22);
            this.textBoxReceiveSSL.TabIndex = 5;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonConnect.Location = new System.Drawing.Point(16, 66);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(684, 53);
            this.buttonConnect.TabIndex = 7;
            this.buttonConnect.Text = "CONNECT";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(582, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "SSL:";
            // 
            // textBoxSendSSL
            // 
            this.textBoxSendSSL.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSendSSL.Location = new System.Drawing.Point(641, 38);
            this.textBoxSendSSL.Name = "textBoxSendSSL";
            this.textBoxSendSSL.Size = new System.Drawing.Size(59, 22);
            this.textBoxSendSSL.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(458, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 24);
            this.label2.TabIndex = 11;
            this.label2.Text = "Port:";
            // 
            // textBoxSendPort
            // 
            this.textBoxSendPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSendPort.Location = new System.Drawing.Point(517, 38);
            this.textBoxSendPort.Name = "textBoxSendPort";
            this.textBoxSendPort.Size = new System.Drawing.Size(59, 22);
            this.textBoxSendPort.TabIndex = 10;
            // 
            // labelSendServer
            // 
            this.labelSendServer.AutoSize = true;
            this.labelSendServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSendServer.Location = new System.Drawing.Point(12, 37);
            this.labelSendServer.Name = "labelSendServer";
            this.labelSendServer.Size = new System.Drawing.Size(161, 24);
            this.labelSendServer.TabIndex = 9;
            this.labelSendServer.Text = "Sending Server:";
            // 
            // textBoxSendServer
            // 
            this.textBoxSendServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSendServer.Location = new System.Drawing.Point(194, 38);
            this.textBoxSendServer.Name = "textBoxSendServer";
            this.textBoxSendServer.Size = new System.Drawing.Size(258, 22);
            this.textBoxSendServer.TabIndex = 8;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 125);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxSendSSL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxSendPort);
            this.Controls.Add(this.labelSendServer);
            this.Controls.Add(this.textBoxSendServer);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.labelSSL);
            this.Controls.Add(this.textBoxReceiveSSL);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.textBoxReceivePort);
            this.Controls.Add(this.labelRecieveServer);
            this.Controls.Add(this.textBoxReceiveServer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SettingsForm - MailClient";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SettingsForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxReceiveServer;
        private System.Windows.Forms.Label labelRecieveServer;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.TextBox textBoxReceivePort;
        private System.Windows.Forms.Label labelSSL;
        private System.Windows.Forms.TextBox textBoxReceiveSSL;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxSendSSL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSendPort;
        private System.Windows.Forms.Label labelSendServer;
        private System.Windows.Forms.TextBox textBoxSendServer;
    }
}