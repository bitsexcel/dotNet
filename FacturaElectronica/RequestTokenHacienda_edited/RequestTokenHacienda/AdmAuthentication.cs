using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace RequestTokenHacienda
{
    public class AdmAuthentication
    {
        public static readonly string DatamarketAccessUri = "https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token";
        private string clientId;
        private string clientSecret;
        private string request;
        private AdmAccessToken token;
        private Timer accessTokenRenewer;
        //Access token expires every 10 minutes. Renew it every 9 minutes only.
        private const int RefreshTokenDuration = 9;
        public AdmAuthentication(string clientId, string clientSecret)
        {
            this.clientId = clientId;
            this.clientSecret = clientSecret;
            //If clientid or client secret has special characters, encode before sending request
            this.request = string.Format("grant_type=password&client_id=api-stag&username={0}&password={1}", HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            Console.WriteLine(this.request);
            this.token = HttpPost(DatamarketAccessUri, this.request);
           
            //renew the token every specfied minutes
            accessTokenRenewer = new Timer(new TimerCallback(OnTokenExpiredCallback), this, TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
        }
        public AdmAccessToken GetAccessToken()
        {
            Console.WriteLine(this.token.access_token);
            return this.token;
        }
        private void RenewAccessToken()
        {
            AdmAccessToken newAccessToken = HttpPost(DatamarketAccessUri, this.request);
            //swap the new token with old one
            //Note: the swap is thread unsafe
            this.token = newAccessToken;
            Console.WriteLine(string.Format("Renewed token for user: {0} is: {1}", this.clientId, this.token.access_token));
        }
        private void OnTokenExpiredCallback(object stateInfo)
        {
            try
            {
                RenewAccessToken();
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Failed renewing access token. Details: {0}", ex.Message));
            }
            finally
            {
                try
                {
                    accessTokenRenewer.Change(TimeSpan.FromMinutes(RefreshTokenDuration), TimeSpan.FromMilliseconds(-1));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(string.Format("Failed to reschedule the timer to renew access token. Details: {0}", ex.Message));
                }
            }
        }
        private AdmAccessToken HttpPost(string DatamarketAccessUri, string requestDetails)
        {
            //Prepare OAuth request 
            WebRequest webRequest = (System.Net.HttpWebRequest)WebRequest.Create(DatamarketAccessUri);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Method = "POST";
            byte[] bytes = Encoding.UTF8.GetBytes(requestDetails);
            webRequest.ContentLength = bytes.Length;
            using (Stream outputStream = webRequest.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }
            
            //using (WebResponse webResponse = webRequest.GetResponse())
            //{
            try
            {
                WebResponse webResponse = webRequest.GetResponse();
                //var webResponse = (HttpWebResponse)webRequest.GetResponse();
               // DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));

                //JavaScriptSerializer serializer = new JavaScriptSerializer(AdmAccessToken);
                //Get deserialized object from JSON stream
                var responseString = new StreamReader(webResponse.GetResponseStream()).ReadToEnd();
                //Console.WriteLine(responseString);
                
                AdmAccessToken token = JsonConvert.DeserializeObject<AdmAccessToken>(responseString);
                this.token = token;
                JsonConvert.PopulateObject(responseString, token);
                //AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());
                Console.WriteLine("access_token: " +token.access_token);
                Console.WriteLine("refresh_token: " + token.refresh_token);
                //return token;

            }
            catch (System.Net.WebException wex)
            {
                Console.Write("Fallo en WebResponse: " + wex);
            }
            return token;

        }
        }
    }


