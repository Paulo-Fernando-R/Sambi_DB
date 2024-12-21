using System.ComponentModel;

namespace db.Index.Enums
{
    public enum OperatorsEnum
    {
        [Description("==")]
        Equal,

        [Description("!=")]
        NotEqual,

        [Description(">")]
        GreaterThan,

        [Description(">=")]
        GreaterOrEqualThan,

        [Description("<")]
        LessThan,

        [Description("<=")]
        LessOrEqualThan,

        [Description("[==]")]
        AreInArray,

        [Description("%")]
        Like,

        [Description("")]
        Undefined

    }

    public static class OperatorsEnumExtensions
    {
        public static string ToDescriptionString(this OperatorsEnum val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }

    public static class OperationsEnum
    {
        // public OperationsEnum() { }


        public const string Equal = "==";
        public const string NotEqual = "!=";
        public const string Like = "%";
        public const string GreaterThan = ">";
        public const string GreaterOrEqualThan = ">=";
        public const string LessThan = "<";
        public const string LessOrEqualThan = "<=";
        public const string AreInArray = "[==]";
        public const string Undefined = "";


    }
}
