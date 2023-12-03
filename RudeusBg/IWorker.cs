using Rudeus.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Bg
{
    public interface IWorker
    {
        IProcedure AccessTokenValidator { get; set; }
        IProcedure ScheduledRelularExecuter { get; set; }
        IProcedure UserLoginExecuter { get; set; }
    }
}
