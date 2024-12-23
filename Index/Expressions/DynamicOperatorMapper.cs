using db.Index.Enums;
using db.Index.Exceptions;
using Newtonsoft.Json.Linq;
namespace db.Index.Expressions
{
    public static class DynamicOperatorMapper
    {

        public static readonly Dictionary<string, Func<JObject, string, string, string, bool>> OperationsDictionary = new Dictionary<string, Func<JObject, string, string, string, bool>>
        {
            [OperatorsEnum.Equal.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;

                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] == float.Parse(value);
                }

                DateTime valueDateTime = DateTime.Now;

                if (DateTime.TryParse((string)x[property], out valueDateTime))
                {
                    return valueDateTime == DateTime.Parse(value);
                }

                DateOnly valueDateOnly = new DateOnly();

                if (DateTime.TryParse((string)x[property], out valueDateTime))
                {
                    return valueDateOnly == DateOnly.Parse(value);
                }

                bool valueBool = false;

                if (bool.TryParse((string)x[property], out valueBool))
                {
                    return valueBool == bool.Parse(value);
                }

                string valueString = string.Empty;

                try
                {
                    return (string)x[property] == value;

                }
                catch (Exception)
                {
                    throw;
                }


            },
            [OperatorsEnum.NotEqual.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;

                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] != float.Parse(value);
                }

                DateTime valueDateTime = DateTime.Now;

                if (DateTime.TryParse((string)x[property], out valueDateTime))
                {
                    return valueDateTime != DateTime.Parse(value);
                }

                DateOnly valueDateOnly = new DateOnly();

                if (DateTime.TryParse((string)x[property], out valueDateTime))
                {
                    return valueDateOnly != DateOnly.Parse(value);
                }

                bool valueBool = false;

                if (bool.TryParse((string)x[property], out valueBool))
                {
                    return valueBool != bool.Parse(value);
                }

                try
                {
                    return (string)x[property] == value;

                }
                catch (Exception)
                {
                    throw;
                }
            },

            [OperatorsEnum.GreaterThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return valueFloat > float.Parse(value);
                }
                throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.GreaterOrEqualThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] >= float.Parse(value);
                }
                throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] < float.Parse(value);
                }
                throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessOrEqualThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] <= float.Parse(value);
                }
                throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.Like.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                try
                {
                    var like = (string)x[property];
                    return like.Contains(value);

                }
                catch (Exception)
                {
                    throw;
                }
            }



        };








        public static Func<JObject, string, string, string, bool> GetOperation(string operation)
        {
            if(DynamicOperatorMapper.OperationsDictionary.TryGetValue(operation, out var result))
            {
                return result;
            }
            throw new OperationNotAllowedException($"Operator {operation} is not suported");


           
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
