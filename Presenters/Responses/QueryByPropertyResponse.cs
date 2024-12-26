namespace db.Presenters.Responses
{
    public class QueryByPropertyResponse
    {
        public string Id { get; set; }
        public dynamic Data { get; set; }

        public QueryByPropertyResponse()
        {
            Id = string.Empty;
            Data = string.Empty;
        }
    }
}
