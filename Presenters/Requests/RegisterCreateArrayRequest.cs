namespace db.Presenters.Requests
{
    public class RegisterCreateArrayRequest
    {
        public required string RegisterId { get; set; }
        public required string CollectionName { get; set; }
        public required string ArrayName { get; set; }
        public required object Data { get; set; }

        public RegisterCreateArrayRequest()
        {
            RegisterId = string.Empty;
            CollectionName = string.Empty;
            ArrayName = string.Empty;
            Data = new object();
        }
    }
}
