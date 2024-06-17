using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        public GameObject Instantiate(string path, string pathPosition);
        GameObject InstantiatePhoton(string path, string pathPosition);
        GameObject InstantiatePhoton(string path);
        public GameObject InstantiatePhotonRoom(string path, string pathPosition);
    }
}