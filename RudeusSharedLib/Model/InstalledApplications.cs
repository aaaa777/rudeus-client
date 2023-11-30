using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.System.Inventory;

namespace Rudeus.Model
{
    internal class ApplicationData
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;

        public ApplicationData() { }
        public ApplicationData(string name, string version) 
        {
            Name = name;
            Version = version;
        }
    }
    internal class InstalledApplications
    {
        public static async Task<List<ApplicationData>> LoadAsync()
        {
            var resApps = new List<ApplicationData>();

            IReadOnlyList<InstalledDesktopApp> apps = await InstalledDesktopApp.GetInventoryAsync();
            foreach (InstalledDesktopApp app in apps)
            {
                Console.Write(app.DisplayVersion + "; ");
                Console.WriteLine(app.DisplayName);
                resApps.Add(new ApplicationData(app.DisplayName, app.DisplayVersion));
            }
            return resApps;
        }
    }
}
