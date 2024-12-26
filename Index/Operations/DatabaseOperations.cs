using db.Index.Exceptions;
using db.Presenters.Requests;

namespace db.Index.Operations
{
    public class DatabaseOperations
    {
        private readonly IConfiguration _configuration;

        public DatabaseOperations(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void DatabaseCreate(DatabaseCreateRequest request)
        {
            string currentDir = AppDomain.CurrentDomain.BaseDirectory;
            string ParentFolderName = _configuration["Databases:FolderName"];
            string newFolderPath = Path.Combine(currentDir, ParentFolderName, request.DatabaseName);

            if (Directory.Exists(newFolderPath))
            {
                throw new AlreadyExistsException($"Database '{request.DatabaseName}' already exists");
            }

            Directory.CreateDirectory(newFolderPath);
        }
    }
}
