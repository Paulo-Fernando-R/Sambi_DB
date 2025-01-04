namespace db.Presenters.Requests
{
    public class RegisterDeleteRequest
    {
        public required string RegisterId { get; set; }
        public required string CollectionName { get; set; }
        public required bool Confirm { get; set; }

        public RegisterDeleteRequest()
        {
            RegisterId = string.Empty;
            CollectionName = string.Empty;
            Confirm = false;
        }
    }
}
