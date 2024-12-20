using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace db.Models
{


    public class BTreeNode
    {
        public string Id { get; set; } // Identificador único para o nó
        public List<string> Keys { get; set; } = new List<string>(); // Chaves armazenadas no nó
        public List<string> ChildrenIds { get; set; } = new List<string>(); // IDs dos filhos
        public bool IsLeaf { get; set; } // Se o nó é folha
        public bool IsRoot { get; set; } // Se o nó é a raiz


        public BTreeNode(bool isLeaf)
        { 
            Id = Guid.NewGuid().ToString(); // Gera um ID único para o nó
            Keys = new List<string>();
            ChildrenIds = new List<string>();
            IsRoot = false;
            IsLeaf = isLeaf;
        }

        // Serializa o nó para JSON
       // public string Serialize() => JsonConvert.SerializeObject(this);

        public string Serialize()
        {
            var data = new
            {
                Id = Id,
                Keys = Keys.ToArray(),
                ChildrenIds = ChildrenIds.ToArray(),
                IsLeaf = IsLeaf,
                IsRoot = IsRoot

            };

            return JsonConvert.SerializeObject(data);
        }

        // Desserializa um nó de JSON
        public static BTreeNode Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<BTreeNode>(json);
        }


    }

}