using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HelloApplicationHttp
{
    class Program
    {
        static void Main(string[] args)
        {

            var request = (HttpWebRequest)WebRequest.Create("https://idp.comprobanteselectronicos.go.cr/auth/realms/rut-stag/protocol/openid-connect/token");

            var postData = "username=cpf-07-0185-0132%40stag.comprobanteselectronicos.go.cr&grant_type=password&password=y%40%2FK%7B_%5BIh%267%3Eao%3D%3DI%3FIw&client_id=api-stag";
            //postData += "";
            var data = Encoding.ASCII.GetBytes(postData);

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
