// See https://aka.ms/new-console-template for more information

using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var input = new HelloRequest { Name = "Giovanna" };
            var channel = GrpcChannel.ForAddress("https://localhost:7015");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(input);

            Console.WriteLine(reply.Message);

            Console.ReadLine();
        }
    }
}


