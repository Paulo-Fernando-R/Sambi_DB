namespace db.Presenters.Requests
{
    public class RegisterUpdateArrayRequest
    {
        public required string RegisterId { get; set; }
        public required string CollectionName { get; set; }
        public required string ArrayName {  get; set; }
        public required string Property {  get; set; }
        public required string Value { get; set; }
        public required object NewValue { get; set; }
        public bool Confirm { get; set; }

        public RegisterUpdateArrayRequest()
        {
            RegisterId = string.Empty;
            CollectionName = string.Empty;
            Property = string.Empty;
            Value = string.Empty;
            Confirm = false;
        }
    }
}
