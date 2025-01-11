namespace db.Index.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException() : base() { }
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string identification, string rule) : base($"{identification} {rule}") { }
        public BadRequestException(string identification, string rule, string where) : base($"{identification} {rule} in {where}") { }
    }
}
