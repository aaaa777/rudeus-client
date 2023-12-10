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

        /// <summary>
        /// 変更がない場合はnullを返す
        /// </summary>
        /// <returns></returns>
        public UpdateMacRequest? BuildUpdateMacRequest()
        {
            var interfaces = LM.GetNetworkInterfaces();
            if(Settings.InterfacesHashP == Utils.GetNetworkInterfaceHash(interfaces))
            {
                return null;
            }

            var request = new UpdateMacRequest();
            foreach(var i in interfaces)
            {
                var requestData = new UpdateMacInterface();
                requestData.mac_address = i["mac_address"];
                requestData.name = i["name"];
                request.request_data.interfaces.Add(requestData);
            }

            return request;
        }

        /// <summary>
        /// 変更がない場合はnullを返す予定
        /// </summary>
        /// <param name="disableCache"></param>
        /// <returns></returns>
        public UpdateRequest BuildUpdateRequest(bool disableCache=false)
        {
            var request = new UpdateRequest();
            bool isChanged = false;
            
            // hostname
            if(disableCache || LM.GetHostname() != Settings.HostnameP)
            {
                request.request_data.hostname = LM.GetHostname();
                isChanged = true;
            }

            // spec
            if(disableCache || LM.GetSpec() != Settings.SpecP)
            {
                request.request_data.spec = LM.GetSpec();
                isChanged = true;
            }

            // cpu_name
            if(disableCache || LM.GetCpuName() != Settings.CpuNameP)
            {
                request.request_data.cpu_name = LM.GetCpuName();
                isChanged = true;
            }

            // memory
            if(disableCache || LM.GetMemory() != Settings.MemoryP)
            {
                request.request_data.memory = Int32.Parse(LM.GetMemory());
                isChanged = true;
            }

            // c_drive
            if(disableCache || LM.GetCDrive() != Settings.CDriveP)
            {
                request.request_data.c_drive = Int32.Parse(LM.GetCDrive());
                isChanged = true;
            }

            // os
            if(disableCache || LM.GetOS() != Settings.OSP)
            {
                request.request_data.os = LM.GetOS();
                isChanged = true;
            }

            // os_version
            if(disableCache || LM.GetOSVersion() != Settings.OSVersionP)
            {
                request.request_data.os_version = LM.GetOSVersion();
                isChanged = true;
            }

            // withsecure
            if(disableCache || LM.GetWithSecure() != Settings.WithSecureP)
            {
                request.request_data.withsecure = LM.GetWithSecure() != "";
                isChanged = true;
            }

            //if(!isChanged)
            //{
            //    return null;
            //}
            return request;
        }
    }
}
