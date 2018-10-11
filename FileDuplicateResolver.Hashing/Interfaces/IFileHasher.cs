using System.IO;

namespace FileDuplicateResolver.Hashing.Interfaces
{
    public interface IFileHasher
    {
        int BufferSize { get; set; }
        string GetFileChecksum(Stream file);
    }
}