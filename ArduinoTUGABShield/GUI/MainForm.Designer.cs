namespace GUI
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.chkGreenLed = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.chkBuzzer = new System.Windows.Forms.CheckBox();
            this.nmudDisplay = new System.Windows.Forms.NumericUpDown();
            this.lblButton1 = new System.Windows.Forms.Label();
            this.lblButton2 = new System.Windows.Forms.Label();
            this.lblButton3 = new System.Windows.Forms.Label();
            this.lblPot1 = new System.Windows.Forms.Label();
            this.lblPot2 = new System.Windows.Forms.Label();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblLight = new System.Windows.Forms.Label();
            this.lblMic = new System.Windows.Forms.Label();
            this.gpbPort = new System.Windows.Forms.GroupBox();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbPortNames = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nmudDisplay)).BeginInit();
            this.gpbPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkGreenLed
            // 
            this.chkGreenLed.AutoSize = true;
            this.chkGreenLed.Location = new System.Drawing.Point(119, 61);
            this.chkGreenLed.Name = "chkGreenLed";
            this.chkGreenLed.Size = new System.Drawing.Size(79, 17);
            this.chkGreenLed.TabIndex = 0;
            this.chkGreenLed.Text = "Green LED";
            this.chkGreenLed.UseVisualStyleBackColor = true;
            this.chkGreenLed.Click += new System.EventHandler(this.chkGreenLed_Click);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(119, 84);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(81, 17);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "Yellow LED";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.Click += new System.EventHandler(this.chkYellowLed_Click);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(119, 107);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(70, 17);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "Red LED";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.Click += new System.EventHandler(this.chkRedLed_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(18, 129);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // chkBuzzer
            // 
            this.chkBuzzer.AutoSize = true;
            this.chkBuzzer.Location = new System.Drawing.Point(119, 38);
            this.chkBuzzer.Name = "chkBuzzer";
            this.chkBuzzer.Size = new System.Drawing.Size(58, 17);
            this.chkBuzzer.TabIndex = 4;
            this.chkBuzzer.Text = "Buzzer";
            this.chkBuzzer.UseVisualStyleBackColor = true;
            this.chkBuzzer.Click += new System.EventHandler(this.chkBuzzer_Click);
            // 
            // nmudDisplay
            // 
            this.nmudDisplay.Location = new System.Drawing.Point(119, 12);
            this.nmudDisplay.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nmudDisplay.Name = "nmudDisplay";
            this.nmudDisplay.Size = new System.Drawing.Size(81, 20);
            this.nmudDisplay.TabIndex = 5;
            this.nmudDisplay.ValueChanged += new System.EventHandler(this.nmudDisplay_ValueChanged);
            // 
            // lblButton1
            // 
            this.lblButton1.AutoSize = true;
            this.lblButton1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblButton1.Location = new System.Drawing.Point(237, 39);
            this.lblButton1.Name = "lblButton1";
            this.lblButton1.Size = new System.Drawing.Size(58, 15);
            this.lblButton1.TabIndex = 6;
            this.lblButton1.Text = "Button1: 1";
            // 
            // lblButton2
            // 
            this.lblButton2.AutoSize = true;
            this.lblButton2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblButton2.Location = new System.Drawing.Point(237, 62);
            this.lblButton2.Name = "lblButton2";
            this.lblButton2.Size = new System.Drawing.Size(58, 15);
            this.lblButton2.TabIndex = 7;
            this.lblButton2.Text = "Button2: 1";
            // 
            // lblButton3
            // 
            this.lblButton3.AutoSize = true;
            this.lblButton3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblButton3.Location = new System.Drawing.Point(237, 85);
            this.lblButton3.Name = "lblButton3";
            this.lblButton3.Size = new System.Drawing.Size(58, 15);
            this.lblButton3.TabIndex = 8;
            this.lblButton3.Text = "Button3: 1";
            // 
            // lblPot1
            // 
            this.lblPot1.AutoSize = true;
            this.lblPot1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPot1.Location = new System.Drawing.Point(375, 39);
            this.lblPot1.Name = "lblPot1";
            this.lblPot1.Size = new System.Drawing.Size(43, 15);
            this.lblPot1.TabIndex = 9;
            this.lblPot1.Text = "Pot1: 0";
            // 
            // lblPot2
            // 
            this.lblPot2.AutoSize = true;
            this.lblPot2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPot2.Location = new System.Drawing.Point(375, 62);
            this.lblPot2.Name = "lblPot2";
            this.lblPot2.Size = new System.Drawing.Size(43, 15);
            this.lblPot2.TabIndex = 10;
            this.lblPot2.Text = "Pot2: 0";
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemp.Location = new System.Drawing.Point(313, 39);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(48, 15);
            this.lblTemp.TabIndex = 11;
            this.lblTemp.Text = "Temp: 0";
            // 
            // lblLight
            // 
            this.lblLight.AutoSize = true;
            this.lblLight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblLight.Location = new System.Drawing.Point(313, 62);
            this.lblLight.Name = "lblLight";
            this.lblLight.Size = new System.Drawing.Size(44, 15);
            this.lblLight.TabIndex = 12;
            this.lblLight.Text = "Light: 0";
            // 
            // lblMic
            // 
            this.lblMic.AutoSize = true;
            this.lblMic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblMic.Location = new System.Drawing.Point(313, 85);
            this.lblMic.Name = "lblMic";
            this.lblMic.Size = new System.Drawing.Size(38, 15);
            this.lblMic.TabIndex = 13;
            this.lblMic.Text = "Mic: 0";
            // 
            // gpbPort
            // 
            this.gpbPort.Controls.Add(this.btnDisconnect);
            this.gpbPort.Controls.Add(this.btnConnect);
            this.gpbPort.Controls.Add(this.cmbPortNames);
            this.gpbPort.Location = new System.Drawing.Point(12, 12);
            this.gpbPort.Name = "gpbPort";
            this.gpbPort.Size = new System.Drawing.Size(90, 111);
            this.gpbPort.TabIndex = 17;
            this.gpbPort.TabStop = false;
            this.gpbPort.Text = "Port";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(6, 75);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(75, 23);
            this.btnDisconnect.TabIndex = 19;
            this.btnDisconnect.Text = "Disconnect";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(6, 46);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 18;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbPortNames
            // 
            this.cmbPortNames.FormattingEnabled = true;
            this.cmbPortNames.Location = new System.Drawing.Point(6, 19);
            this.cmbPortNames.Name = "cmbPortNames";
            this.cmbPortNames.Size = new System.Drawing.Size(75, 21);
            this.cmbPortNames.TabIndex = 17;
            this.cmbPortNames.SelectedValueChanged += new System.EventHandler(this.cmbPortNames_SelectedValueChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 395);
            this.Controls.Add(this.gpbPort);
            this.Controls.Add(this.lblMic);
            this.Controls.Add(this.lblLight);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.lblPot2);
            this.Controls.Add(this.lblPot1);
            this.Controls.Add(this.lblButton3);
            this.Controls.Add(this.lblButton2);
            this.Controls.Add(this.lblButton1);
            this.Controls.Add(this.nmudDisplay);
            this.Controls.Add(this.chkBuzzer);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.chkGreenLed);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "Arduino TUGAB Shield";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nmudDisplay)).EndInit();
            this.gpbPort.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkGreenLed;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.CheckBox chkBuzzer;
        private System.Windows.Forms.NumericUpDown nmudDisplay;
        private System.Windows.Forms.Label lblButton1;
        private System.Windows.Forms.Label lblButton2;
        private System.Windows.Forms.Label lblButton3;
        private System.Windows.Forms.Label lblPot1;
        private System.Windows.Forms.Label lblPot2;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblLight;
        private System.Windows.Forms.Label lblMic;
        private System.Windows.Forms.GroupBox gpbPort;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbPortNames;
    }
}

