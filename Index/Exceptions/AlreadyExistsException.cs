namespace db.Index.Exceptions
{
    public class AlreadyExistsException : BaseException
    {
        public AlreadyExistsException() : base() { }
        public AlreadyExistsException(string message) : base(message) { }

        public AlreadyExistsException(string what, string identification) :
            base($"{what} {identification} already exists")
        { }

        public AlreadyExistsException(string what, string identification, string where) :
           base($"{what} {identification} already exists in {where}")
        { }

    }
}
