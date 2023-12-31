﻿using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Launcher.Procedure
{
    /// <summary>
    /// LauncherはExitCodeを返すべき手続きだったので作成
    /// </summary>
    public interface IExecuter : IProcedure
    {
        int ExitCode { get; set; }
        Task RunExe();
    }
}
