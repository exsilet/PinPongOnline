using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.Player;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Photon
{
    [RequireComponent(typeof(NetworkRunner))]
    public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPrefabRef _playerPrefab;
        [SerializeField] private BallMovet _ball;
        [SerializeField] private GameObject[] _spawnPoints;

        private NetworkRunner _networkRunner;
        private int _index;

        private void Awake()
        {
            _networkRunner = GetComponent<NetworkRunner>();
        }

        private void Start()
        {
            InitializeNetworkRunner(_networkRunner, GameMode.AutoHostOrClient, NetAddress.Any(),
                SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex), null);
        }

        protected virtual Task InitializeNetworkRunner(NetworkRunner runner, GameMode gameMode, NetAddress address,
            SceneRef scene, Action<NetworkRunner> initialized)
        {
            runner.ProvideInput = true;

            return runner.StartGame(new StartGameArgs
            {
                GameMode = gameMode,
                Address = address,
                Scene = scene,
                CustomLobbyName = "OrLobbyID",
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
        }


        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                _index = player.PlayerId % _spawnPoints.Length;
                var spawnPosition = _spawnPoints[_index].transform.localPosition;

                var playerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);

                runner.SetPlayerObject(player, playerObject);

                if (_index > 0)
                {
                    var ball = runner.Spawn(_ball, _ball.transform.localPosition, Quaternion.identity, PlayerRef.None);
                }
            }
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (NetworkPlayer.Local != null)
            {
                PlayerInputHandle playerInputHandle = NetworkPlayer.Local.GetComponent<PlayerInputHandle>();
                if (playerInputHandle != null)
                    input.Set(playerInputHandle.GetNetworkInput());
            }
        }

        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            throw new NotImplementedException();
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key,
            ArraySegment<byte> data)
        {
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }
    }
}