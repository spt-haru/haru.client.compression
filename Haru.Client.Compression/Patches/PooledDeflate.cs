using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Converters;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class PooledDeflate : APatch
    {
        public PooledDeflate() : base()
        {
            Id = "com.Haru.Client.Compression.pooleddeflate";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(Pooled9LevelZLib).GetMethod(nameof(Pooled9LevelZLib.CompressToBytesNonAlloc));
        }

        // note: EFT uses the resultBuffer by reference, ensure the result is stored in there!
        protected static bool Patch(ref int __result, string text, ref byte[] resultBuffer)
        {
            var utf8 = Utf8.ToBytes(text);
            resultBuffer = MemoryZlib.Compress(utf8, 9).ToArray();

            __result = resultBuffer.Length;
            return false;
        }
    }
}