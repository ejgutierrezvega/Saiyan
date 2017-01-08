using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Saiyan.Repository
{
    /// <summary>
    /// Based on Mongo DB object id specifications.
    /// https://docs.mongodb.com/v3.2/reference/method/ObjectId/
    /// http://api.mongodb.com/java/current/org/bson/types/ObjectId.html
    /// </summary>
    internal static class IdGenerator
    {
        private static int increment;
        private static readonly byte[] machinePid;

        static IdGenerator()
        {
            increment = new Random().Next();
            machinePid = new byte[5];
            using (var a = MD5.Create())
            {
                var h = a.ComputeHash(Encoding.UTF8.GetBytes(Environment.MachineName));
                for (var i = 0; i < 3; i++)
                    machinePid[i] = h[i];
            }

            var pid = Process.GetCurrentProcess().Id;
            machinePid[3] = (byte)(pid >> 8);
            machinePid[4] = (byte)pid;
        }

        internal static Guid NewGuid(long timeStamp)
        {
            var i = Interlocked.Increment(ref increment) & 0x00ffffff;
            var g = new Guid(
                (int)(timeStamp >> 32),
                (short)(timeStamp >> 16),
                (short)timeStamp,
                machinePid.Concat(new[] 
                    {
                        (byte)(increment >> 16),
                        (byte)(increment >> 8),
                        (byte)increment
                    }).ToArray()
                );
            return g;
        }
    }
}
