using db.Index.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO.Compression;
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

        public void SearchByProperty(string property, string value, OperatorsEnum operation)
        {
            var nodes = ReadNodes(1);
            var list = new List<JObject>();
          

            foreach (SearchTreeNode node in nodes)
            {
                list.Add(node.DynamicKeys());

            }

          
            
            switch (operation)
            {
                /*case "!=":
                      result = list.AsQueryable()
                        .Where(d => d[property] != null && (object)d[property] != value);
                    break;*/
                
                case OperatorsEnum.Equal:
                    var result = list.AsQueryable()
                     .Where(d => d[property] != null && object.Equals((object)d[property], value));

                    foreach (var item in result)
                    {
                        Console.WriteLine(item); // Output: Bob
                    }
                    break;
                case OperatorsEnum.GreaterOrEqualThan:
                     result = list.AsQueryable()
                     .Where(d => d[property] != null && (float)d[property] > float.Parse(property));
                    break;
                default:
                    break;
            }

            /*var result = list.AsQueryable()
                         .Where(d => d["Age"] != null && (int)d["Age"] > 30);


            */

           
           /* foreach (var item in result as Array)
            {
                Console.WriteLine(item); // Output: Bob
            }*/


        }

        public void DeleteById(string id)
        {
            using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Update))
            {
                var entry = archive.GetEntry(id);

                if (entry == null)
                {
                    archive.Dispose();
                    return;
                }

                RemoveChildInParent(id, ReadNode("Root"));

                entry.Delete();
                archive.Dispose();
            }
        }

        private void RemoveChildInParent(string id, SearchTreeNode node)
        {
            if (node.ChildrenIds.Contains(id))
            {
                node.ChildrenIds.Remove(id);
                WriteNode(node);
                return;
            }

            for (int i = 0; i < node.ChildrenIds.Count; i++)
            {
                var newNode = ReadNode(node.ChildrenIds[i]);

                if (newNode == null)
                {
                    return;
                }

                if (newNode.ChildrenIds.Contains(id))
                {
                    newNode.ChildrenIds.Remove(id);
                    WriteNode(newNode);
                    return;
                }
                RemoveChildInParent(id, newNode);
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
