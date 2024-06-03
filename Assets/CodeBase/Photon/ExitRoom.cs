using CodeBase.Infrastructure.LevelLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
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
            //PhotonNetwork.LeaveRoom();
        }

        public void OnLeftRoom()
        {
            //base.OnLeftRoom();
            
            _stateMachine.Enter<LoadMenuState, string>(MenuScene);
        }
    }
}