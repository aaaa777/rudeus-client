
namespace Rudeus.API
{
    public class Exceptions
    {
        public class ServerUnavailableException : System.Exception
        {
            public ServerUnavailableException(string message) : base(message) { }
        }

        public class AccessTokenUnavailableException : System.Exception
        {
            public AccessTokenUnavailableException(string message) : base(message) { }
        }

        public class UnexpectedResponseException : System.Exception
        {
            public UnexpectedResponseException(string message) : base(message) { }
        }
    }
}