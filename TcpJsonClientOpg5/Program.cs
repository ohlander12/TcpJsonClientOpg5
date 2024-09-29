using System;
using System.IO;
using System.Net.Sockets;
using System.Text.Json;

class Client
{
    static void Main(string[] args)
    {
        using TcpClient client = new TcpClient("localhost", 8080);
        using NetworkStream ns = client.GetStream();
        using StreamReader reader = new StreamReader(ns);
        using StreamWriter writer = new StreamWriter(ns) { AutoFlush = true };

        while (true)
        {
            Console.WriteLine("Enter method (Random/Add/Subtract)");
            string method = Console.ReadLine();

            if (method.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.Write("Tal1: ");
            if (!int.TryParse(Console.ReadLine(), out int tal1))
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            Console.Write("Tal2: ");
            if (!int.TryParse(Console.ReadLine(), out int tal2))
            {
                Console.WriteLine("Invalid input");
                continue;
            }

            var request = new { Method = method, Tal1 = tal1, Tal2 = tal2 };
            string jsonRequest = JsonSerializer.Serialize(request);
            writer.WriteLine(jsonRequest);

            
            string? jsonResponse = reader.ReadLine();
            if (jsonResponse != null)
            {
                Console.WriteLine("Received response: " + jsonResponse);
            }
        }
    }
}
