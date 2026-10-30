using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
class Program
{
    static Dictionary<string, string> cache = new Dictionary<string, string>();
    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
        listener.Start();
        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8) { AutoFlush = true };

            string request = reader.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(request))
            {
                string[] Part = request.Split(' ');
                string command = Part[0];
                if (command == "PUT")
                {
                    if (Part.Length >= 3)
                    {
                        string key = Part[1];
                        string val = Part[2];
                        cache[key] = val;
                        writer.WriteLine("Successfully added to cache.");
                    }
                    else
                    {
                        writer.WriteLine("Error: Missing key or value for PUT command");
                    }
                }
                else if (command == "GET")
                {
                    if (Part.Length >= 2)
                    {
                        string key = Part[1];
                        if (cache.ContainsKey(key))
                        {
                            writer.WriteLine(cache[key]);
                        }
                        else
                        {
                            writer.WriteLine("Error: Key not found in cache");
                        }
                    }
                    else
                    {
                        writer.WriteLine("Error: Missing key for GET command");
                    }
                }
                else
                {
                    writer.WriteLine("Error: Just select from the given commands");
                }
            }
            else
            {
                writer.WriteLine("Error: Empty request received");
            }

            client.Close();
        }
    }
}

