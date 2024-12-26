using db.Index.Enums;

namespace db.Presenters.Requests
{
    public class QueryByPropertiesConditions
    {
        public required string Key { get; set; }
        public required string Value { get; set; }
        public required string Operation {get; set;}
        public string ArrayProperty { get; set;}

        public QueryByPropertiesConditions()
        {
            Key = string.Empty; 
            Value = string.Empty;
            Operation = OperatorsEnum.Undefined.ToDescriptionString();
            ArrayProperty = string.Empty;
        }
    }

}
