using db.Index.Enums;

namespace db.Presenters.Requests
{
    public class QueryByPropertiesRequest
    {
        public required string CollectionName { get; set; }
        public int? Limit { get; set; }
        public int? Skip { get; set; }
        public string? ConditionsBehavior { get; set; }
        public required List<QueryByPropertiesConditions> QueryConditions { get; set; }

        public QueryByPropertiesRequest()
        {
            CollectionName = string.Empty;
            Limit = 20;
            Skip = 0;
            ConditionsBehavior = OperatorsEnum.And.ToDescriptionString();
            QueryConditions = new List<QueryByPropertiesConditions>();
        }
    }
}
