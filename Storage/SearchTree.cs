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
        #region Init
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

        #endregion Init


        #region FileAccess
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
        #endregion FileAccess


        #region Search
        public async Task<SearchTreeNode?> SearchById(string registerId)
        {

            try
            {
                var node = await ReadNode(registerId);
                return node;
            }
            catch (System.IO.FileNotFoundException)
            {
                throw new DirectoryNotExistsException(what: "Collection", identification: FileName);
                //throw new InternalServerErrorException($"Collection {FileName} not exists.");

            }
            catch (Exception ex)
            {
                throw new InternalServerErrorException(ex.Message);
            }
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

        #endregion Search


        #region Delete
        public async Task DeleteById(string id)
        {
            var deleted = await ReadNode(id);
            if (deleted == null)
            {
                throw new NotFoundException(what: "Register", identification: id);
                //throw new NotFoundException($"Register '{id}' not exists");
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

        public async Task<int> DeleteArray(RegisterDeleteArrayRequest request)
        {
            var node = await ReadNode(request.RegisterId);
            int affectedItems = 0;

            if (node == null)
            {
                throw new NotFoundException(what: "Register", identification: request.RegisterId, where: request.CollectionName);
                //throw new NotFoundException($"Register '{request.RegisterId}' not exists");
            }

            var oldData = JsonConvert.DeserializeObject<JObject>(node.Keys);

            if (!oldData.ContainsKey(request.ArrayName))
            {
                throw new BadRequestException(identification: request.Property, rule: "not exists", where: request.ArrayName);
                //throw new BadRequestException($"Property '{request.Property}' not exists");
            }

            var arr = oldData[request.ArrayName];

            // TODO Testar se está funcionando by Ferreira
            /* var a = arr.Where(e =>
             {

                 var aux = e[request.Property];

                 if (aux == null)
                 {
                     return false;
                 }
                 if (aux.ToString() == request.Value) { return true; }
                 return false;
             });

             foreach (var item in a)
             {
                 arr[item].Remove();
                 affectedItems++;
             }*/
            //

            for (var i = 0; i < arr.Count(); i++)
            {
                var item = arr[i];
                var aux = item[request.Property];

                if (aux == null)
                {
                    continue;
                }

                if (aux.ToString() == request.Value)
                {
                    arr[i].Remove();
                    affectedItems++;
                }


                Console.WriteLine(aux);
            }

            if (affectedItems == 0)
            {
                return affectedItems;
            }

            oldData[request.ArrayName] = arr;
            node.Keys = oldData.ToString();

            await WriteNode(node);
            return affectedItems;

        }

        #endregion Delete


        #region Update
        public async Task Update(string id, JObject newData)
        {

            var node = await ReadNode(id);

            if (node == null)
            {
                throw new NotFoundException(what: "Register", identification: id);
                // throw new NotFoundException($"Register '{id}' not exists");
            }

            var oldData = JsonConvert.DeserializeObject<JObject>(node.Keys);

            //TODO Alterar foreach para função
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

        public async Task<int> UpdateArray(RegisterUpdateArrayRequest request)
        {
            var node = await ReadNode(request.RegisterId);
            int affectedItems = 0;

            if (node == null)
            {
                throw new NotFoundException(what: "Register", identification: request.RegisterId);
                //throw new NotFoundException($"Register '{request.RegisterId}' not exists");
            }

            var oldData = JsonConvert.DeserializeObject<JObject>(node.Keys);

            if (!oldData.ContainsKey(request.ArrayName))
            {
                throw new BadRequestException(identification: request.Property, rule: "not exists", where: request.ArrayName);
                //throw new BadRequestException($"Property '{request.Property}' not exists");
            }

            var arr = oldData[request.ArrayName];

            for (var i = 0; i < arr.Count(); i++)
            {
                var item = arr[i];
                var aux = item[request.Property];

                if (aux == null)
                {
                    continue;
                }

                if (aux.ToString() == request.Value)
                {
                    item[request.Property] = request.NewValue.ToString();
                    arr[i] = item;
                    affectedItems++;
                }


                Console.WriteLine(aux);
            }

            if (affectedItems == 0)
            {
                return affectedItems;
            }

            oldData[request.ArrayName] = arr;
            node.Keys = oldData.ToString();

            await WriteNode(node);
            return affectedItems;

        }

        #endregion Update


        #region Insert
        public async Task Insert(string jsonData)
        {

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

        public async Task AddArray(RegisterCreateArrayRequest request)
        {
            var node = await ReadNode(request.RegisterId);

            if (node == null)
            {
                throw new NotFoundException(what: "Register", identification: request.RegisterId);
                //throw new NotFoundException($"Register '{request.RegisterId}' not exists");
            }

            var oldData = JsonConvert.DeserializeObject<JObject>(node.Keys);

            if (!oldData.ContainsKey(request.ArrayName))
            {
                oldData.Add(request.ArrayName, new JArray());
            }


            JArray? extracted = oldData[request.ArrayName].ToObject<JArray>();
            JToken? converted = JsonConvert.DeserializeObject<JToken>(request.Data.ToString() ?? "");
            IEnumerable<JToken?> arr = extracted.Append(converted);


            oldData[request.ArrayName] = JToken.FromObject(arr);
            node.Keys = oldData.ToString();

            await WriteNode(node);

        }

        #endregion Insert

    }
}

