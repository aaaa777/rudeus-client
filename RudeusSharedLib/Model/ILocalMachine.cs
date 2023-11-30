namespace Rudeus.Model
{
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