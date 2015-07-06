using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace DeviceWebVarna
{
    /// <summary>
    /// DeviceNet, Varna, Client
    /// </summary>
    public class Client
    {

        #region Constants

        /// <summary>
        /// Service URL.
        /// </summary>
        private const string ServiceSendDataUrl = @"http://devicewebvarnaservice.azurewebsites.net/RestService/SimpleWriteSingleValueDeviceData";

        #endregion

        #region Variables

        /// <summary>
        /// E-mails user.
        /// </summary>
        private string userName = "";//Registered user name on devicewebvarna.azurewebsites.net

        /// <summary>
        /// Sensor ID.
        /// </summary>
        private string sensorIdentifier = "";//Your device identifier

        /// <summary>
        /// Sensor secret.
        /// </summary>
        private string sensorSecret = "";//Your device secret

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userName">This is your e-mail.</param>
        /// <param name="sensorIdentifier">ID</param>
        /// <param name="sensorSecret">Secret</param>
        public Client(string userName, string sensorIdentifier, string sensorSecret)
        {
            this.userName = userName;
            this.sensorIdentifier = sensorIdentifier;
            this.sensorSecret = sensorSecret;
        }

        #endregion

        #region Public

        /// <summary>
        /// Send data to the service.
        /// </summary>
        /// <param name="value">Sensor value.</param>
        /// <returns>True, if the tranaction is succesfull.</returns>
        public bool Send(double value)
        {
            List<DeviceDataItem> values = new List<DeviceDataItem>();

            DeviceDataItem dataItem = new DeviceDataItem()
            {
                Timestamp = DateTime.UtcNow,
                Value = value
            };

            values.Add(dataItem);

            return SendSensorData(values);
        }

        #endregion

        #region Private

        private bool SendSensorData(List<DeviceDataItem> values)
        {
            bool result = false;

            try
            {
                //Build data using this example format "UserName;SensorId;SensorSecret;Value;Time;Value;Time;Value;Time;Value;Time;Value;Time;"
                StringBuilder sensorData = new StringBuilder();
                sensorData.Append(userName);
                sensorData.Append(";");
                sensorData.Append(sensorIdentifier);
                sensorData.Append(";");
                sensorData.Append(sensorSecret);
                sensorData.Append(";");


                foreach (DeviceDataItem sensorValue in values)
                {
                    sensorData.Append(sensorValue.Value.ToString());
                    sensorData.Append(";");
                    //sensorData.Append(sensorValue.Timestamp.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));//    Date and Time with Milliseconds: 07/16/2008 08:32:45.126 AM
                    sensorData.Append(sensorValue.Timestamp.ToString("O"));//    Date and Time with Milliseconds: 07/16/2008 08:32:45.126 AM
                    sensorData.Append(";");
                }

                //Command data content is xml format
                StringBuilder requestData = new StringBuilder();
                requestData.Append("<string xmlns=\"http://schemas.microsoft.com/2003/10/Serialization/\">");
                requestData.Append(sensorData.ToString());
                requestData.Append("</string>");

                //Send data using http POST
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceSendDataUrl);
                request.Method = "POST";
                request.ContentType = "text/xml;charset=utf-8";
                request.KeepAlive = false;
                request.ProtocolVersion = HttpVersion.Version11;
                byte[] bytes = Encoding.UTF8.GetBytes(requestData.ToString());
                request.ContentLength = bytes.Length;
                Stream stream = request.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();

                //Handle response
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                response.Close();

                result = response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return result;
        }

        #endregion

    }
}
