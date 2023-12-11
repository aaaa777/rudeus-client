using Rudeus;
using Rudeus.Model;
using Rudeus.Procedure;
using SharedLib.Model.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rudeus.Procedure
{
    public class LocalMachineInfoUpdater : IProcedure
    {
        public IRootSettings RootSettings { get; set; }

        public ILocalMachine LocalMachine { get; set; }
        public LocalMachineInfoUpdater(IRootSettings? settings = null, ILocalMachine? localMachine = null) 
        {
            RootSettings = settings ?? new RootSettings();
            LocalMachine = localMachine ?? Model.LocalMachine.GetInstance();
        }
        public async Task Run()
        {
            RootSettings.HostnameP = LocalMachine.GetHostname();
            RootSettings.SpecP = LocalMachine.GetSpec();
            RootSettings.CpuNameP = LocalMachine.GetCpuName();
            RootSettings.MemoryP = LocalMachine.GetMemory();
            RootSettings.CDriveP = LocalMachine.GetCDrive();
            RootSettings.OSP = LocalMachine.GetOS();
            RootSettings.OSVersionP = LocalMachine.GetOSVersion();
            RootSettings.WithSecureP = LocalMachine.GetWithSecure();
            RootSettings.LabelIdP = LocalMachine.GetLabelId();

            RootSettings.InterfacesHashP = Utils.GetNetworkInterfaceHash(LocalMachine.GetNetworkInterfaces());
            RootSettings.InstalledAppsHashP = Utils.GetInstalledAppsHash(InstalledApplications.LoadAsync().Result);
        }


    }
}
