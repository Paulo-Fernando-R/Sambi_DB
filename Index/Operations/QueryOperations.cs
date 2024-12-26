using db.Index.Exceptions;
using db.Index.Expressions;
using db.Models;
using db.Presenters.Requests;
using db.Presenters.Responses;

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

        public List<QueryByPropertyResponse> QueryByPropertyOpAND(string operatorType, QueryByPropertiesRequest request)
        {
            var sTree = new SearchTree(request.CollectionName + ".zip");
      
            var res = sTree.SearchByProperty(operatorType, request.QueryConditions);
            var list = new List<QueryByPropertyResponse>();

            foreach (var item in res)
            {
                Console.WriteLine(item);
                var obj = new QueryByPropertyResponse() { Data = item.DynamicKeys(), Id = item.Id };
                list.Add(obj);

            }

            return list;

        }


    }
}