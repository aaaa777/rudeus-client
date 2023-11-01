namespace RudeusBgForm
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            form = new TaskTrayForm();
            Application.Run();
            //Application.Run(form);

#if (DEBUG)
            // Debug�r���h�̏ꍇ�I���O�ɑ҂�������
            Console.WriteLine("Program end. Press Enter to exit");
            Console.ReadLine();
#endif
        }
    }
}