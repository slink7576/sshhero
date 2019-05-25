using Renci.SshNet;
using SSH.Core.Entities;
using SSH.Core.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace SSH.Core
{
    public class SSHClient : IDisposable, ISshActions
    {
        private readonly SshClient _client;

        public SSHClient(string hostname, string username, string password)
        {
            _client = new SshClient(hostname, username, password);
            _client.Connect();
        }

        public SSHClient(Credentials credentials)
        {
            _client = new SshClient(credentials.Hostname,
                credentials.Login, credentials.Password);
            _client.Connect();
        }


        public SshCommand Execute(string command)
        {
            return _client.RunCommand(command);
        }

        public void Dispose()
        {
            _client.Disconnect();
            _client.Dispose();
        }

        public bool CheckConnection()
        {
            return _client.IsConnected;
        }

        public IEnumerable<ProcessInfo> GetProcesses()
        {
            var command = _client.RunCommand("TERM=xterm ps -eo cmd,%mem,%cpu,pid --sort=-%mem | head");
            var data = Regex.Split(string.Join("",Regex.Split(command.Result, "PID")[1]), "\n");
            var result = data.ToList().Select(line => {
                var items = Regex.Split(line, "  ").Where(elem => elem != "").ToList();
                if(items.Count != 0)
                {
                    return new ProcessInfo()
                    {
                        CPU = Double.Parse(items[2], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                        Memory = Double.Parse(items[1], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                        Name = items[0].Split(' ')[0],
                        Id = Int32.Parse(items[3], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"))
                    };
                }
                return null;
            }).Where(elem => elem != null).ToList();
            return result;
        }

        public SystemInfo GetInfo()
        {
            return new SystemInfo() { Os = _client.RunCommand("uname -v").Result };
        }
    }
}
