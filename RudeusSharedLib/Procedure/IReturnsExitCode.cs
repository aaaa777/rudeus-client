using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rudeus.Procedure
{
    public interface IReturnsExitCode : IProcedure
    {
        int ExitCode { get; set; }
    }
}
