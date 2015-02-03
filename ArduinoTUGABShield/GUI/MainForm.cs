using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

using TUGABShield;

namespace GUI
{
    public partial class MainForm : Form
    {

        #region Variables

        /// <summary>
        /// Pooling timer.
        /// </summary>
        System.Windows.Forms.Timer poolingTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Development board.
        /// </summary>
        private Board myDev;

        /// <summary>
        /// Delimiting characters.
        /// </summary>
        private char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        private string devicePortName = "";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private

        /// <summary>
        /// Form load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SearchForPorts();

            this.myDev = new Board();

            this.poolingTimer.Stop();
            this.poolingTimer.Interval = 5000;
            this.poolingTimer.Tick += poolingTimer_Tick;
        }

        /// <summary>
        /// Pooling event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poolingTimer_Tick(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.GetPot(1);
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetPot(2);
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetLightSensor();
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetTemperaturSensor();
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetMic();
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetButton(1);
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetButton(2);
                Application.DoEvents();
                Thread.Sleep(200);
                this.myDev.GetButton(3);
                Application.DoEvents();
            }
            
        }

        /// <summary>
        /// Event when message is reacieved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void myDev_Message(object sender, System.IO.ErrorEventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                string inData = e.GetException().Message;

                // Determin
                if(inData[0] == '#')
                {
                    if (inData.Contains("BTN1"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblButton1.InvokeRequired)
                        {
                            this.lblButton1.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblButton1.Text = String.Format("Button1: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("BTN2"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblButton2.InvokeRequired)
                        {
                            this.lblButton2.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblButton2.Text = String.Format("Button2: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("BTN3"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblButton3.InvokeRequired)
                        {
                            this.lblButton3.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblButton3.Text = String.Format("Button3: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("POT1"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblPot1.InvokeRequired)
                        {
                            this.lblPot1.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblPot1.Text = String.Format("Pot1: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("POT2"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblPot2.InvokeRequired)
                        {
                            this.lblPot2.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblPot2.Text = String.Format("Pot2: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("LIGHT"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblLight.InvokeRequired)
                        {
                            this.lblLight.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblLight.Text = String.Format("Light: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("TEMP"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblTemp.InvokeRequired)
                        {
                            this.lblTemp.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblTemp.Text = String.Format("Temp: {0}", tokens[1]);
                            });
                        }
                    }

                    else if (inData.Contains("MIC"))
                    {
                        string[] tokens = inData.Split(this.delimiterChars);
                        if (this.lblMic.InvokeRequired)
                        {
                            this.lblMic.BeginInvoke((MethodInvoker)delegate()
                            {
                                this.lblMic.Text = String.Format("Mic: {0}", tokens[1]);
                            });
                        }
                    }
                }

                //Console.Write("{0}", inData);
            }
        }

        /// <summary>
        /// LED Green ON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkGreenLed_Click(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.SetLED(1, this.chkGreenLed.Checked);
            }
        }

        /// <summary>
        /// LED Yello ON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkYellowLed_Click(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.SetLED(2, this.checkBox2.Checked);
            }
        }

        /// <summary>
        /// LED Red ON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkRedLed_Click(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.SetLED(3, this.checkBox3.Checked);
            }
        }

        /// <summary>
        /// Buzzer ON/OFF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkBuzzer_Click(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.SetBuzzer(this.chkBuzzer.Checked);
            }
        }

        /// <summary>
        /// Numeric control for the display value.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nmudDisplay_ValueChanged(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.SetDisplay((int)this.nmudDisplay.Value);
            }
        }

        /// <summary>
        /// Reset the device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            if (this.myDev.IsConnected)
            {
                this.myDev.Reset();
            }
        }

        /// <summary>
        /// Connect to the device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConnect_Click(object sender, EventArgs e)
        {
            this.myDev = new Board(this.devicePortName);
            this.myDev.Message += this.myDev_Message;
            this.myDev.Connect();
            this.btnConnect.Enabled = false;
            this.btnDisconnect.Enabled = true;
            this.poolingTimer.Start();
        }

        /// <summary>
        /// Disconnect from the device.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            this.myDev.Message -= this.myDev_Message;
            this.myDev.Disconnect();
            this.btnConnect.Enabled = true;
            this.btnDisconnect.Enabled = false;
            this.poolingTimer.Stop();
        }

        /// <summary>
        /// Search for ports on the PC.
        /// </summary>
        private void SearchForPorts()
        {
            cmbPortNames.Items.Clear();

            string[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (portNames.Length == 0)
            {
                cmbPortNames.Text = "No Ports";
                return;
            }

            foreach (string item in portNames)
            {
                //store the each retrieved available prot names into the Listbox...
                cmbPortNames.Items.Add(item);
            }

            // 
            cmbPortNames.Text = cmbPortNames.Items[0].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbPortNames_SelectedValueChanged(object sender, EventArgs e)
        {
            this.devicePortName = cmbPortNames.Text;
        }

        #endregion
    }
}
