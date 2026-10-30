using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Put");
            Console.WriteLine("2. Get");
            string choice = Console.ReadLine() ?? "1";
            TcpClient client = new TcpClient("127.0.0.1", 8888);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            if (choice == "1")
            {
                Console.Write("Key: ");
                string key = Console.ReadLine() ?? "Key";
                Console.Write("value: ");
                string value = Console.ReadLine() ?? "Fatima";
                writer.WriteLine($"PUT {key} {value}");
                string response = reader.ReadLine() ?? "";
                Console.WriteLine($"Server: {response}");
            }
            else if (choice == "2")
            {
                Console.Write("Key: ");
                string key = Console.ReadLine() ?? "";
                writer.WriteLine($"GET {key}");
                string response = reader.ReadLine() ?? "";
                Console.WriteLine($"Server: {response}");
            }
            client.Close();
        }
    }
}

