using Rudeus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.SharedLib.Model
{
    public class FakeLocalMachine : ILocalMachine
    {
        public FakeLocalMachine() { }

        public static ILocalMachine GetInstance()
        {
            throw new NotImplementedException();
        }

        public string GetPhysicalRamInfo()
        {
            throw new NotImplementedException();
        }

        public List<string> GetStorageDeviceIdList()
        {
            throw new NotImplementedException();
        }


        public string GetDeviceId()
        {
            return $"000000-123456";
        }

        public string GetHostname()
        {
            return "P12-345";
        }

        public string GetWinVersion()
        {
            return "12345.6789";
        }

        public string GetSpec()
        {
            return "Intel Xeon E5 2250;16GB";
        }

        public string GetCpuName()
        {
            return "Intel Xeon E5";
        }

        public string GetMemory()
        {
            return "16GB";
        }

        public string GetCDrive()
        {
            return "256GB";
        }

        public string GetOS()
        {
            return "windows 10";
        }

        public string GetOSVersion()
        {
            return "10.23456.234";
        }

        public string GetWithSecure()
        {
            return "";
        }

        public string GetLabelId()
        {
            return "P12-345";
        }

        public string GetProductName()
        {
            throw new NotImplementedException();
        }
    }
}
