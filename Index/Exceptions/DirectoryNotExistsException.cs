namespace db.Index.Exceptions
{
    public class DirectoryNotExistsException : BaseException
    {
        public DirectoryNotExistsException() : base() { }
        public DirectoryNotExistsException(string message) : base(message) { }
    }
}
