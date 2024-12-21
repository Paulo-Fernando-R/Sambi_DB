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

    public class OperationsEnum
    {
        public OperationsEnum() { }
        private OperationsEnum(string value) { Value = value; }
        public string Value { get; private set; }

        public static OperationsEnum Equal { get { return new OperationsEnum("=="); } }
        public static OperationsEnum NotEqual { get { return new OperationsEnum("!="); } }
        public static OperationsEnum Like { get { return new OperationsEnum("%"); } }
        public static OperationsEnum GreaterThan { get { return new OperationsEnum(">"); } }
        public static OperationsEnum GreaterOrEqualThan { get { return new OperationsEnum(">="); } }
        public static OperationsEnum LessThan { get { return new OperationsEnum("<"); } }
        public static OperationsEnum LessOrEqualThan { get { return new OperationsEnum("<="); } }
        public static OperationsEnum AreInArray { get { return new OperationsEnum("[==]"); } }
        public static OperationsEnum Undefined { get { return new OperationsEnum(string.Empty); } }

        public override string ToString()
        {
            //var values = Enum.GetValues(typeof(OperationsEnum)).Cast<string>().OrderBy(x => x);

            //values.Contains("==");
            return Value;
        }
    }
}
