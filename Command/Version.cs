using SharedLib;

namespace Rudeus.Command
{
    public class Version : IVersion
    {
        public static new string ToString() 
        {
            return "0.2.0";
        }
    }
}
