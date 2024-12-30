using db.Index.Exceptions;
using db.Index.Expressions;
using db.Presenters.Requests;
using db.Storage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
namespace db.Models
{
    public class SearchTree
    {
        private readonly string FileName; //= "SeachTreeStorage.zip";
        private readonly int Degree;
        private readonly ZipFileManager zipFileManager;
        public SearchTreeNode Root { get; private set; }

        public SearchTree(String fileName)
        {
            FileName = $"{fileName}.zip";
            zipFileManager = new ZipFileManager(FileName);
            Degree = 2;

            var read = zipFileManager.ReadNode("Root");

            if (read != null)
            {
                Root = read;
            }
            else
            {
                Root = new SearchTreeNode(true, true, Degree) { Id = "Root" };
            }
        }

        public async Task WriteNode(SearchTreeNode node)
        {
            var zip = new ZipFileManager(FileName);

            await zip.WriteNodeAsync(node);
            return;

        }

        private async Task<SearchTreeNode?> ReadNode(string nodeId)
        {
            var res = await zipFileManager.ReadNodeAsync(nodeId);
            return res;
        }

        private async Task<List<SearchTreeNode>?> ReadNodes(int limit)
        {
            var a = await zipFileManager.ReadNodes(limit);
            return a;

        }


        public async Task<List<SearchTreeNode>> SearchByProperty(string operatorType, List<QueryByPropertiesConditions> conditions)
        {
            var nodes = await ReadNodes(1);
            var list = new List<SearchTreeNode>();

            if (nodes == null)
            {
                return list;
            }

            list = nodes.Where(x =>
            {
                var item = new JObject(x.DynamicKeys());
                return DynamicOperatorMapper.ExecuteAllConditions(item, operatorType, conditions);

            }).ToList();

            return list;

        }

        public async Task DeleteById(string id)
        {
            var deleted = await ReadNode(id);
            if (deleted == null)
            {
                throw new NotFoundException($"Register '{id}' not exists");
            }

            await zipFileManager.DeleteNodeAsync(id);
            await RemoveChildInParent(id, Root);

            for (var i = 0; i < deleted.ChildrenIds.Count; i++)
            {
                var readed = await ReadNode(deleted.ChildrenIds[i]);
                if (readed != null)
                    await Relocate(readed);
            }
        }

        private async Task RemoveChildInParent(string id, SearchTreeNode node)
        {
            if (node.ChildrenIds.Contains(id))
            {
                node.ChildrenIds.Remove(id);
                await WriteNode(node);
                return;
            }

            for (int i = 0; i < node.ChildrenIds.Count; i++)
            {
                var newNode = await ReadNode(node.ChildrenIds[i]);

                if (newNode == null)
                {
                    return;
                }

                if (newNode.ChildrenIds.Contains(id))
                {
                    newNode.ChildrenIds.Remove(id);
                    await WriteNode(newNode);
                    return;
                }
                await RemoveChildInParent(id, newNode);
            }
        }

        public async Task<SearchTreeNode?> SearchById(string registerId)
        {

            try
            {
                var node = await ReadNode(registerId);
                return node;
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new InternalServerErrorException($"Collection {FileName} not exists.");
            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex.Message);
            }
        }

        public async Task Update(string id, JObject newData)
        {

            var node = await ReadNode(id);

            if (node == null)
            {
                throw new NotFoundException($"Register '{id}' not exists");
            }

            var oldData = JsonConvert.DeserializeObject<JObject>(node.Keys);
            

            foreach (var item in newData)
            {
                if (oldData.ContainsKey(item.Key))
                {
                    oldData[item.Key] = item.Value;
                }
                else
                {
                    oldData.TryAdd(item.Key, item.Value);
                }

            }
            node.Keys = JsonConvert.SerializeObject(oldData);
            await WriteNode(node);
        }

        public async Task Insert(string jsonData)
        {
            /*if (Root.Keys == string.Empty)
            {
                Root.Keys = jsonData;
                await WriteNode(Root);
                return;
            }*/

            if (Root.ChildrenIds.Count < Degree)
            {
                var newNode = new SearchTreeNode(true, false, Degree);
                newNode.Keys = jsonData;
                await WriteNode(newNode);
                Root.isLeaf = false;
                Root.ChildrenIds.Add(newNode.Id);
                await WriteNode(Root);
                return;
            }

            string? id = await FindNonFullNode(Root.ChildrenIds);

            if (id == null) return;
            //TODO

            var node = await ReadNode(id);

            if (node.Keys == string.Empty)
            {
                node.Keys = jsonData;
                await WriteNode(node);
            }
            else
            {
                var newNode = new SearchTreeNode(true, false, Degree);
                newNode.Keys = jsonData;
                await WriteNode(newNode);
                node.isLeaf = false;
                node.ChildrenIds.Add(newNode.Id);
                await WriteNode(node);
            }

        }

        public async Task Relocate(SearchTreeNode item)
        {

            if (Root.ChildrenIds.Count < Degree)
            {
                var newNode = new SearchTreeNode(true, false, Degree);
                newNode.Keys = item.Keys;
                await WriteNode(newNode);
                Root.isLeaf = false;
                Root.ChildrenIds.Add(newNode.Id);
                await WriteNode(Root);
                return;
            }

            string? id = await FindNonFullNode(Root.ChildrenIds);

            if (id == null) return;
            //TODO

            var node = await ReadNode(id);

            if (node.Keys == string.Empty)
            {
                node.Keys = item.Keys;
                node.Id = item.Id;
                await WriteNode(node);
            }
            else
            {
                var newNode = new SearchTreeNode(true, false, Degree) { Id = item.Id };
                newNode.Keys = item.Keys;
                await WriteNode(newNode);
                node.isLeaf = false;
                node.ChildrenIds.Add(newNode.Id);
                await WriteNode(node);
            }
        }



        private async Task<string?> FindNonFullNode(List<string> nodes)
        {
            string? id = null;

            for (int i = 0; i < nodes.Count; i++)
            {
                var node = await ReadNode(nodes[i]);

                if (node == null)
                {
                    await RemoveChildInParent(nodes[i], Root);
                    continue;
                }

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

                if (id == null)
                {
                    id = await FindNonFullNode(node.ChildrenIds);
                }

                break;
            }
            return id;
        }


    }
}
