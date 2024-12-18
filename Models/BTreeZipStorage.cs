using System;
using System.IO;
using System.IO.Compression;

namespace db.Models
{
    public class BTreeZipStorage
    {
        private readonly string _zipFilePath;

        public BTreeZipStorage(string zipFilePath)
        {
            _zipFilePath = zipFilePath;
        }

        public void WriteNode(string nodeId, byte[] data)
        {

            using (var zip = ZipFile.Open(_zipFilePath, ZipArchiveMode.Update))
            {
                var existingEntry = zip.GetEntry(nodeId);
                existingEntry?.Delete(); // Remove a entrada existente, se houver

                var entry = zip.CreateEntry(nodeId);
                using (var stream = entry.Open())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            /*using (var archive = new ZipArchive(File.Open(_zipFilePath, FileMode.OpenOrCreate), ZipArchiveMode.Update))
            {
                var entry = archive.GetEntry(nodeId) ?? archive.CreateEntry(nodeId);
                using var stream = entry.Open();
                stream.SetLength(0); // Limpar o conteúdo antigo
                stream.Write(data, 0, data.Length);
            }*/
        }

        public byte[] ReadNode(string nodeId)
        {

            using (var zip = ZipFile.Open(_zipFilePath, ZipArchiveMode.Read))
            {
                var entry = zip.GetEntry(nodeId);
                if (entry == null)
                    throw new Exception($"Node '{nodeId}' not found.");

                using (var stream = entry.Open())
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    return memoryStream.ToArray();
                }
            }

            /*using (var archive = new ZipArchive(File.Open(_zipFilePath, FileMode.OpenOrCreate), ZipArchiveMode.Read))
            {
                var entry = archive.GetEntry(nodeId);
                if (entry == null) throw new Exception($"Nó {nodeId} não encontrado no armazenamento.");
                using var stream = entry.Open();
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }*/
        }
    }

}
