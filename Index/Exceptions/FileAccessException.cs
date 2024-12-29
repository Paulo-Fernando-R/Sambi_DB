namespace db.Index.Exceptions
{
    public class FileAccessException : BaseException
    {
        public FileAccessException() : base() { }
        public FileAccessException(string message) : base(message) { }
    }
}
