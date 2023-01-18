using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SLGame.Modules
{
    public class AssetLoader
    {
        /// <summary>
        /// Asset path in Addressables tab
        /// </summary>
        private string _assetPathID;

        /// <summary>
        /// Cached gameobject after loading
        /// </summary>
        public GameObject _cachedGameObject { get; private set; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="assetPathID">Asset path in Addressables tab</param>
        public AssetLoader(string assetPathID)
        {
            this._assetPathID = assetPathID;
        }



        /// <summary>
        /// Async load gameobject from addressables
        /// </summary>
        /// <param name="assetPathID">Path to file</param>
        /// <returns>GameObject</returns>
        public async Task<GameObject> Load()
        {
            var handle = Addressables.InstantiateAsync(_assetPathID);

            _cachedGameObject = await handle.Task;
            return _cachedGameObject;
        }

        /// <summary>
        /// Turn off gameobject and unload it from RAM
        /// </summary>
        public void Unload()
        {
            if (_cachedGameObject == null)
                return;

            _cachedGameObject.SetActive(false);
            Addressables.ReleaseInstance(_cachedGameObject);
            _cachedGameObject = null;
        }
    }
}
