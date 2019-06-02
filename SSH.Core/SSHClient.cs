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
            catch (SshConnectionException c)
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
            var command = _client.RunCommand("TERM=xterm ps -eo pid,%mem,%cpu,cmd");
            var data = Regex.Split(string.Join("", Regex.Split(command.Result, "CMD")[1]), "\n");

            var processes = new List<ProcessInfo>();

            foreach(var row in data.Where(element => element != ""))
            {
                var tags = row.Split(' ').Where(tag => tag.Length != 0).ToList();
                if(tags[3][0] != '[')
                {
                    processes.Add(new ProcessInfo()
                    {
                        Id = Int32.Parse(tags[0], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                        Memory = Double.Parse(tags[1], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                        CPU = Double.Parse(tags[2], NumberStyles.Currency, CultureInfo.GetCultureInfo("en-US")),
                        Name = tags[3]
                    });
                }
            }
            return processes.OrderBy(proc => proc.CPU).Reverse();
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

        public SshCommand KillProcess(int id)
        {
            var cmd = _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S kill " + id);
            return cmd;
        }
    }
}
