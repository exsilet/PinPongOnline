using UnityEngine;

namespace CodeBase.Photon
{
    public class ExitRoom : MonoBehaviour
    {
        private const string MenuScene = "MenuScene";

        private void Awake()
        {
            
        }
        
        public void LeaveRoom()
        {
            //PhotonNetwork.LeaveRoom();
        }

        public void OnLeftRoom()
        {
            //base.OnLeftRoom()
        }
    }
}