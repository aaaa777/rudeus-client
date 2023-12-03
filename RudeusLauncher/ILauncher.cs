using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Procedure
{
    public interface ILauncher : IProcedure
    {
        int ExitCode { get; set; }
    }
}
