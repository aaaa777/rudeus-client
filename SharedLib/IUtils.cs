namespace Rudeus
{
    /// <summary>
    /// ユーティリティクラスのインターフェース
    /// </summary>
    public interface IUtils
    {
        /// <summary>
        /// メールアドレスから学籍番号を抽出する
        /// </summary>
        /// <param name="mailAddress">sXXXXXXX@s.do-johodai.ac.jpのメールアドレス</param>
        /// <returns>XXXXXXXの数字部分</returns>
        public abstract static string ConcatStudentNumberFromMail(string mailAddress);

        /// <summary>
        /// メールアドレスが学生用のものかどうかを判定する
        /// </summary>
        /// <param name="mailAddress">メールアドレス</param>
        /// <returns></returns>
        public abstract static bool IsStudentMailAddress(string mailAddress);

        /// <summary>
        /// 廃止予定
        /// デバイスを登録し、データを設定する
        /// </summary>
        public abstract static void RegisterDeviceAndSetData();

        /// <summary>
        /// ディレクトリを再帰的にコピーする
        /// </summary>
        /// <param name="sourceDir"></param>
        /// <param name="destDir"></param>
        /// <param name="recursive"></param>
        public abstract static void CopyDirectory(string sourceDir, string destDir, bool recursive);

        /// <summary>ドット区切り数字列を比較する</summary>
        /// <param name="a">対象文字列A</param>
        /// <param name="b">対象文字列B</param>
        /// <param name="depth">比較する列数 (0なら全て)</param>
        /// <returns>A-Bの符号 (1, 0, -1)</returns>
        public abstract static int CompareVersionString(string a, string b, int depth);

        /// <summary>
        /// key=value形式の引数をパースする
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract static Dictionary<string, string> ParseArgs(string[] args);

        /// <summary>
        /// DI用の引数が中途半端に指定されているかどうかを判定する
        /// </summary>
        public abstract static bool IsArgsAllNullOrAllObject(List<object> args);
    }
}