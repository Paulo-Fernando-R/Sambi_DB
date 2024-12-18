using Newtonsoft.Json;
namespace db.Models
{
    public class SearchTreeNode
    {
        public string Id { get; set; }
        public bool IsRoot { get; set; }
        public bool isLeaf { get; set; }
        public List<String> Keys { get; set; }
        public List<String> ChildrenIds { get; set; }
        public int degree { get; set; }

        public SearchTreeNode(bool isLeaf, bool isRoot, int degree)
        {
            this.isLeaf = isLeaf;
            this.isLeaf = isRoot;
            this.degree = degree;
            Keys = new List<String>();
            ChildrenIds = new List<String>();
            Id = Guid.NewGuid().ToString();

        }

        public dynamic DynamicKeys()
        {
            string keys = JsonConvert.SerializeObject(Keys);
            dynamic converted = JsonConvert.DeserializeObject<dynamic>(keys);
            return converted;
        }

        public string Serialize() => JsonConvert.SerializeObject(this);

        // Desserializa um nó de JSON
        public static SearchTreeNode Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<SearchTreeNode>(json);
        }


    }
}
