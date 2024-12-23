namespace db.Index.Exceptions
{
    public class OperationNotAllowedException : BaseException
    {
        public OperationNotAllowedException() : base() { }
        public OperationNotAllowedException(string message) : base(message) { }
    }
}
