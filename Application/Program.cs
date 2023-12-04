namespace Rudeus.Application
{
    internal static class Program
    {
        private static TaskTrayForm? form;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            form = new TaskTrayForm();
            System.Windows.Forms.Application.Run();
            //ApplicationData.Run(form);

#if (!RELEASE)
            // Debugビルドの場合終了前に待ちが入る
            Console.WriteLine("Program end. Press Enter to exit");
            //Console.ReadLine();
#endif
        }
    }
}