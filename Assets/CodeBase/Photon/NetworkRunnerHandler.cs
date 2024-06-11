using System;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Photon
{
    public class NetworkRunnerHandler : MonoBehaviour
    {
        public NetworkRunner _networkRunnerPrefab;

        private NetworkRunner _networkRunner;

        private void Awake()
        {
            // var networkRunnerInScene = FindObjectOfType<NetworkRunner>();
            //
            // if (networkRunnerInScene != null) 
            //     _networkRunner = networkRunnerInScene;
        }

        private void Start()
        {
            _networkRunner = Instantiate(_networkRunnerPrefab);
            _networkRunner.name = "Network Runner";

            // if (SceneManager.GetActiveScene().name != "GameScene")
            // {
                Task clientTask = InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, "TestSession",
                    NetAddress.Any(),
                    SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
            //}
        }

        protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, string sessionName,
            NetAddress address, SceneRef scene, Action<NetworkRunner> initialized)
        {
            var sceneManager = runner.GetComponents(typeof(MonoBehaviour))
                .OfType<INetworkSceneManager>()
                .FirstOrDefault();

            if (sceneManager == null)
            {
                sceneManager = runner.gameObject.AddComponent<NetworkSceneManagerDefault>();
            }

            runner.ProvideInput = true;

            return runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                Address = address,
                Scene = scene,
                SessionName = sessionName,
                CustomLobbyName = "OrLobbyID",
                SceneManager = sceneManager,
                //ConnectionToken = connectionToken
            });
        }
    }
}