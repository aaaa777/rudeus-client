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

        public UpdateRequest BuildUpdateRequestWithChanges()
        {
            var request = new UpdateRequest();
            
            // hostname
            if(LM.GetHostname() != Settings.HostnameP)
            {
                request.request_data.hostname = LM.GetHostname();
            }

            // spec
            if(LM.GetSpec() != Settings.SpecP)
            {
                request.request_data.spec = LM.GetSpec();
            }

            // cpu_name
            if(LM.GetCpuName() != Settings.CpuNameP)
            {
                request.request_data.cpu_name = LM.GetCpuName();
            }

            // memory
            if(LM.GetMemory() != Settings.MemoryP)
            {
                request.request_data.memory = Int32.Parse(LM.GetMemory());
            }

            // c_drive
            if(LM.GetCDrive() != Settings.CDriveP)
            {
                request.request_data.c_drive = Int32.Parse(LM.GetCDrive());
            }

            // os
            if(LM.GetOS() != Settings.OSP)
            {
                request.request_data.os = LM.GetOS();
            }

            // os_version
            if(LM.GetOSVersion() != Settings.OSVersionP)
            {
                request.request_data.os_version = LM.GetOSVersion();
            }

            // withsecure
            if(LM.GetWithSecure() != Settings.WithSecureP)
            {
                request.request_data.withsecure = LM.GetWithSecure();
            }

            // label_id
            if(LM.GetLabelId() != Settings.LabelIdP)
            {
                request.request_data.label_id = LM.GetLabelId();
            }

            return request;
        }
    }
}
