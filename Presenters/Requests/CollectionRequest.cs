namespace db.Presenters.Requests
{
    public class CollectionRequest
    {
        public required string CollectionName { get; set; }

        public CollectionRequest()
        {
            CollectionName = string.Empty;
        }
    }
}
