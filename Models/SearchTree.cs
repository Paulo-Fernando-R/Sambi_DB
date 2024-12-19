using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace db.Models
{
    public class SearchTree
    {
        private readonly string FileName; //= "SeachTreeStorage.zip";
        private readonly int Degree;
        public SearchTreeNode Root { get; private set; }

        public SearchTree()
        {
            FileName = "SeachTreeStorage.zip";
            Degree = 2;

            Root = new SearchTreeNode(true, true, Degree) { Id = "Root" };

        }

        public void WriteNode(SearchTreeNode node)
        {

            string serializedNode = node.Serialize();
            using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Update))
            {
                var already = archive.GetEntry(node.Id);

                if (already != null)
                {
                    already.Delete();
                }

                var entry = archive.CreateEntry(node.Id);

                using (var entryStream = entry.Open())
                using (var writer = new StreamWriter(entryStream))
                {
                    writer.Write(serializedNode);
                    writer.Dispose();
                }
                archive.Dispose();

            }

            /*  using (var fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
              using (var zip = new ZipArchive(fs, ZipArchiveMode.Update))
              {
                  // Verifica se o nó já foi salvo
                  // string serializedNode = JsonConvert.SerializeObject(node);
                  string serializedNode = node.Serialize();
                  using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Update))
                  {
                      var entry = archive.CreateEntry(node.Id);
                      using (var entryStream = entry.Open())
                      using (var writer = new StreamWriter(entryStream))
                      {
                          writer.Write(serializedNode);
                      }
                  }
              }*/
        }

        private SearchTreeNode ReadNode(string nodeId)
        {
            using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Read))
            {
                var entry = archive.GetEntry(nodeId);
                if (entry == null)
                    return null;

                using (var entryStream = entry.Open())
                using (var reader = new StreamReader(entryStream))
                {
                    string json = reader.ReadToEnd();
                    // return JsonConvert.DeserializeObject<BTreeNode>(json);
                    reader.Close();
                    return SearchTreeNode.Deserialize(json);
                }
            }
        }

        private List<SearchTreeNode> ReadNodes(int limit)
        {
            using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Read))
            {
                var entries = archive.Entries;
                if (entries == null)
                    return null;

                var nodes = new List<SearchTreeNode>();

                for (int i = 0; i < entries.Count; i++)
                {
                    using (var entryStream = entries[i].Open())
                    using (var reader = new StreamReader(entryStream))
                    {
                        string json = reader.ReadToEnd();
                        // return JsonConvert.DeserializeObject<BTreeNode>(json);
                        reader.Close();
                        nodes.Add(SearchTreeNode.Deserialize(json));

                    }
                }
                archive.Dispose();

                return nodes;

            }
        }

        public void GetKeys()
        {
            var nodes = ReadNodes(1);
            var list = new List<dynamic>();

            foreach (SearchTreeNode node in nodes)
            {
                list.Add(node.DynamicKeys());
            }



            Console.WriteLine(JsonConvert.SerializeObject(list));
        }

        public void SearchByProperty()
        {
            var nodes = ReadNodes(1);
            var list = new List<JObject>();

            foreach (SearchTreeNode node in nodes)
            {
                list.Add(node.DynamicKeys());

            }

            var result = list.AsQueryable()
                         .Where(d => d["Age"] != null && (int)d["Age"] > 30);

            /* var result = list.Where(d =>
             {
                 var dict = (IDictionary<string, object>)d;
                 return dict.ContainsKey("Age") && (int)dict["Age"] > 25;
             });*/

            foreach (var item in result)
            {
                Console.WriteLine(item); // Output: Bob
            }


        }

        public void Insert(string jsonData)
        {
            if (Root.Keys == string.Empty)
            {
                Root.Keys = jsonData;
                WriteNode(Root);
                return;
            }

            if (Root.ChildrenIds.Count < Degree)
            {
                var newNode = new SearchTreeNode(true, false, Degree);
                newNode.Keys = jsonData;
                WriteNode(newNode);
                Root.isLeaf = false;
                Root.ChildrenIds.Add(newNode.Id);
                WriteNode(Root);
                return;
            }




            string id = FindNonFullNode(Root.ChildrenIds);

            if (id == null || id == string.Empty) return;
            //TODO

            var node = ReadNode(id);

            if (node.Keys == string.Empty)
            {
                node.Keys = jsonData;
                WriteNode(node);
            }
            else
            {
                var newNode = new SearchTreeNode(true, false, Degree);
                newNode.Keys = jsonData;
                WriteNode(newNode);
                node.isLeaf = false;
                node.ChildrenIds.Add(newNode.Id);
                WriteNode(node);
            }

        }

        public void SearchById(string id)
        {

            var node = ReadNode(id);
            Console.WriteLine(node.Serialize());
        }

        public void GetAll()
        {
            var nodes = ReadNodes(5);

            Console.WriteLine(JsonConvert.SerializeObject(nodes));
        }

        private string FindNonFullNode(List<string> nodes)
        {
            string id = string.Empty;

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = ReadNode(nodes[i]);

                if (node.Keys == string.Empty)
                {
                    id = node.Id;
                }

                if (node.isLeaf)
                {
                    id = node.Id;
                }

                if (node.ChildrenIds.Count < Degree)
                {
                    id = node.Id;
                }

                if (id == string.Empty)
                {
                    FindNonFullNode(node.ChildrenIds);
                }
            }
            return id;
        }


    }
}
