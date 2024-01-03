using BepInEx;
using Haru.Reflection;
using Haru.Client.Compression.Patches;

namespace Haru.Client.Compression
{
    [BepInPlugin("com.Haru.Client.Compression", "Haru.Client.Compression", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private readonly APatch[] _patches;

        public Plugin()
        {
            _patches = new APatch[]
            {
                new PooledInflate(),
                new PooledDeflate(),
                new SimpleCompressToBytes1(),
                new SimpleCompressToBytes2(),
                new SimpleDecompress(),
                new SimpleDecompressToBytes()
            };
        }

        // used by bepinex
        private void Awake()
        {
            Logger.LogInfo("Loading: Haru.Compression");

            foreach (var patch in _patches)
            {
                patch.Enable();
            }
        }

        // used by bepinex
        private void OnApplicationQuit()
        {
            foreach (var patch in _patches)
            {
                patch.Disable();
            }
        }
    }
}