using db.Index.Exceptions;
using db.Models;
using db.Presenters.Requests;
using Newtonsoft.Json;

namespace db.Index.Operations
{
    public class RegisterOperations
    {
        private readonly IConfiguration _configuration;
        private readonly string currentDir;
        private readonly string parentFolderName;

        public RegisterOperations (IConfiguration configuration)
        {
            _configuration = configuration;
            currentDir = AppDomain.CurrentDomain.BaseDirectory;
            parentFolderName = _configuration["Databases:FolderName"];
        }

        public void Create(string databaseName, RegisterCreateRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if(!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);

            var sTree = new SearchTree(collection);
          
            sTree.Insert(request.Data.ToString());
        }
    }
}
