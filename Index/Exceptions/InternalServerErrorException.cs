namespace db.Index.Exceptions
{
    public class InternalServerErrorException : BaseException
    {
        public InternalServerErrorException() : base() { }
        public InternalServerErrorException(string message) : base(message) { }
    }
}
