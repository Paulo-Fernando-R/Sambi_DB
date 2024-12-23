using db.Index.Enums;
using db.Index.Exceptions;
using db.Index.Expressions;
using db.Models;
using db.Presenters.Requests;

namespace db.Index.Operations
{
    public class QueryOperations
    {
        public SearchTreeNode QueryById(QueryByIdRequest request)
        {
            var sTree = new SearchTree(request.CollectionName + ".zip");
            var res = sTree.SearchById(request.RegisterId);

            try
            {
                if (res == null)
                {
                    throw new NotFoundException($"Register {request.RegisterId} not found.");
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void QueryByPropertiesFactory(QueryByPropertiesRequest request)
        {
            var sTree = new SearchTree(request.CollectionName + ".zip");
            var condition = DynamicOperatorMapper.GetOperation(request.QueryConditions[0].Operation);
          

       
            var res = sTree.SearchByProperty("Age", "20", request.QueryConditions);

            foreach (var item in res)
            {
                Console.WriteLine(item);
            }

        }


    }
}
/*
 
 for (int i = 0; i < request.QueryConditions.Count; i++)
            {
                switch (request.QueryConditions[i].Operation)
                {
                    case OperationsEnum.Equal:
                        break;

                    case OperationsEnum.NotEqual:
                        break;

                    case OperationsEnum.GreaterThan:
                        break;

                    case OperationsEnum.LessThan:
                        break;

                    case OperationsEnum.GreaterOrEqualThan:
                        break;

                    case OperationsEnum.LessOrEqualThan:
                        break;

                    case OperationsEnum.AreInArray: 
                        break;

                    case OperationsEnum.Like: 
                        break;

                    case OperationsEnum.Undefined: 
                        break;

                    default:
                        break;
                }
            }
 */