namespace db.Index.Exceptions
{
    public class NotFoundException : BaseException
    {
        // TODO Criar classe abstrada de excessão e fazer especializações para remover texto hardcoded
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string what, string identification) :
            base($"{what} {identification} not found")
        { }

        public NotFoundException(string what, string identification, string where) :
          base($"{what} {identification} not found in {where}")
        { }
    }
}
