﻿using Renci.SshNet;
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
        RebootCommandResponse Reboot();
        KillProcessCommandResponse KillProcess(int id);
        ExecuteCustomCommandResponse Execute(string command, bool isSudo);
        GetFilesCommandResponse GetFiles(string path);
        DeleteObjectCommandResponse Delete(string path, string name);
        CreateObjectCommandResponse Create(string path, string name, bool isFile);
        CopyObjectCommandResponse Copy(string from, string to);
        GetFileBodyCommandResponse GetFile(string file);
    }
}
