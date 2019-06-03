﻿using Renci.SshNet;
using Renci.SshNet.Common;
using SSH.Core.Commands;
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
    public class SSHClient : IDisposable, ISSHActions
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

        public void Dispose()
        {
            _client.Disconnect();
            _client.Dispose();
        }

        public ExecuteCustomCommandResponse Execute(string command, bool isSudo)
        {
            SshCommand comm = null;
            if (isSudo)
            {
                comm = _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S " + command);
            }
            else
            {
                comm = _client.RunCommand(command);
            }
            return new ExecuteCustomCommandResponse()
            {
                IsError = comm.ExitStatus == 0 ? false : true,
                Error = comm.Error,
                Result = comm.Result
            };
        }      

        public CheckConnectionCommandResponse CheckConnection()
        {
            var command = _client.RunCommand("echo 1");
            return new CheckConnectionCommandResponse()
            {
                IsConnected = command.ExitStatus == 0 ? true : false,
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error
            };
        }

        public GetProcessesCommandResponse GetProcesses()
        {
            var command = _client.RunCommand("TERM=xterm ps -eo pid,%mem,%cpu,cmd");
            var data = Regex.Split(string.Join("", Regex.Split(command.Result, "CMD")[1]), "\n");

            var processes = new List<ProcessInfo>();

            foreach (var row in data.Where(element => element != ""))
            {
                var tags = row.Split(' ').Where(tag => tag.Length != 0).ToList();
                if (tags[3][0] != '[')
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
            return new GetProcessesCommandResponse()
            {
                Processes = processes.OrderBy(proc => proc.CPU).Reverse(),
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error
            };
        }

        public GetSystemInfoCommandResponse GetInfo()
        {
            var command = _client.RunCommand("uname -v");
            return new GetSystemInfoCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error,
                Information = new SystemInfo()
                {
                    Os = command.Result
                }
            };
        }

        public RebootCommandResponse Reboot()
        {
            var command = _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S shutdown -r");
            return new RebootCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error
            };
        }

        public KillProcessCommandResponse KillProcess(int id)
        {
            var command = _client.RunCommand("echo -e '" + _credentials.Password + "' | sudo -S kill " + id);
            return new KillProcessCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error
            };
        }

        public GetFilesCommandResponse GetFiles(string path)
        {
            throw new NotImplementedException();
        }
    }
}
