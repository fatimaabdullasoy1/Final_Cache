using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using Shared1;
class Program
{
    static Dictionary<string, string> cache = new Dictionary<string, string>();
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("1. Put");
            Console.WriteLine("2. Get");
            Console.WriteLine("0. Exit");
            Console.Write("Choose: ");
            string choice = Console.ReadLine() ?? "1";
            if (choice == "0")
            {
                break;
            }
            if (choice == "1")
            {
                Console.Write("Key: ");
                string key = Console.ReadLine() ?? "Key";
                Console.Write("Value: ");
                string value = Console.ReadLine() ?? "Value";
                string request = MessageHelper.FormatMessage("PUT", key, value);
                string response = SendRequest(request);
                Console.WriteLine($"Server: {request}");
                if (response == "Successfully added to cache.")
                {
                    cache[key] = value;
                }
            }
            else if (choice == "2")
            {
                Console.Write("Key: ");
                string key = Console.ReadLine() ?? "Key";
                if (cache.ContainsKey(key))
                {
                    Console.WriteLine($"Cache: cache[key]");
                }
                else
                {
                    string request = MessageHelper.FormatMessage("GET", key);
                    string response = SendRequest(request);
                    Console.WriteLine($"Server: {request}");
                    cache[key] = response;
                }
            }
        }
    }
    static string SendRequest(string request)
    {
        try
        {
            using TcpClient client = new TcpClient("127.0.0.1", 8888);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };
            writer.WriteLine(request);
            return reader.ReadLine() ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return "";
        }
    }
}

