namespace UserService.Exceptions
{
    public class DatabaseSavingException : Exception
    {
        public DatabaseSavingException(string message) : base(message) {}
        public DatabaseSavingException(string message, Exception innerException) : base(message, innerException) {}
    }
}