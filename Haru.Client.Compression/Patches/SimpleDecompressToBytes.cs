using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class SimpleDecompressToBytes: APatch
    {
        public SimpleDecompressToBytes() : base()
        {
            Id = "com.Haru.Client.Compression.simpledecompresstobytes";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(SimpleZlib).GetMethod(nameof(SimpleZlib.DecompressToBytes));
        }

        protected static bool Patch(ref byte[] __result, byte[] bytes)
        {
            __result = MemoryZlib.Decompress(bytes).ToArray();
            return false;
        }
    }
}