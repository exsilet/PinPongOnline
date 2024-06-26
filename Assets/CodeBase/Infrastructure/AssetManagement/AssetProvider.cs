﻿using Photon.Pun;
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

        public GameObject InstantiatePhoton(string path, string pathPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            var prefabPosition = Resources.Load<GameObject>(pathPosition);
            return PhotonNetwork.Instantiate(prefab.name, prefabPosition.transform.position, Quaternion.identity);
        }
        
        public GameObject InstantiatePhoton(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            Vector2 transformPosition = new Vector2(0, 0);
            return PhotonNetwork.Instantiate(prefab.name, transformPosition, Quaternion.identity);
        }
        
        public GameObject InstantiatePhotonRoom(string path, string pathPosition)
        {
            var prefab = Resources.Load<GameObject>(path);
            var prefabPosition = Resources.Load<GameObject>(pathPosition);
            return PhotonNetwork.InstantiateRoomObject(prefab.name, prefabPosition.transform.position, Quaternion.identity);
        }
    }
}