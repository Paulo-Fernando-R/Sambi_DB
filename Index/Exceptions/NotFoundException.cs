namespace db.Index.Exceptions
{
    public class NotFoundException : BaseException
    {
        // TODO Criar classe abstrada de excessão e fazer especializações para remover texto hardcoded
        public NotFoundException() : base() { }
        public NotFoundException(string message) : base(message) { }
    }
}
