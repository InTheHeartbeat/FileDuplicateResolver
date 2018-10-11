using FileDuplicateResolver.Hashing.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using FileDuplicateResolver.Hashing.Models;

namespace FileDuplicateResolver.Hashing
{
    public class SHA265FileHasher : IFileHasher, IObservableHandler<TimeSpan>
    {
        private const int DefaultBufferSize = 32768;

        public event Action<TimeSpan> ActionCompleted;
        public int BufferSize { get; set; }

        private readonly Stopwatch _actionStopwatch;

        public SHA265FileHasher(int bufferSize = DefaultBufferSize)
        {
            BufferSize = bufferSize;
            _actionStopwatch = new Stopwatch();
        }        
        public string GetFileChecksum(Stream file)
        {            
            _actionStopwatch.Restart();            
            string resultCheckSum = ComputeHash(file);
            _actionStopwatch.Stop();

            OnActionCompleted(_actionStopwatch.Elapsed);
            return resultCheckSum;
        }

        private string ComputeHash(Stream data)
        {
            using (SHA256Managed shaHasher = new SHA256Managed())
            {
                using (BufferedStream buffer = new BufferedStream(data, BufferSize))
                {
                    byte[] hash = shaHasher.ComputeHash(buffer);
                    return BitConverter.ToString(hash).Replace("-", string.Empty);
                }
            }
        }


        protected virtual void OnActionCompleted(TimeSpan obj)
        {
            ActionCompleted?.Invoke(obj);
        }
    }
}
