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
        List<string> GetStorageDeviceIdList();

        string GetProductName();
        string GetDeviceId();

        string GetHostname();

        string GetWinVersion();

        string GetSpec();
        string GetCpuName();
        string GetMemory();
        string GetCDrive();
        string GetOS();
        string GetOSVersion();
        string GetWithSecure();
        string GetLabelId();
        List<Dictionary<string, string>> GetNetworkInterfaces();
    }
}