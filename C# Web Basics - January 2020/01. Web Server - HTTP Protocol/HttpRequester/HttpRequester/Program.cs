using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequester
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string NewLine = "\r\n";

            // Console.WriteLine(IPAddress.Loopback);
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1234); // 80 default
            tcpListener.Start();
           
            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                using NetworkStream networkStream = tcpClient.GetStream();

                byte[] requestBytes = new byte[1000000]; // TODO: Use buffer
                int bytesRead = networkStream.Read(requestBytes, 0, requestBytes.Length);
                string request =  Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
                // Console.WriteLine(request);

                string responseText = @"<form action='/Account/Login' method='post'>
                                        <input type=date name='date' />
                                        <input type=text name='username' />
                                        <input type=password name='pasword' />
                                        <input type=submit value='Login' />
                                        </form>";

                string response = "HTTP/1.0 200 OK" + NewLine +
                                  "Server: SoftUniServer/1.0" + NewLine +
                                  "Content-Type: text/html" + NewLine +
                                  // "Location: https://google.com" + NewLine +
                                  // "Content-Disposition: attachment; filename=pipi.html" + NewLine +
                                  "Content-Lenght: " + responseText.Length + NewLine +
                                  NewLine +
                                  responseText; 

                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                networkStream.Write(responseBytes, 0, responseBytes.Length);
            }
        }

        public static async Task HttpRequest()
        {
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Add("User-Agent", "MyConsoleBrowser/1.0");
            HttpResponseMessage response = await client.GetAsync("https://softuni.bg/");
            //Console.WriteLine(response.Content);
            string result = await response.Content.ReadAsStringAsync();
            //  Console.WriteLine(result);
            File.WriteAllText("index.html", result);
        }
    }
}
