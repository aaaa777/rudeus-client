namespace Rudeus.Model
{
    /// <summary>
    /// ローカルマシンの情報を取得するモデルのインターフェース
    /// </summary>
    public interface ILocalMachine
    {
        public abstract static ILocalMachine GetInstance();

        public string GetPhysicalRamInfo();
        public List<string> GetStorageDeviceIdList();

        public string GetDeviceId();

        public string GetHostname();

        public string GetWinVersion();

        public string GetSpec();
    }
}