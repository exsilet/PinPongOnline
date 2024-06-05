using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Player;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

namespace CodeBase.Photon
{
    public class Spawner : MonoBehaviour, INetworkRunnerCallbacks
    {
        [SerializeField] private NetworkPlayer _playerPrefab;

        private PlayerInputHandle _playerInputHandle;

        private void Start()
        {
        }


        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
        {
        }

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer)
            {
                Vector3 localPlayer = new Vector3(-7, -1.7f);
                
                runner.Spawn(_playerPrefab, localPlayer, Quaternion.identity, player);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (_playerInputHandle != null && NetworkPlayer.Local != null)
                _playerInputHandle = NetworkPlayer.Local.GetComponent<PlayerInputHandle>();

            if (_playerInputHandle != null) 
                input.Set(_playerInputHandle.GetNetworkInput());
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
            throw new NotImplementedException();
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
            throw new NotImplementedException();
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            throw new NotImplementedException();
        }

        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
        {
            throw new NotImplementedException();
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
            throw new NotImplementedException();
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
            throw new NotImplementedException();
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
            throw new NotImplementedException();
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
            throw new NotImplementedException();
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
            throw new NotImplementedException();
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
            throw new NotImplementedException();
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key,
            ArraySegment<byte> data)
        {
            throw new NotImplementedException();
        }

        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
        {
            throw new NotImplementedException();
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
            throw new NotImplementedException();
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
            throw new NotImplementedException();
        }
    }
}