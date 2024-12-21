using db.Index.Exceptions;
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

        }


    }
}
