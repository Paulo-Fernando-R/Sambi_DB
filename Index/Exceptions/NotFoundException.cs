namespace db.Index.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
