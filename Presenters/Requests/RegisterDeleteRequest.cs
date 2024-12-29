namespace db.Presenters.Requests
{
    public class RegisterDeleteRequest
    {
        public string RegisterId { get; set; }
        public string CollectionName { get; set; }
        public bool Confirm { get; set; }

        public RegisterDeleteRequest()
        {
            RegisterId = string.Empty;
            CollectionName = string.Empty;
            Confirm = false;
        }
    }
}
