using Renci.SshNet;
using Renci.SshNet.Common;
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
        private readonly Credentials _credentials;

        public SSHClient(string hostname, string username, string password)
        {
            _client = new SshClient(hostname, username, password);
            _credentials = new Credentials() { Hostname = hostname, Login = username, Password = password };
            _client.Connect();
        }

        public SSHClient(Credentials credentials)
        {
            _client = new SshClient(credentials.Hostname,
                credentials.Login, credentials.Password);
            _credentials = credentials;
            _client.Connect();
        }

        public SshCommand Execute(string command, bool isSudo)
        {
            try
            {
                if (isSudo)
                    return _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S " + command);
                else
                    return _client.RunCommand(command);
            }
            catch(SshConnectionException c)
            {
                return _client.CreateCommand(command);
            }
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


          /*  foreach(var line in data.Where(elem => elem != ""))
            {
                var items = Regex.Split(line, "  ").Where(elem => elem != "").ToList();
                var cpu = Double.Parse(items[2], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
                var memory = Double.Parse(items[1], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US"));
            }*/


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

        public bool Reboot()
        {
            using (var cmd = _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S shutdown -r"))
            {
                if (cmd.ExitStatus == 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
