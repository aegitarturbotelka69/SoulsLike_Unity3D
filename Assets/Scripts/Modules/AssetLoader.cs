using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SLGame.Modules
{
    public class AssetLoader
    {
        private GameObject _cachedGameObject;

        public async Task<GameObject> Load(string assetPathID)
        {
            var handle = Addressables.InstantiateAsync(assetPathID);

            _cachedGameObject = await handle.Task;
            return _cachedGameObject;
        }


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
