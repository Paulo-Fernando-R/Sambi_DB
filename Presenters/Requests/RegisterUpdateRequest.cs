namespace db.Presenters.Requests
{
    public class RegisterUpdateRequest
    {
        public required string CollectionName { get; set; }
        public required string RegisterId { get; set; }
        public required object Data { get; set; }

        public RegisterUpdateRequest()
        {
            CollectionName = string.Empty;
            RegisterId = string.Empty;
            Data = new object();
        }
    }
}
