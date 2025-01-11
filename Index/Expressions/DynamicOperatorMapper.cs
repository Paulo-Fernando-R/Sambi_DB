using db.Index.Enums;
using db.Index.Exceptions;
using db.Presenters.Requests;
using Newtonsoft.Json.Linq;
namespace db.Index.Expressions
{
    public static class DynamicOperatorMapper
    {
        //TODO Alterar arrow functions para funções comuns 
        //TODO Adicionar métodos de extensão https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
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
                throw new OperationNotAllowedException(operation: operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.GreaterOrEqualThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] >= float.Parse(value);
                }
                throw new OperationNotAllowedException(operation: operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] < float.Parse(value);
                }
                throw new OperationNotAllowedException(operation: operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessOrEqualThan.ToDescriptionString()] = (JObject x, string property, string value, string operation) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {property} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[property], out valueFloat))
                {
                    return (float)x[property] <= float.Parse(value);
                }
                throw new OperationNotAllowedException(operation: operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
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
            if (DynamicOperatorMapper.OperationsDictionary.TryGetValue(operation, out var result))
            {
                return result;
            }
            throw new OperationNotAllowedException(operation: operation, violation: "is not suported");
            //throw new OperationNotAllowedException($"Operator '{operation}' is not allowed");
        }

        public static bool ExecuteAllConditions(JObject item, string logicOperator, List<QueryByPropertiesConditions> conditions)
        {


            if (OperatorsEnum.Or.ToDescriptionString() == logicOperator)
            {
                return ExecuteAllConditionsOr(item, conditions);

            }
            else if (OperatorsEnum.And.ToDescriptionString() == logicOperator)
            {
                return ExecuteAllConditionsAnd(item, conditions);
            }
            else
            {
                throw new OperationNotAllowedException(operation: logicOperator, violation: "is not suported");
                //throw new OperationNotAllowedException($"The operator '{logicOperator}' is not allowed");
            }


        }

        private static bool ExecuteAllConditionsAnd(JObject item, List<QueryByPropertiesConditions> conditions)
        {
            bool flag = true;
            for (int i = 0; i < conditions.Count; i++)
            {
                var condition = DynamicOperatorMapper.GetOperation(conditions[i].Operation);
                bool res = condition(item, conditions[i].Key, conditions[i].Value, conditions[i].Operation);
                if (!res)
                {
                    flag = res;
                }
            }
            return flag;
        }

        private static bool ExecuteAllConditionsOr(JObject item, List<QueryByPropertiesConditions> conditions)
        {
            bool flag = false;
            for (int i = 0; i < conditions.Count; i++)
            {
                var condition = DynamicOperatorMapper.GetOperation(conditions[i].Operation);
                bool res = condition(item, conditions[i].Key, conditions[i].Value, conditions[i].Operation);
                if (res)
                {
                    flag = res;
                }
            }
            return flag;
        }

    }
}
