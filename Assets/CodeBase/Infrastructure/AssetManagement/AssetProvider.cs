using Mirror;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab, at, Quaternion.identity);
        }
        
        public GameObject Instantiate(string path, string pathPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            var prefabPosition = Resources.Load<GameObject>(pathPosition);
            return Object.Instantiate(prefab, prefabPosition.transform.position, Quaternion.identity);
        }

        public GameObject InstantiateClient(string path, string pathPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            var prefabPosition = Resources.Load<GameObject>(pathPosition);
            GameObject client = Object.Instantiate(prefab, prefabPosition.transform.position, Quaternion.identity);
            NetworkServer.Spawn(client);

            return client;
        }
        
        public GameObject InstantiateServer(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            GameObject ball = Object.Instantiate(prefab);
            NetworkServer.Spawn(ball);
            return ball;
        }
        
        // public GameObject InstantiatePhotonRoom(string path, string pathPosition)
        // {
        //     var prefab = Resources.Load<GameObject>(path);
        //     var prefabPosition = Resources.Load<GameObject>(pathPosition);
        //     return PhotonNetwork.InstantiateRoomObject(prefab.name, prefabPosition.transform.position, Quaternion.identity);
        // }
    }
}