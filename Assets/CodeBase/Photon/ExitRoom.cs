using CodeBase.Infrastructure.LevelLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using Photon.Pun;

namespace CodeBase.Photon
{
    public class ExitRoom : MonoBehaviourPunCallbacks
    {
        private const string MenuScene = "MenuScene";
        private IGameStateMachine _stateMachine;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }
        
        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            
            _stateMachine.Enter<LoadMenuState, string>(MenuScene);
        }
    }
}