namespace Rudeus.Procedure
{
    /// <summary>
    /// ひとまとまりの処理を実行するインターフェース
    /// 例えば、アクセストークンの再発行、インストール済みアプリの送信など
    /// </summary>
    public interface IProcedure
    {
        /// <summary>
        /// 処理を実行する
        /// </summary>
        Task Run();
    }
}