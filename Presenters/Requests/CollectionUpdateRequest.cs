namespace db.Presenters.Requests
{
    public class CollectionUpdateRequest
    {
        public required string CollectionName { get; set; }
        public required string NewCollectionName { get; set; }
        public required bool Confirm { get; set; }

        public  CollectionUpdateRequest()
        {
            CollectionName = string.Empty;
            NewCollectionName = string.Empty;
            Confirm = false;
        }
    }
}
