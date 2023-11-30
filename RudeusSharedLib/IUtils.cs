namespace Rudeus.Model
{
    public interface IUtils
    {
        public abstract string ConcatStudentNumberFromMail(string mailAddress);

        public abstract bool IsStudentMailAddress(string mailAddress);

        public abstract string GetPhysicalRamInfo();

        public abstract string[] GetStorageDeviceIdList();

        public abstract static string GetDeviceId();

        public abstract static string GetHostname();

        public abstract static string GetWinVersion();

        public abstract static void RegisterDeviceAndSetData();

        public abstract static void CopyDirectory(string sourceDir, string destDir);

        public abstract static int CompareVersionString(string a, string b, int depth);
    }
}