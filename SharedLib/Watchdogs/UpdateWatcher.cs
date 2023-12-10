using Rudeus.API.Request;
using System;
using System.Collections.Generic;
using System.Text;
using Rudeus.Model;
using SharedLib.Model.Settings;

namespace Rudeus.Watchdogs
{
    public class UpdateWatcher
    {
        public ILocalMachine LM { get; set; }

        public IRootSettings Settings { get; set; }

        public UpdateWatcher(ILocalMachine? lm = null, IRootSettings? settings = null) 
        {
            // TODO: DI用引数チェックをUtilsで共通化する
            // 全てnullの場合はデフォルトなのでOK
            if (!(lm == null && settings == null))
            {
                // どれか一つでもnullの場合はエラー
                if(lm == null || settings == null)
                {
                    throw new ArgumentException("lm, urb, settings cannot be null at the same time");
                }
            }
            LM = lm ?? LocalMachine.GetInstance();
            Settings = settings ?? new RootSettings();
        }

        public UpdateMacRequest BuildUpdateMacRequest()
        {
            var request = new UpdateMacRequest();
            /*
            // mac_address
            if(LM.GetMacAddress() != Settings.MacAddressP)
            {
                request.response_data.mac_address = LM.GetMacAddress();
            }
            */
            var requestData = new UpdateMacInterface();
            requestData.mac_address = "12-34-56-78-90-AB";
            requestData.name = "Rudeus Virtual LAN card";
            request.request_data.interfaces.Add(requestData);

            return request;
        }

        public UpdateRequest BuildUpdateRequest(bool disableCache=false)
        {
            var request = new UpdateRequest();
            
            // hostname
            if(disableCache || LM.GetHostname() != Settings.HostnameP)
            {
                request.request_data.hostname = LM.GetHostname();
            }

            // spec
            if(disableCache || LM.GetSpec() != Settings.SpecP)
            {
                request.request_data.spec = LM.GetSpec();
            }

            // cpu_name
            if(disableCache || LM.GetCpuName() != Settings.CpuNameP)
            {
                request.request_data.cpu_name = LM.GetCpuName();
            }

            // memory
            if(disableCache || LM.GetMemory() != Settings.MemoryP)
            {
                request.request_data.memory = Int32.Parse(LM.GetMemory());
            }

            // c_drive
            if(disableCache || LM.GetCDrive() != Settings.CDriveP)
            {
                request.request_data.c_drive = Int32.Parse(LM.GetCDrive());
            }

            // os
            if(disableCache || LM.GetOS() != Settings.OSP)
            {
                request.request_data.os = LM.GetOS();
            }

            // os_version
            if(disableCache || LM.GetOSVersion() != Settings.OSVersionP)
            {
                request.request_data.os_version = LM.GetOSVersion();
            }

            // withsecure
            if(disableCache || LM.GetWithSecure() != Settings.WithSecureP)
            {
                request.request_data.withsecure = LM.GetWithSecure() != "";
            }

            return request;
        }
    }
}
