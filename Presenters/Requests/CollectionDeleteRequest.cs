namespace db.Presenters.Requests
{
    public class CollectionDeleteRequest
    {
        public required string CollectionName { get; set; }
        public required bool Confirm {  get; set; }

        public CollectionDeleteRequest()
        {
            this.Confirm = false;
            this.CollectionName = string.Empty;
        }
    }
}
