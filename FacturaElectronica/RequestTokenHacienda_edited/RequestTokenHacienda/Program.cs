using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Threading;
using System.Xml;

namespace RequestTokenHacienda
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            AdmAccessToken admToken;
            string headerValue;
            AdmAuthentication admAuth = new AdmAuthentication("cpf-07-0185-0132@stag.comprobanteselectronicos.go.cr", "*s_jD{q$S;.@/HP}C}iX");
            try
            {
                admToken = admAuth.GetAccessToken();
                Console.WriteLine("desde main: " +admToken.access_token);
                headerValue = "Bearer " + admToken.access_token;
                DetectMethod(headerValue);

            }
            catch(WebException e)
            {
                ProcessWebException(e);
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
            }

        }
        private static void DetectMethod(string authToken)
        {
            //Console.WriteLine("Enter Text to detect language:");
            //string textToDetect = Console.ReadLine();
            //Keep appId parameter blank as we are sending access token in authorization header.
            //string uri = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";
            string uri = "https://api.comprobanteselectronicos.go.cr/recepcion-sandbox/v4.3/";
            //string uri = "http://api.microsofttranslator.com/v2/Http.svc/Detect?text=" + textToDetect;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            httpWebRequest.Method = "POST";
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    string languageDetected = (string)dcs.ReadObject(stream);
                    Console.WriteLine(string.Format("Language detected:{0}", languageDetected));
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey(true);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
        private static void ProcessWebException(WebException e)
        {
            Console.WriteLine("{0}", e.ToString());
            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.UTF8))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            Console.WriteLine("Http status code={0}, error message={1}", e.Status, strResponse);
        }
    }
    
    
}