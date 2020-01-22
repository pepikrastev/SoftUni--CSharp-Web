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
            // Console.WriteLine(IPAddress.Loopback);
            TcpListener tcpListener = new TcpListener(IPAddress.Loopback, 1234); // 80 default
            tcpListener.Start();
           
            while (true)
            {
                TcpClient tcpClient = await tcpListener.AcceptTcpClientAsync();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => ProcessClientAsync(tcpClient));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private static async Task ProcessClientAsync(TcpClient tcpClient)
        {
            const string NewLine = "\r\n";
            using NetworkStream networkStream = tcpClient.GetStream();
            byte[] requestBytes = new byte[1000000]; // TODO: Use buffer

        //  int bytesRead =  networkStream.Read(requestBytes, 0, requestBytes.Length);
            int bytesRead = await networkStream.ReadAsync(requestBytes, 0, requestBytes.Length);

            string request = Encoding.UTF8.GetString(requestBytes, 0, bytesRead);
            // Console.WriteLine(request);
            string responseText = @"<form action='/Account/Login' method='post'>
                                        <input type=date name='date' />
                                        <input type=text name='username' />
                                        <input type=password name='pasword' />
                                        <input type=submit value='Login' />
                                        </form>" + "<h1>" + DateTime.UtcNow + "</h1>";

            string response = "HTTP/1.0 200 OK" + NewLine +
                              "Server: SoftUniServer/1.0" + NewLine +
                              "Content-Type: text/html" + NewLine +
                              // "Location: https://google.com" + NewLine +
                              // "Content-Disposition: attachment; filename=pipi.html" + NewLine +
                              "Content-Lenght: " + responseText.Length + NewLine +
                              NewLine +
                              responseText;
            // Console.WriteLine(response);

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

           //    networkStream.Write(responseBytes, 0, responseBytes.Length);
           await networkStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            Console.WriteLine(request);
        }

    }
}
