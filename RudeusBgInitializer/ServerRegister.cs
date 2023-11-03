using Rudeus.Model;
using Rudeus.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class ServerRegister
{
    public static void Run()
    {
        Utils.RegisterDeviceAndSetData();
    }
}

