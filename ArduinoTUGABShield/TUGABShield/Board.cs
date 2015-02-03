using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace TUGABShield
{
    public class Board : IDisposable
    {

        #region Variables

        /// <summary>
        /// 
        /// </summary>
        private int timeOut;

        /// <summary>
        /// Comunication port.
        /// </summary>
        private SerialPort port;

        /// <summary>
        /// Comunication lock object.
        /// </summary>
        private Object requestLock = new Object();

        /// <summary>
        /// TODO: MAke cooment.
        /// </summary>
        private bool isConnected = false;

        private string portName = String.Empty;

        /// <summary>
        /// Delimiting characters.
        /// </summary>
        private char[] delimiterChars = { ' ', ',', '.', ':', '\t' };

        #endregion

        #region Properties

        /// <summary>
        /// Maximum timeout.
        /// </summary>
        public int MaxTimeout { get; set; }

        /// <summary>
        /// If the board is correctly connected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return this.isConnected;
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Rise when error occurred beteween PLC and PC.
        /// </summary>
        public event EventHandler<ErrorEventArgs> Message;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="port">Comunication port.</param>
        public Board(string portName)
        {
            // max tiem 5 minutes
            this.MaxTimeout = 30000;

            // Save the port name.
            this.portName = portName;
        }

        public Board()
        {
            // TODO: Complete member initialization
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Board()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (this.isConnected)
            {
                this.port.Close();
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Connetc to the serial port.
        /// </summary>
        public void Connect()
        {
            while (!this.isConnected)
            {
                try
                {
                    if (!this.isConnected)
                    {
                        this.port = new SerialPort(this.portName);
                        this.port.BaudRate = 19200;
                        this.port.DataBits = 8;
                        this.port.StopBits = StopBits.One;
                        this.port.Parity = Parity.None;
                        this.port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                        this.port.Open();

                        this.timeOut = 0;
                        this.isConnected = true;
                    }
                }
                catch (Exception exception)
                {
                    this.timeOut += 1000;
                    Thread.Sleep(timeOut);
                    this.isConnected = false;
                }
                finally
                {
                    if (timeOut > this.MaxTimeout)
                    {
                        throw new InvalidOperationException("Could not connect to the device.");
                    }
                }
            }
        }

        public void Disconnect()
        {
            this.port.Close();
            this.isConnected = false;
        }

        /// <summary>
        /// Reset the MCU
        /// </summary>
        public void Reset()
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    this.port.DtrEnable = true;
                    Thread.Sleep(200);
                    this.port.DtrEnable = false;
                }
            }
        }

        /// <summary>
        /// Set the LED display value.
        /// </summary>
        /// <param name="value">Value</param>
        public void SetDisplay(int value)
        {
            lock (this.requestLock)
            {
                if (value > 99)
                {
                    value = 99;
                }

                if (value < 0)
                {
                    value = 0;
                }

                if (port.IsOpen)
                {
                    string command = String.Format("?DISPLAY{0:D2}", value);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Turn ON/OFF LEDs.
        /// </summary>
        /// <param name="index">Index of the LED.</param>
        /// <param name="value">Value for the LED.</param>
        public void SetLED(int index, bool value)
        {
            lock (this.requestLock)
            {
                if (index > 3)
                {
                    index = 2;
                }

                if (index < 1)
                {
                    index = 0;
                }

                if (port.IsOpen)
                {
                    string command = String.Format("?LED{0}{1}", index, value ? 1 : 0);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Set the buzzer.
        /// </summary>
        /// <param name="value">Value for the buzzer.</param>
        public void SetBuzzer(bool value)
        {
            lock (this.requestLock)
            {
                if (port.IsOpen)
                {
                    string command = String.Format("?BUZZER{0}", value ? 1 : 0);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Get potentiometer data.
        /// </summary>
        /// <param name="index">Index of the potentiometer.</param>
        public void GetPot(int index)
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    if (index > 2)
                    {
                        index = 2;
                    }

                    if (index < 1)
                    {
                        index = 1;
                    }

                    string command = String.Format("?POT{0}", index);
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Gets the light sensor.
        /// </summary>
        public void GetLightSensor()
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    string command = String.Format("?LIGHT");
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Get the temperature sensor.
        /// </summary>
        public void GetTemperaturSensor()
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    string command = String.Format("?TEMP");
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Get the microphone value.
        /// </summary>
        public void GetMic()
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    string command = String.Format("?MIC");
                    this.SendRequest(command);
                }
            }
        }

        /// <summary>
        /// Get the button state.
        /// </summary>
        /// <param name="index">Number of the button.</param>
        public void GetButton(int index)
        {
            lock (this.requestLock)
            {
                if (this.isConnected)
                {
                    if (index > 3)
                    {
                        index = 3;
                    }

                    if (index < 1)
                    {
                        index = 1;
                    }

                    string command = String.Format("?BUTTON{0}", index);
                    this.SendRequest(command);
                }
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// Data recievce event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            // Wait ...
            Thread.Sleep(150);

            if (sender != null)
            {
                // Make serial port to get data from.
                SerialPort sp = (SerialPort)sender;

                //string indata = sp.ReadLine();
                string inData = sp.ReadExisting();

                //TODO: Parse the incommning string from the serial port.
                // POS will be the index of the array.
                // CM will be the data in the cell.
                //string[] tokens = inData.Split(this.delimiterChars);

                //Console.WriteLine("Data: {0};\r\nTokens: {1}", indata, tokens.Length);

                //Console.WriteLine("Test: {0}", indata);

                if (this.Message != null)
                {
                    this.Message(this, new ErrorEventArgs(new Exception(inData)));
                }

                // Discart the duffer.
                sp.DiscardInBuffer();
            }
        }

        private void SendRequest(string request)
        {
            try
            {
                if (this.isConnected)
                {
                    this.port.WriteLine(request);
                }
            }
            catch (Exception exception)
            {
                this.isConnected = false;
                this.timeOut = 0;
                // Reconnect.
                this.Connect();
            }
        }
        #endregion



    }
}
