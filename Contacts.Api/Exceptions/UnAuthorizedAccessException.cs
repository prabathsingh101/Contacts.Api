namespace Contacts.Api.Exceptions
{
    public class UnAuthorizedAccessException : Exception
    {
        public UnAuthorizedAccessException(string message) : base(message) { }
    }
}