using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Converters;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class PooledInflate : APatch
    {
        public PooledInflate() : base()
        {
            Id = "com.Haru.Client.Compression.pooledinflate";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(Pooled9LevelZLib).GetMethod(nameof(Pooled9LevelZLib.DecompressNonAlloc));
        }

        protected static bool Patch(ref string __result, byte[] compressedBytes)
        {
            var utf8 = MemoryZlib.Decompress(compressedBytes).ToArray();

            __result = Utf8.ToString(utf8);
            return false;
        }
    }
}