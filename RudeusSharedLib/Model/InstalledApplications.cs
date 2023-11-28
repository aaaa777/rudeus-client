using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Windows.System.Inventory;

namespace Rudeus.Model
{
    internal class InstalledApplication
    {
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
    }
    internal class InstalledApplications
    {
        public static async Task<List<InstalledApplication>> LoadAsync()
        {
            var resApps = new List<InstalledApplication>();

            IReadOnlyList<InstalledDesktopApp> apps = await InstalledDesktopApp.GetInventoryAsync();
            foreach (InstalledDesktopApp app in apps)
            {
                Console.Write(app.DisplayVersion + "; ");
                Console.WriteLine(app.DisplayName);
                resApps.Add(new InstalledApplication() { Name = app.DisplayName, Version = app.DisplayVersion });
            }
            return resApps;
        }
    }
}
