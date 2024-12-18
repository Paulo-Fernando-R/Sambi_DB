namespace db.Models
{
    public class NodeEntry
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public NodeEntry(string key, string value)
        {
            Key = key;
            Value = value;
        }

        override
        public string ToString()
        {
            return "Key: " + Key + " | " + "value: " + Value;
        }
    }

    public class NodeEntries
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<NodeEntry> Entries { get; set; }

        public NodeEntries()
        {
            CreatedAt = DateTime.UtcNow;
            Id = Guid.NewGuid().ToString();
            Entries = new List<NodeEntry>();
        }

        override
        public string ToString()
        {
            string ret = string.Empty;

            foreach (var entry in Entries)
            {
                ret += entry.ToString();
            }

            return ret;
        }
    }

    public class Key
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public Key(int id, string value) { Id = id; Value = value; }
    }
}
