namespace db.Index.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException() : base() { }
        public AlreadyExistsException(string message) : base(message) { }
    }
}
