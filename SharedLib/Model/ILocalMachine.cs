namespace Rudeus.Model
{
    /// <summary>
    /// ローカルマシンの情報を取得するモデルのインターフェース
    /// </summary>
    public interface ILocalMachine
    {
        public abstract static ILocalMachine GetInstance();

        public string GetPhysicalRamInfo();

        /// <summary>
        /// 物理ディスクドライブのIDリストを取得
        /// 現時点で利用していない
        /// </summary>
        /// <returns></returns>
        public List<string> GetStorageDeviceIdList();

        public string GetDeviceId();

        public string GetHostname();

        public string GetWinVersion();

        public string GetSpec();
        string GetCpuName();
        string GetMemory();
        string GetCDrive();
        string GetOS();
        string GetOSVersion();
        string GetWithSecure();
        string GetLabelId();
    }
}