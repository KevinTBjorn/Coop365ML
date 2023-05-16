using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Cooop365ML.Models;

namespace Cooop365ML.Services
{
    public class RoboflowService
    {
        public static string GetPrediction(string picture)
        {
            byte[] imageArray = System.IO.File.ReadAllBytes(@"YOUR_IMAGE.jpg");
            string encoded = Convert.ToBase64String(imageArray);
            byte[] data = Encoding.ASCII.GetBytes(encoded);
            string api_key = "ZYKSxtBW9niTQwA5Gpio"; // Your API Key
            string model_endpoint = "fruits_detector-w7cxo/1"; // Set model endpoint

            // Construct the URL
            string uploadURL =
                    "https://detect.roboflow.com/"
                    + model_endpoint
                    + "?api_key="
                    + api_key
                    + $"&name={picture}";

            // Service Request Config
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Configure Request
            WebRequest request = WebRequest.Create(uploadURL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            // Write Data
            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            // Get Response
            string responseContent = null;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader sr99 = new StreamReader(stream))
                    {
                        responseContent = sr99.ReadToEnd();
                    }
                }
            }

            return responseContent;
        }
    }
}
