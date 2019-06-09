using Renci.SshNet;
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
using System.Threading;
using System.Threading.Tasks;

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
            var task = Task.Run(() =>
            {
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
            });
            if (task.Wait(TimeSpan.FromSeconds(2)))
            {
                return new ExecuteCustomCommandResponse()
                {
                    IsError = comm.ExitStatus == 0 ? false : true,
                    Error = comm.Error,
                    Result = comm.Result
                };
                
            }
            else
            {
                return new ExecuteCustomCommandResponse()
                {
                    IsError = true,
                    Error = "Command timeout"
                };
            }        
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

        public GetFilesCommandResponse GetFiles(string path = "")
        {
            SshCommand folderCommand = null;
            SshCommand filesCommand = null;
            var files = new List<FileNode>();
            var folders = new List<FileNode>();
            if (path.Length != 0)
            {
                filesCommand = _client.RunCommand("cd " + '"' + path + '"' + " && ls -p | grep -v /");
                folderCommand = _client.RunCommand("cd " + '"' + path + '"' + " && ls -dm */");
            }
            else
            {
                path = _client.RunCommand("pwd").Result;
                filesCommand = _client.RunCommand("ls -p | grep -v /");
                folderCommand = _client.RunCommand("ls -dm */");
            }
            if (folderCommand.ExitStatus == 0)
            {
                folders = folderCommand.Result.Split(',').Select(element =>
                {
                    var folderName = element[0] == ' ' ? element.Substring(1) : element;
                    folderName = folderName.Contains("\n") ? folderName.Replace("\n", "") : folderName;
                    folderName = folderName.Contains("/") ? folderName.Replace("/", "") : folderName;
                    return new FileNode()
                    {
                        Name = folderName,
                        IsFile = false
                    };
                }).ToList();
            }
            if (filesCommand.ExitStatus == 0)
            {
                files = filesCommand.Result.Split('\n').Where(element => element != "").Select(element =>
                {
                    var fileName = element[0] == ' ' ? element.Substring(1) : element;
                    fileName = fileName.Contains("\n") ? fileName.Replace("\n", "") : fileName;
                    fileName = fileName.Contains("/") ? fileName.Replace("/", "") : fileName;
                    return new FileNode()
                    {
                        Name = fileName,
                        IsFile = true
                    };
                }).ToList();
            }
            return new GetFilesCommandResponse()
            {
                IsError = filesCommand.ExitStatus == 1 &&
                    folderCommand.ExitStatus == 1 ? true : false,
                Error = filesCommand.Error + "\n" + folderCommand.Error,
                CurrentPath = path.Contains("\n") ? path.Replace("\n", "") : path,
                Nodes = folders.Concat(files)
                    .OrderBy(element => element.IsFile).ToList()
            };

        }

        public DeleteObjectCommandResponse Delete(string path, string name)
        {
            var command = _client.RunCommand("rm " + path + "/" + name + " -R");
            return new DeleteObjectCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error,
                Nodes = GetFiles(path).Nodes,
            };
        }

        public CreateObjectCommandResponse Create(string path, string name, bool isFile)
        {
            SshCommand command = null;
            if (isFile)
            {
                command = _client.RunCommand("touch " + path + "/" + name);
            }
            else
            {
                command = _client.RunCommand("mkdir " + path + "/" + name);
            }
            return new CreateObjectCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error,
                Nodes = GetFiles(path).Nodes
            };
        }

        public CopyObjectCommandResponse Copy(string from, string to, string file)
        {
            var command = _client.RunCommand("cp " + '"' + from + "/" + file + '"' + " " + '"' + to + '"');
            return new CopyObjectCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error,
                Nodes = GetFiles(to).Nodes
            };
        }

        public GetFileBodyCommandResponse GetFile(string path)
        {
            var command = _client.RunCommand("cat " + "'" + path + "'");
            if (command.Result.Length < 20000)
            {
                return new GetFileBodyCommandResponse()
                {
                    IsError = command.ExitStatus == 0 ? false : true,
                    Error = command.Error,
                    Data = command.Result
                };
            }
            else
            {
                return new GetFileBodyCommandResponse()
                {
                    IsError = true,
                    Error = "File is too big"
                };
            }
           
        }

        public CutObjectCommandResponse Cut(string from, string to, string file)
        {
            var copyResponse = Copy(from, to, file);
            var deleteResponse = Delete(from, file);
            return new CutObjectCommandResponse()
            {
                IsError = copyResponse.IsError,
                Error = copyResponse.Error,
                Nodes = copyResponse.Nodes
            };
        }

        public BaseCommandResponse WriteFile(string path, string data)
        {
            var command = _client.RunCommand("printf " + '"' + data + '"' + " > " + '"' + path + '"');
            return new BaseCommandResponse()
            {
                IsError = command.ExitStatus == 0 ? false : true,
                Error = command.Error
            };
        }
    }
}
