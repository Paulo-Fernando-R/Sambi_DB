namespace db.Index.Exceptions
{
    public class OperationNotAllowedException : BaseException
    {
        public OperationNotAllowedException() : base() { }
        public OperationNotAllowedException(string message) : base(message) { }
        public OperationNotAllowedException(string operation, string violation) : base($"Operation {operation} {violation}") { }
    }
}
