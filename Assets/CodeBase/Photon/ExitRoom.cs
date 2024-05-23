using CodeBase.Infrastructure.LevelLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using Mirror;
using UnityEngine;

namespace CodeBase.Photon
{
    public class ExitRoom : MonoBehaviour
    {
        private const string MenuScene = "MenuScene";
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }
        
        public void LeaveRoom()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StartHost();
                _stateMachine.Enter<LoadMenuState, string>(MenuScene);
            }
            else if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StopClient();
                _stateMachine.Enter<LoadMenuState, string>(MenuScene);
            }
            else if (NetworkServer.active)
            {
                NetworkManager.singleton.StopServer();
                _stateMachine.Enter<LoadMenuState, string>(MenuScene);
            }
        }
    }
}