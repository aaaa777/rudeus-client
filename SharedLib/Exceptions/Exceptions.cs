namespace SharedLib.Exceptions
{
    public class ServerUnavailableException : Exception
    {
        public ServerUnavailableException(string message) : base(message) { }
    }

    public class AccessTokenUnavailableException : Exception
    {
        public AccessTokenUnavailableException(string message) : base(message) { }
    }

    public class UnexpectedResponseException : Exception
    {
        public UnexpectedResponseException(string message) : base(message) { }
    }
    
}