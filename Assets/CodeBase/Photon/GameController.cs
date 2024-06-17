using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.Infrastructure.Player;
using Fusion;
using TMPro;
using UnityEngine;

namespace CodeBase.Photon
{
    public class GameController : NetworkBehaviour ,IPlayerJoined
    {
        [SerializeField] private float _startDelay = 4.0f;
        [SerializeField] private float _endDelay = 4.0f;
        [SerializeField] private TextMeshProUGUI _startEndDisplay;
        
        [Networked] private TickTimer Timer { get; set; }
        [Networked] private GamePhase Phase { get; set; }
        [Networked] private NetworkBehaviourId Winner { get; set; }
        
        private TickTimer _dontCheckforWinTimer;
        
        private List<NetworkBehaviourId> _playerDataNetworkedIds = new List<NetworkBehaviourId>();

        private void Awake()
        {
            GetComponent<NetworkObject>().Flags |= NetworkObjectFlags.MasterClientObject;
        }

        public override void Spawned()
        {
            if (Object.HasStateAuthority)
            {
                Phase = GamePhase.Starting;
                Timer = TickTimer.CreateFromSeconds(Runner, _startDelay);
            }
        }
        
        public override void Render()
        {
            switch (Phase)
            {
                case GamePhase.Starting:
                    UpdateStartingDisplay();
                    break;
                case GamePhase.Running:
                    UpdateRunningDisplay();
                    if (HasStateAuthority)
                    {
                        CheckIfGameHasEnded();
                    }
                    break;
                case GamePhase.Ending:
                    UpdateEndingDisplay();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private void UpdateStartingDisplay()
        {
            _startEndDisplay.text = $"Game Starts In {Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0)}";

            if (!Object.HasStateAuthority) 
                return;

            if (!Timer.Expired(Runner)) 
                return;
            
            FindObjectOfType<Spawner>().StartRocketSpawner();
            
            Phase = GamePhase.Running;
        }
        
        private void UpdateRunningDisplay()
        {
            _startEndDisplay.gameObject.SetActive(false);
        }
        
        private void UpdateEndingDisplay()
        {
            if (Runner.TryFindBehaviour(Winner, out PlayerDataNetworked playerData) == false) return;

            _startEndDisplay.gameObject.SetActive(true);
            _startEndDisplay.text = $"{playerData.NickName} won with {playerData.Score} points. Disconnecting in {Mathf.RoundToInt(Timer.RemainingTime(Runner) ?? 0)}";
            
            if (Timer.Expired(Runner))
                Runner.Shutdown();
        }

        private void CheckIfGameHasEnded()
        {
            if (Timer.ExpiredOrNotRunning(Runner))
            {
                GameHasEnded();
                return;
            }
            
            if (_dontCheckforWinTimer.Expired(Runner) == false)
            {
                return;
            }
            
            if (Runner.ActivePlayers.Count() == 1) return;

            foreach (var playerDataNetworkedId in _playerDataNetworkedIds)
            {
                if (Runner.TryFindBehaviour(playerDataNetworkedId,
                        out PlayerDataNetworked playerDataNetworkedComponent) ==
                    false) continue;

                if (playerDataNetworkedComponent.Score < 5 == false) continue;

                Winner = playerDataNetworkedId;
            }

            if (Winner == default)
            {
                Winner = _playerDataNetworkedIds[0];
            }

            GameHasEnded();
        }
        
        private void GameHasEnded()
        {
            Timer = TickTimer.CreateFromSeconds(Runner, _endDelay);
            Phase = GamePhase.Ending;
        }
        
        
        public void PlayerJoined(PlayerRef player)
        {
            if (player == Object.InputAuthority)
                Runner.Despawn(Object);
        }
    }
    
    public enum GamePhase
    {
        Starting,
        Running,
        Ending
    }
}
