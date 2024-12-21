using db.Index.Enums;
using System.Linq.Expressions;
namespace db.Index.Expressions
{
    public static class DynamicOperatorMapper
    {
        // Mapeia strings para funções de Expression
        public static readonly Dictionary<string, Func<Expression, Expression, Expression>> OperationMap =
            new Dictionary<string, Func<Expression, Expression, Expression>>
            {
            { OperationsEnum.Equal, Expression.Equal },

            { OperationsEnum.NotEqual, Expression.NotEqual },

            { OperationsEnum.GreaterThan, (left, right) =>
                Expression.GreaterThan(
                    Expression.Convert(left, typeof(IComparable)),
                    Expression.Convert(right, typeof(IComparable))) },

            { OperationsEnum.LessThan, (left, right) =>
                Expression.LessThan(
                    Expression.Convert(left, typeof(IComparable)),
                    Expression.Convert(right, typeof(IComparable))) },

            { OperationsEnum.AreInArray, (left, right) =>
                Expression.Call(
                    Expression.Convert(left, typeof(string)),
                    nameof(string.Contains),
                    Type.EmptyTypes,
                    Expression.Convert(right, typeof(string))) },
            {
              OperationsEnum.GreaterOrEqualThan, (left, right) =>
                Expression.GreaterThanOrEqual(
                    Expression.Convert(left, typeof(IComparable)),
                    Expression.Convert(right, typeof(IComparable)))},

            {
              OperationsEnum.LessOrEqualThan, (left, right) =>
                Expression.LessThanOrEqual(
                    Expression.Convert(left, typeof(IComparable)),
                    Expression.Convert(right, typeof(IComparable)))},

             { OperationsEnum.Like, (left, right) =>
                Expression.Call(
                    typeof(Program).GetMethod(nameof(DynamicOperatorMapper.Like), new[] { typeof(string), typeof(string) })!,
                    left,
                    right) }
            };

        // Método auxiliar para obter a função baseada no nome da operação
        public static Func<Expression, Expression, Expression> GetOperation(string operation)
        {
            if (OperationMap.TryGetValue(operation, out var func))
            {
                return func;
            }
            throw new NotSupportedException($"Operation '{operation}' is not supported.");
        }
        public static bool Like(string? source, string? pattern)
        {
            if (source == null || pattern == null)
                return false;

            // Substituir os curingas SQL por padrões regex
            var regexPattern = "^" + System.Text.RegularExpressions.Regex.Escape(pattern)
                .Replace("%", ".*")   // '%' → qualquer sequência de caracteres
                .Replace("_", ".")    // '_' → um único caractere
                + "$";

            return System.Text.RegularExpressions.Regex.IsMatch(source, regexPattern);
        }

    }
}
