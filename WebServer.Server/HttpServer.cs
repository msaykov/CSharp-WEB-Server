﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WebServer.Server.Http;

namespace WebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener listener;

        public HttpServer(string ipAddress , int port)
        {
            this.ipAddress = IPAddress.Parse("127.0.0.1");
            this.port = port;
            listener = new TcpListener(this.ipAddress, port);
        }

        public async Task Start()
        {
            listener.Start();

            Console.WriteLine($"Server started no port {port}...");
            Console.WriteLine("Listening for requests....");

            while (true)
            {
                var connection = await listener.AcceptTcpClientAsync();
                var networkStream = connection.GetStream();

                var requestText = await ReadRequest(networkStream);
                Console.WriteLine(requestText);

                var request = HttpRequest.Parse(requestText);

                await WriteResponse(networkStream);

                connection.Close();
            }


        }

        private static async Task WriteResponse(NetworkStream networkStream)
        {
            var content = "Hello from the dark side , Миленчук";
            var contentLength = Encoding.UTF8.GetByteCount(content);

            var response = $@"HTTP/1.1 200 OK
Server: My Web Server
Date: {DateTime.UtcNow.ToString("r")}
Content-Length: {contentLength}
content-Type: text/plain; charset=UTF8

{content}";

            var responseBytes = Encoding.UTF8.GetBytes(response);
            await networkStream.WriteAsync(responseBytes);
        }

        private static async Task<string> ReadRequest(NetworkStream networkStream)
        {
            var bufferLength = 1024;
            var totalBytesRead = 0;
            var buffer = new byte[bufferLength];
            var requestBuilder = new StringBuilder();

            while (networkStream.DataAvailable)
            {
                var bytesRead = await networkStream.ReadAsync(buffer, 0, bufferLength);
                totalBytesRead += bytesRead;
                if (totalBytesRead > 10 * 1024)
                {
                    throw new InvalidOperationException("Request is too large.");
                }
                requestBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            }

            return requestBuilder.ToString();
        }
    }
}
