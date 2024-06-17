using CodeBase.Infrastructure.Logic.PlayerSpawner;
using Fusion;
using UnityEngine;

namespace CodeBase.Photon
{
    public class Spawner : NetworkBehaviour
    {
        [SerializeField] private NetworkPrefabRef _playerNetworkPrefab;
        [SerializeField] private SpawnPoint[] _spawnPoints;
        [Networked] private bool _gameIsReady { get; set; } = false;

        public override void Spawned()
        {
            if (_gameIsReady)
            {
                SpawnRocket(Runner.LocalPlayer);
            }
        }

        public void StartRocketSpawner()
        {
            _gameIsReady = true;
            RpcInitialRocketSpawn();
        }

        [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
        private void RpcInitialRocketSpawn()
        {
            SpawnRocket(Runner.LocalPlayer);
        }

        private void SpawnRocket(PlayerRef player)
        {
            int index = player.PlayerId % _spawnPoints.Length;
            var spawnPosition = _spawnPoints[index].transform.position;

            var playerObject = Runner.Spawn(_playerNetworkPrefab, spawnPosition, Quaternion.identity, player);
            Runner.SetPlayerObject(player, playerObject);
        }
    }
}