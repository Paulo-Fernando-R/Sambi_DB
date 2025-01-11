using db.Index.Exceptions;
using db.Presenters.Requests;
using System.IO.Compression;

namespace db.Index.Operations
{

    public class CollectionOperations
    {
        private readonly IConfiguration _configuration;
        private readonly string currentDir;
        private readonly string parentFolderName;

        public CollectionOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            currentDir = AppDomain.CurrentDomain.BaseDirectory;
            parentFolderName = _configuration["Databases:FolderName"];
        }

        public void Create(string databaseName, CollectionRequest request)
        {
            string databasePath = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(databasePath))
            {
                throw new DirectoryNotExistsException(what: "Database", identification: databaseName);
                //throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string newCollection = Path.Combine(currentDir, parentFolderName, databaseName, request.CollectionName);
            newCollection = $"{newCollection}.zip";

            if (File.Exists(newCollection))
            {
                throw new AlreadyExistsException(what: "Collection", identification: request.CollectionName, where: databaseName);
                //   throw new AlreadyExistsException($"Collection '{request.CollectionName}' already exists");

            }

            using (var archive = ZipFile.Open(newCollection, ZipArchiveMode.Create)) { }

        }

        public void Delete(string databaseName, string collectionName)
        {
            string databasePath = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(databasePath))
            {
                throw new DirectoryNotExistsException(what: "Database", identification: databaseName);
                //throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, collectionName);
            collection = $"{collection}.zip";

            if (!File.Exists(collection))
            {
                throw new DirectoryNotExistsException(what: "Collection", identification: collectionName);
                // throw new DirectoryNotExistsException($"Collection '{collectionName}' not exists");
            }

            File.Delete(collection);



        }

        public void Update(string databaseName, string collectionName, string newCollectionName)
        {
            string databasePath = Path.Combine(currentDir, parentFolderName, databaseName);

            if (!Directory.Exists(databasePath))
            {
                throw new DirectoryNotExistsException(what: "Database", identification: databaseName);
                //throw new DirectoryNotExistsException($"Database '{databaseName}' not exists");
            }

            string collection = Path.Combine(currentDir, parentFolderName, databaseName, collectionName);
            string newCollection = Path.Combine(currentDir, parentFolderName, databaseName, newCollectionName);

            collection = $"{collection}.zip";
            newCollection = $"{newCollection}.zip";

            if (!File.Exists(collection))
            {
                throw new DirectoryNotExistsException(what: "Collection", identification: newCollectionName);
                // throw new DirectoryNotExistsException($"Collection '{collectionName}' not exists");
            }

            if (File.Exists(newCollection))
            {
                throw new AlreadyExistsException(what: "Collection", identification: newCollectionName, where: databaseName);
                // throw new AlreadyExistsException($"Collection '{newCollectionName}' already exists");
            }

            File.Move(collection, newCollection);

        }
    }
}
