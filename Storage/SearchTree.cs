using db.Index.Enums;
using db.Index.Exceptions;
using db.Index.Expressions;
using db.Presenters.Requests;
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

        public SearchTree(String fileName)
        {
            FileName = $"{fileName}.zip";
            Degree = 2;

            var read = ReadNode("Root");
            if (read != null)
            {
                Root = read;
            }
            else
            {
                Root = new SearchTreeNode(true, true, Degree) { Id = "Root" };
            }
        }

        public void WriteNode(SearchTreeNode node)
        {

            string serializedNode = node.Serialize();

            /* if(!File.Exists(FileName))
             {
                 using (var archive = ZipFile.Open(FileName, ZipArchiveMode.Create)) { }
             }*/

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

        public List<SearchTreeNode> SearchByProperty(string operatorType, List<QueryByPropertiesConditions> conditions)
        {
            var nodes = ReadNodes(1);
            var list = new List<SearchTreeNode>();

            /*foreach (var node in nodes)
            {
                var item = new JObject(node.DynamicKeys());

                if (DynamicOperatorMapper.ExecuteAllConditions(item, "&&", conditions))
                {
                    list.Add(item);
                }
            }*/

            list = nodes.Where(x =>
            {
                var item = new JObject(x.DynamicKeys());

                return DynamicOperatorMapper.ExecuteAllConditions(item, operatorType, conditions);
            }).ToList();

            return list;

            /* for (int i = 0; i < conditions.Count; i++)
             {
                 var a = nodes.Where(x =>
                 {
                     var y = x.DynamicKeys();
                     var condition = DynamicOperatorMapper.GetOperation(conditions[i].Operation);

                     return condition(y, conditions[i].Key, conditions[i].Value, conditions[i].Operation);

                 }).ToList();
             }

             return list;

             foreach (SearchTreeNode node in nodes)
             {
                 list.Add(node.DynamicKeys());

             }



             for (int i = 0; i < conditions.Count; i++)
             {
                 var condition = DynamicOperatorMapper.GetOperation(conditions[i].Operation);
                 list = list.Where(x => condition(x, conditions[i].Key, conditions[i].Value, conditions[i].Operation)).ToList();

             }

             return list;*/


        }

        private bool testc(dynamic x, string property, string value, OperatorsEnum operation)
        {
            var a = new List<string>() { "==", "!=" };
            //  .Where(d => d[property] != null && (object)d[property] != value);
            return (float)x[property] > float.Parse(value);
            return true;
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

        public SearchTreeNode SearchById(string registerId)
        {

            try
            {
                var node = ReadNode(registerId);
                return node;
            }
            catch (System.IO.FileNotFoundException ex)
            {
                throw new InternalServerErrorException($"Collection {FileName} not exists.");
            }
            /*catch (System.IO.DirectoryNotFoundException)
            {
                throw new NotFoundException($"Register {registerId} not found.");
            }*/
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex.Message);
            }
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
