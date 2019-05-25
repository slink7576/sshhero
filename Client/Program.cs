using System;
using SSH.Core;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var client = new SSHClient("192.168.1.33", "slink7576", "slinkonline2")){
               
                foreach(var proc in client.GetProcesses())
                {
                    Console.WriteLine(proc.Id + " | " + proc.Name + " " + proc.CPU + " " + proc.Memory);
                }
                Console.WriteLine("Ended");
                Console.ReadKey();
            };
            
        }
    }
}
