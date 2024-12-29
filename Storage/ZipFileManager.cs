using db.Index.Exceptions;
using db.Models;
using System.IO.Compression;

namespace db.Storage
{
    public class ZipFileManager
    {
        private readonly string Filename;
        private readonly SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim ReadSemaphore = new SemaphoreSlim(1000, 1000);
        private readonly object ReadLock = new object();

        public ZipFileManager(string filename)
        {
            Filename = filename;
        }

        public async Task WriteNodeAsync(SearchTreeNode node)
        {
            await Semaphore.WaitAsync();

            try
            {
                string serializedNode = node.Serialize();

                using (var archive = ZipFile.Open(Filename, ZipArchiveMode.Update))
                {
                    var existingEntry = archive.GetEntry(node.Id);
                    existingEntry?.Delete();

                    var newEntry = archive.CreateEntry(node.Id);

                    using (var entryStram = newEntry.Open())
                    using (var writer = new StreamWriter(entryStram))
                    {
                        await writer.WriteAsync(serializedNode);
                    }
                }

            }
            catch (Exception)
            {

                throw new FileAccessException("Problems adding the register, it may be that demand is too high.");
            }
            finally
            {
                Semaphore.Release();
            }
        }

        public async Task<SearchTreeNode?> ReadNodeAsync(string nodeId)
        {
            await ReadSemaphore.WaitAsync();
            await Semaphore.WaitAsync();
            Semaphore.Release();

            try
            {
                using (var archive = ZipFile.Open(Filename, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry(nodeId);
                    if (entry == null)
                    {
                        return null;
                    }

                    using (var entryStream = entry.Open())
                    using (var reader = new StreamReader(entryStream))
                    {
                        string json = reader.ReadToEnd();
                        return SearchTreeNode.Deserialize(json);
                    }
                }
            }
            catch (Exception)
            {
                throw new FileAccessException("Problems read the register, it may be that demand is too high.");
                throw;
            }
            finally
            {
                ReadSemaphore.Release();
            }



        }

        public SearchTreeNode ReadNode(string nodeId)
        {
            lock (ReadLock) // Bloqueio para múltiplas leituras simultâneas
            {
                using (var archive = ZipFile.Open(Filename, ZipArchiveMode.Read))
                {
                    var entry = archive.GetEntry(nodeId);
                    if (entry == null)
                        return null;

                    using (var entryStream = entry.Open())
                    using (var reader = new StreamReader(entryStream))
                    {
                        string json = reader.ReadToEnd();
                        return SearchTreeNode.Deserialize(json);
                    }
                }
            }
        }

        public async Task<List<SearchTreeNode>?> ReadNodes(int limit = 100)
        {

            await ReadSemaphore.WaitAsync();
            await Semaphore.WaitAsync();

            try
            {
                using (var archive = ZipFile.Open(Filename, ZipArchiveMode.Read))
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
            catch (Exception)
            {
                throw new FileAccessException("Problems read the register, it may be that demand is too high.");
            }

            finally
            {
                Semaphore.Release();
                ReadSemaphore.Release();
            }


            /*
             
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
             */
        }
    }
}
