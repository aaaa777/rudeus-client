using Rudeus;
using Rudeus.API;
using Rudeus.API.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{ 
    internal class ServerRegister : IProcedure
    {
        public void Run()
        {
            Utils.RegisterDeviceAndSetData();
        }
    }
}