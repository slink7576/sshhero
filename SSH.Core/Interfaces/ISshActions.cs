using Renci.SshNet;
using SSH.Core.Commands;
using SSH.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSH.Core.Interfaces
{
    public interface ISSHActions
    {
        GetProcessesCommandResponse GetProcesses();
        GetSystemInfoCommandResponse GetInfo();
        CheckConnectionCommandResponse CheckConnection();
        BaseCommandResponse Reboot();
        BaseCommandResponse KillProcess(int id);
        ExecuteCustomCommandResponse Execute(string command, bool isSudo);
        GetFilesCommandResponse GetFiles(string path);
        DeleteObjectCommandResponse Delete(string path, string name);
        CreateObjectCommandResponse Create(string path, string name, bool isFile);
        CopyObjectCommandResponse Copy(string from, string to, string file);
        CutObjectCommandResponse Cut(string from, string to, string file);
        GetFileBodyCommandResponse GetFile(string path);
        BaseCommandResponse WriteFile(string path, string data);
    }
}
