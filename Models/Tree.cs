//using System.Text.Json;
using Newtonsoft.Json;

using System.IO.Compression;


namespace db.Models
{




    public class BTree
    {
        private const string FileName = "btree_storage.zip"; // Arquivo de armazenamento
        private readonly int _degree; // Grau da árvore B
        private BTreeNode root; // Raiz da árvore
        private Dictionary<string, BTreeNode> nodes; // Cache de nós, mapeando ID para nó

        public BTree(int degree)
        {
            _degree = degree;
            nodes = new Dictionary<string, BTreeNode>(); // Inicializa o cache de nós
            Load(); // Carrega os dados do arquivo ao iniciar
        }

        // Método para carregar os dados ao iniciar
        private void Load()
        {
            if (File.Exists(FileName))
            {
                using (var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read))
                using (var zip = new ZipArchive(fs, ZipArchiveMode.Read))
                {
                    foreach (var entry in zip.Entries)
                    {
                        using (var reader = new StreamReader(entry.Open()))
                        {
                            var jsonData = reader.ReadToEnd();
                            var node = JsonConvert.DeserializeObject<BTreeNode>(jsonData);
                            nodes[node.Id] = node; // Adiciona o nó ao cache
                        }
                    }

                    // Reconstruir a árvore a partir dos dados carregados
                    root = nodes.Values.FirstOrDefault(n => n.IsRoot); // Supondo que você tenha uma flag ou forma de identificar a raiz
                }
            }
            else
            {
                // Caso o arquivo não exista, inicializa a raiz
                root = new BTreeNode(true) { IsRoot = true }; // Inicializa uma árvore vazia com uma raiz
            }
        }

        // Método para salvar o nó
        private void SaveNode(string id, BTreeNode node)
        {
            using (var fs = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            using (var zip = new ZipArchive(fs, ZipArchiveMode.Update))
            {
                // Verifica se o nó já foi salvo
                if (!nodes.ContainsKey(id))
                {
                    var entry = zip.CreateEntry(id);
                    using (var writer = new StreamWriter(entry.Open()))
                    {
                        var jsonData = JsonConvert.SerializeObject(node);
                        writer.Write(jsonData);
                    }
                    nodes[id] = node; // Armazena o nó no cache
                }
            }
        }

        // Função para inserção
        public void Insert(string data)
        {
            if (root.Keys.Count == _degree - 1)
            {
                // Se a árvore estiver cheia, divide a raiz
                var newRoot = new BTreeNode(false) { IsRoot = true };
                newRoot.ChildrenIds.Add(root.Id);
                SplitChild(newRoot, 0, root);
                root = newRoot; // Atualiza a raiz
            }

            InsertNonFull(root, data);
        }

        // Função para inserção em um nó não cheio
        private void InsertNonFull(BTreeNode node, string data)
        {
            int i = node.Keys.Count - 1;

            // Se o nó é folha, insere a chave diretamente
            if (node.IsLeaf)
            {
                node.Keys.Add(null); // Cria um espaço para a nova chave
                while (i >= 0 && string.Compare(data, node.Keys[i]) < 0)
                {
                    node.Keys[i + 1] = node.Keys[i];
                    i--;
                }
                node.Keys[i + 1] = data; // Insere a chave no lugar adequado
            }
            else
            {
                // Se o nó não for folha, encontra o filho apropriado
                while (i >= 0 && string.Compare(data, node.Keys[i]) < 0)
                {
                    i--;
                }
                i++;

                // Carrega o filho
                var childNodeId = node.ChildrenIds[i];
                var childNode = LoadNode(childNodeId);

                // Se o filho estiver cheio, divide-o
                if (childNode.Keys.Count >= _degree - 1)
                {
                    SplitChild(node, i, childNode);
                    if (string.Compare(data, node.Keys[i]) > 0)
                        i++;
                }

                // Recursivamente insere no filho
                InsertNonFull(LoadNode(node.ChildrenIds[i]), data);
            }

            // Salva o nó depois da inserção
            SaveNode(node.Id, node);  // Salva o nó após inserção
        }

        // Função para dividir um nó
        private void SplitChild(BTreeNode parent, int index, BTreeNode child)
        {
            var newNode = new BTreeNode(child.IsLeaf);
            int medianIndex = _degree / 2;

            // Move as chaves para o novo nó
            newNode.Keys.AddRange(child.Keys.GetRange(medianIndex + 1, child.Keys.Count - medianIndex - 1));
            child.Keys.RemoveRange(medianIndex, child.Keys.Count - medianIndex);

            // Se não for folha, move os filhos também
            if (!child.IsLeaf)
            {
                newNode.ChildrenIds.AddRange(child.ChildrenIds.GetRange(medianIndex + 1, child.ChildrenIds.Count - medianIndex - 1));
                child.ChildrenIds.RemoveRange(medianIndex + 1, child.ChildrenIds.Count - medianIndex - 1);
            }

            // Atualiza o nó pai
            if (medianIndex < child.Keys.Count)
                parent.Keys.Insert(index, child.Keys[medianIndex]);
            else if (child.Keys.Count > 0)
                parent.Keys.Insert(index, child.ChildrenIds[0]);
            parent.ChildrenIds.Insert(index + 1, newNode.Id);

            // Salva os nós modificados
            SaveNode(child.Id, child);  // Salva o nó filho modificado
            SaveNode(newNode.Id, newNode); // Salva o novo nó
            SaveNode(parent.Id, parent); // Salva o nó pai modificado
        }

        // Função para carregar um nó pelo ID
        private BTreeNode LoadNode(string id)
        {
            if (!nodes.ContainsKey(id))
                throw new Exception("Node not found");

            return nodes[id];
        }
    }


}
