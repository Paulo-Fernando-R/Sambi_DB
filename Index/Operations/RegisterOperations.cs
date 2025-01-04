using db.Index.Exceptions;
using db.Models;
using db.Presenters.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace db.Index.Operations
{
    public class RegisterOperations
    {
        private readonly IConfiguration _configuration;
        private readonly string currentDir;
        private readonly string parentFolderName;

        public RegisterOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            currentDir = AppDomain.CurrentDomain.BaseDirectory;
            parentFolderName = _configuration["Databases:FolderName"];
        }

        public async Task Create(string databaseName, RegisterCreateRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);

            var sTree = new SearchTree(collection);

            await sTree.Insert(request.Data.ToString());
        }

        public async Task Delete(string databaseName, RegisterDeleteRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);
            await sTree.DeleteById(request.RegisterId);

        }

        public async Task AddArray(string databaseName, RegisterCreateArrayRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);
            await sTree.AddArray(request);



        }

        public async Task<int> UpdateArray(string databaseName, RegisterUpdateArrayRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);
            int res = await sTree.UpdateArray(request);

            return res;

        }

        public async Task<int> DeleteArray(string databaseName, RegisterDeleteArrayRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            var sTree = new SearchTree(collection);
            int res = await sTree.DeleteArray(request);

            return res;

        }

        public async Task Update(string databaseName, RegisterUpdateRequest request)
        {
            string path = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);

            var sTree = new SearchTree(collection);

            var data = request.Data.ToString();

            if (data == null)
            {
                throw new BadRequestException("'Data' is required");
            }

            var parsed = JsonConvert.DeserializeObject<JObject>(data);

            await sTree.Update(request.RegisterId, parsed);
        }
    }
}
