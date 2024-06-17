using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider
    {
        public T Get<T>() where T : MonoBehaviour
        {
            if (Resources.Load(typeof(T).Name) == null)
                throw new Exception("Asset not found: " + typeof(T).Name);

            var load =
                Resources
                    .Load(typeof(T).Name)
                    .GetComponent<T>();

            if (load == null)
                throw new Exception("Asset not found: " + typeof(T).Name);

            return load;
        }

        public T Get<T>(string path) where T : MonoBehaviour
        {
            var load =
                Resources
                    .Load(path)
                    .GetComponent<T>();

            if (load == null)
                throw new Exception("Asset not found: " + path);

            return load;
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            if (Resources.Load(typeof(T).Name) == null)
                throw new Exception("Asset not found: " + typeof(T).Name);

            var load =
                Resources
                    .Load(typeof(T).Name);

            if (load == null)
                throw new Exception("Asset not found: " + typeof(T).Name);

            return load as T;
        }

        // public EnemyMeshModel GetEnemyMeshModel(EnemyTypeId enemyId) =>
        //     Resources.Load<EnemyMeshModel>(enemyId.ToString());

        public GameObject Get(string path)
        {
            Object load =
                Resources
                    .Load(path);

            if (load == null)
                throw new Exception("Asset not found: " + path);

            return load as GameObject;
        }
    }
}