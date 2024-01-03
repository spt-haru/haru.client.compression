using System.Linq;
using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Converters;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class SimpleCompressToBytes2 : APatch
    {
        public SimpleCompressToBytes2() : base()
        {
            Id = "com.Haru.Client.Compression.simplecompresstobytes2";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(SimpleZlib).GetMethods()
                .Single( x => x.Name == nameof(SimpleZlib.CompressToBytes)
                    && x.GetParameters()[0].GetType() == typeof(byte[]));
        }

        protected static bool Patch(ref byte[] __result, string text)
        {
            var utf8 = Utf8.ToBytes(text);

            __result = MemoryZlib.Compress(utf8, 9).ToArray();
            return false;
        }
    }
}