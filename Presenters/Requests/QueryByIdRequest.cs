namespace db.Presenters.Requests
{
    public class QueryByIdRequest
    {
        public string CollectionName {  get; set; }
        public string RegisterId { get; set; }

        public QueryByIdRequest(string collectionName, string registerId)
        {
            CollectionName = collectionName;
            RegisterId = registerId;
        }
    }
}
