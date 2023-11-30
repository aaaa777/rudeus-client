namespace Rudeus
{
    public interface IUtils
    {
        public abstract static string ConcatStudentNumberFromMail(string mailAddress);

        public abstract static bool IsStudentMailAddress(string mailAddress);

        public abstract static string GetPhysicalRamInfo();

        public abstract static string[] GetStorageDeviceIdList();

        public abstract static string GetDeviceId();

        public abstract static string GetHostname();

        public abstract static string GetWinVersion();

        public abstract static void RegisterDeviceAndSetData();

        public abstract static void CopyDirectory(string sourceDir, string destDir, bool recursive);

        public abstract static int CompareVersionString(string a, string b, int depth);
    }
}