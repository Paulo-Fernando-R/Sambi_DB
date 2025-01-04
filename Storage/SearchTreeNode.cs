using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace db.Models
{
    public class SearchTreeNode
    {
        public string Id { get; set; }
        public bool IsRoot { get; set; }
        public bool isLeaf { get; set; }
        public string Keys { get; set; }
        //TODO Mudar keys para dicionario ao inves de string Dicionary<String, dynamic> d = new();
        public List<String> ChildrenIds { get; set; }
        public int degree { get; set; }

        public SearchTreeNode(bool isLeaf, bool isRoot, int degree)
        {
            this.isLeaf = isLeaf;
            this.isLeaf = isRoot;
            this.degree = degree;
            Keys = String.Empty;
            ChildrenIds = new List<String>();
            Id = Guid.NewGuid().ToString();

        }

        public dynamic DynamicKeys()
        {
           
            dynamic converted = JsonConvert.DeserializeObject<dynamic>(Keys);
         
            return converted;
        }

        public string JsonKeys()
        {
            return JsonConvert.SerializeObject(Keys);
        }

        public string Serialize() => JsonConvert.SerializeObject(this);

        // Desserializa um nó de JSON
        public static SearchTreeNode Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SearchTreeNode>(json);
        }


    }
}
