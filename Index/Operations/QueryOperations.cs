using db.Index.Exceptions;
using db.Models;
using db.Presenters.Requests;
using db.Presenters.Responses;

namespace db.Index.Operations
{
    public class QueryOperations
    {
        private readonly IConfiguration _configuration;
        private readonly string currentDir;
        private readonly string parentFolderName;

        public QueryOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            currentDir = AppDomain.CurrentDomain.BaseDirectory;
            parentFolderName = _configuration["Databases:FolderName"];
        }
        public SearchTreeNode QueryById(string databaseName, QueryByIdRequest request)
        {
            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);

            var sTree = new SearchTree(collection);
            var res = sTree.SearchById(request.RegisterId);

            try
            {
                if (res == null)
                {
                    throw new NotFoundException($"Register '{request.RegisterId}' not found.");
                }
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<QueryByPropertyResponse> QueryByPropertyOpAND(string databaseName, string operatorType, QueryByPropertiesRequest request)
        {
            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);

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