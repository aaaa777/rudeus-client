using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Rudeus.Model;

#pragma warning disable IDE1006 // 命名スタイル
namespace Rudeus.API.Request
{
    public class SendInstalledAppsRequest : IRequest
    {
        [JsonPropertyName(nameof(type))]
        public string type { get; set; } = "send_installed_app_request";

        [JsonPropertyName(nameof(request_data))]
        public SendInstalledAppsRequestData request_data { get; set; }


/* プロジェクト 'RudeusBgForm' からのマージされていない変更
前:
        public SendInstalledAppsRequest(List<InstalledApplication> apps)
後:
        public SendInstalledAppsRequest(List<Model.ApplicationData> apps)
*/
        public SendInstalledAppsRequest(List<ApplicationData> apps)
        {
            request_data = new(apps);
        }
    }
    internal class SendInstalledAppsRequestData
    {
        [JsonPropertyName(nameof(apps))]
        public List<SendInstalledAppsAppData> apps { get; set; } = new();


/* プロジェクト 'RudeusBgForm' からのマージされていない変更
前:
        public SendInstalledAppsRequestData(List<InstalledApplication> apps) 
後:
        public SendInstalledAppsRequestData(List<Model.ApplicationData> apps) 
*/
        public SendInstalledAppsRequestData(List<ApplicationData> apps) 
        {
            foreach(var app in apps)
            {
                this.apps.Add(new SendInstalledAppsAppData(app.Name, app.Version));
            }

        }
    }

    internal class SendInstalledAppsAppData
    {
        [JsonPropertyName(nameof(name))]
        public string name { get; set; }

        [JsonPropertyName(nameof(version))]
        public string version { get; set; }
        
        public SendInstalledAppsAppData(string name, string version)
        {
            this.name = name;
            this.version = version;
        }
    }

    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(SendInstalledAppsRequest))]
    internal partial class SendInstalledAppsRequestContext : JsonSerializerContext
    {
    }
}
