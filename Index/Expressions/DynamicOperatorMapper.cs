using db.Index.Enums;
using db.Index.Exceptions;
using db.Presenters.Requests;
using Newtonsoft.Json.Linq;
namespace db.Index.Expressions
{
    public static class DynamicOperatorMapper
    {

        #region Dictionary
        //TODO Alterar arrow functions para funções comuns 
        //TODO Adicionar métodos de extensão https://learn.microsoft.com/pt-br/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
        public static readonly Dictionary<string, Func<JObject, QueryByPropertiesConditions, bool>> OperationsDictionary = new Dictionary<string, Func<JObject, QueryByPropertiesConditions, bool>>
        {
            [OperatorsEnum.Equal.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;

                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return (float)x[condition.Key] == float.Parse(condition.Value);
                }

                DateTime valueDateTime = DateTime.Now;

                if (DateTime.TryParse((string)x[condition.Key], out valueDateTime))
                {
                    return valueDateTime == DateTime.Parse(condition.Value);
                }

                DateOnly valueDateOnly = new DateOnly();

                if (DateTime.TryParse((string)x[condition.Key], out valueDateTime))
                {
                    return valueDateOnly == DateOnly.Parse(condition.Value);
                }

                bool valueBool = false;

                if (bool.TryParse((string)x[condition.Key], out valueBool))
                {
                    return valueBool == bool.Parse(condition.Value);
                }

                string valueString = string.Empty;

                try
                {
                    return (string)x[condition.Key] == condition.Value;

                }
                catch (Exception)
                {
                    throw;
                }


            },
            [OperatorsEnum.NotEqual.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;

                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return (float)x[condition.Key] != float.Parse(condition.Value);
                }

                DateTime valueDateTime = DateTime.Now;

                if (DateTime.TryParse((string)x[condition.Key], out valueDateTime))
                {
                    return valueDateTime != DateTime.Parse(condition.Value);
                }

                DateOnly valueDateOnly = new DateOnly();

                if (DateTime.TryParse((string)x[condition.Key], out valueDateTime))
                {
                    return valueDateOnly != DateOnly.Parse(condition.Value);
                }

                bool valueBool = false;

                if (bool.TryParse((string)x[condition.Key], out valueBool))
                {
                    return valueBool != bool.Parse(condition.Value);
                }

                try
                {
                    return (string)x[condition.Key] == condition.Value;

                }
                catch (Exception)
                {
                    throw;
                }
            },

            [OperatorsEnum.GreaterThan.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return valueFloat > float.Parse(condition.Value);
                }
                throw new OperationNotAllowedException(operation: condition.Operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.GreaterOrEqualThan.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return (float)x[condition.Key] >= float.Parse(condition.Value);
                }
                throw new OperationNotAllowedException(operation: condition.Operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessThan.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return (float)x[condition.Key] < float.Parse(condition.Value);
                }
                throw new OperationNotAllowedException(operation: condition.Operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.LessOrEqualThan.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                float valueFloat = 0;
                if (float.TryParse((string)x[condition.Key], out valueFloat))
                {
                    return (float)x[condition.Key] <= float.Parse(condition.Value);
                }
                throw new OperationNotAllowedException(operation: condition.Operation, violation: "is allowed only for Number values");
                //throw new OperationNotAllowedException($"Operation {operation} is allowed only for Number values");
            },

            [OperatorsEnum.Like.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                try
                {
                    var like = (string)x[condition.Key];
                    return like.Contains(condition.Value);

                }
                catch (Exception)
                {
                    throw;
                }
            },

            [OperatorsEnum.AreInArray.ToDescriptionString()] = (JObject x, QueryByPropertiesConditions condition) =>
            {
                if (x == null) throw new ArgumentNullException($"The property {condition.Key} not existis in this collection");

                try
                {
                    bool found = false;

                    if (!x.ContainsKey(condition.ArrayProperty))
                    {
                        return false;
                        throw new BadRequestException(identification: condition.ArrayProperty, rule: "not exists");
                    }

                    var arr = x[condition.ArrayProperty];

                    for (int i = 0; i < arr.Count(); i++)
                    {
                        var item = arr[i];
                        JToken? aux;
                        try
                        {
                            aux = item[condition.Key];
                        }
                        catch (Exception)
                        {
                            continue;
                        }

                        if (aux == null)
                        {
                            continue;
                        }

                        if (aux.ToString() == condition.Value)
                        {
                            found = true;
                            break;
                        }
                    }

                    return found;

                }
                catch (Exception)
                {
                    throw;
                }

            }

        };
        #endregion Dictionary

        #region Functions
        public static Func<JObject, QueryByPropertiesConditions, bool> GetOperation(string operation)
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
                bool res = condition(item, conditions[i]);
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
                bool res = condition(item, conditions[i]);
                if (res)
                {
                    flag = res;
                }
            }
            return flag;
        }
        #endregion Functions

    }
}
