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

        public async Task<SearchTreeNode> QueryById(string databaseName, QueryByIdRequest request)
        {
            // TODO Criar classe util para verificar caminho
            string path = Path.Combine(currentDir, parentFolderName, databaseName);
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);

            var sTree = new SearchTree(collection);
            var res = await sTree.SearchById(request.RegisterId);

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

        public async Task<List<QueryByPropertyResponse>> QueryByProperty(string databaseName, QueryByPropertiesRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);
            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);

            var res = await sTree.SearchByProperty(request.ConditionsBehavior, request.QueryConditions);
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