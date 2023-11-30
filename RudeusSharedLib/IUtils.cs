namespace Rudeus
{
    public interface IUtils
    {
        public abstract static string ConcatStudentNumberFromMail(string mailAddress);

        public abstract static bool IsStudentMailAddress(string mailAddress);

        public abstract static void RegisterDeviceAndSetData();

        public abstract static void CopyDirectory(string sourceDir, string destDir, bool recursive);

        public abstract static int CompareVersionString(string a, string b, int depth);
    }
}