using CodeBase.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        public GameObject Instantiate(string path, string pathPosition);
        public GameObject InstantiateClient(string path, string pathPosition);
        public GameObject InstantiateServer(string path);
    }
}