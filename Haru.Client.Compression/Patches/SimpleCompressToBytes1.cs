using System.Linq;
using System.Reflection;
using ComponentAce.Compression.Libs.zlib;
using Zlib.Managed;
using Haru.Converters;
using Haru.Reflection;

namespace Haru.Client.Compression.Patches
{
    public class SimpleCompressToBytes1 : APatch
    {
        public SimpleCompressToBytes1() : base()
        {
            Id = "com.Haru.Client.Compression.simplecompresstobytes1";
            Type = EPatchType.Prefix;
        }

        protected override MethodBase GetOriginalMethod()
        {
            return typeof(SimpleZlib).GetMethods()
                .Single( x => x.Name == nameof(SimpleZlib.CompressToBytes)
                    && x.GetParameters()[0].GetType() == typeof(string));
        }

        protected static bool Patch(ref byte[] __result, string text)
        {
            var utf8 = Utf8.ToBytes(text);

            __result = MemoryZlib.Compress(utf8, 9).ToArray();
            return false;
        }
    }
}