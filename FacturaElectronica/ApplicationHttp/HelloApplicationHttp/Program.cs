using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ApplicationHttp
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var request = (HttpWebRequest)WebRequest.Create("https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token");

            // var postData = "username=cpf-07-0185-0132%40stag.comprobanteselectronicos.go.cr&grant_type=password&password=y%40%2FK%7B_%5BIh%267%3Eao%3D%3DI%3FIw&client_id=api-stag";
            var username = HttpUtility.UrlEncode("cpf-07-0185-0132@stag.comprobanteselectronicos.go.cr");
            // var susername = Encoding.UTF8.GetString(username);
            //var grant_type =  HttpUtility.UrlEncode("cpf-07-0185-0132@stag.comprobanteselectronicos.go.cr");
            var password =  HttpUtility.UrlEncode("@.?9_.mH^f4WP+w|{1#Y");
            //var spassword = Encoding.UTF8.GetString(password);
            // var client_id = Encoding.UTF8.GetBytes("api-stag");
            // var sclient_id = Encoding.UTF8.GetString(client_id);
            var postData = "username=" + username + "&grant_type=password&password=" + password + "&client_id=api-stag" ;
            //postData += "";
            //var postData = HttpUtility.UrlEncode("username=cpf-07-0185-0132@stag.comprobanteselectronicos.go.cr&grant_type=password&password=y@/K{_[Ih&7>ao==I?Iw&client_id=api-stag");
            Console.WriteLine(postData );
            var data = Encoding.UTF8.GetBytes(postData);
            

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            Console.WriteLine(responseString);
            Console.ReadLine();
        }
    }
}
