using Newtonsoft.Json.Linq;

namespace db.Presenters.Requests
{
    public class RegisterCreateRequest
    {
        public required string CollectionName { get; set; }
        public required object Data { get; set; }

        public RegisterCreateRequest()
        {
            CollectionName = string.Empty;
            Data = new object();
        }
    }

   
}
