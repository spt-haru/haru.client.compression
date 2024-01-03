using System.Linq;
using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Converters;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class SimpleDecompress : APatch
    {
        public SimpleDecompress() : base()
        {
            Id = "com.Haru.Client.Compression.simpledecompress";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(SimpleZlib).GetMethods()
                .Single( x => x.Name == nameof(SimpleZlib.Decompress)
                    && x.GetParameters()[0].GetType() == typeof(byte[]));
        }

        protected static bool Patch(ref string __result, byte[] bytes)
        {
            var utf8 = MemoryZlib.Decompress(bytes).ToArray();

            __result = Utf8.ToString(utf8);
            return false;
        }
    }
}