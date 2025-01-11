using db.Index.Exceptions;
using db.Presenters.Requests;

namespace db.Index.Operations
{
    public class DatabaseOperations
    {
        private readonly IConfiguration _configuration;
        private readonly string currentDir;
        private readonly string parentFolderName;

        public DatabaseOperations(IConfiguration configuration)
        {
            _configuration = configuration;
            currentDir = AppDomain.CurrentDomain.BaseDirectory;
            parentFolderName = _configuration["Databases:FolderName"];
        }

        public void DatabaseCreate(DatabaseCreateRequest request)
        {
            string newFolderPath = Path.Combine(currentDir, parentFolderName, request.DatabaseName);

            if (Directory.Exists(newFolderPath))
            {
                throw new AlreadyExistsException(what: "Database", identification: request.DatabaseName);
                // throw new AlreadyExistsException($"Database '{request.DatabaseName}' already exists");
            }

            Directory.CreateDirectory(newFolderPath);
        }

        public void DatabaseDelete(string databaseName)
        {
            string newFolderPath = Path.Combine(currentDir, parentFolderName, databaseName);

            if (Directory.Exists(newFolderPath))
            {
                Directory.Delete(newFolderPath, true);
                return;
            }
            throw new DirectoryNotExistsException(what: "Database", identification: databaseName);
           // throw new DirectoryNotExistsException($"Database '{databaseName}' does not exist");
        }
    }
}
