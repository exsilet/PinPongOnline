using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    public class PlayerInputHandle : MonoBehaviour
    {
        private const string Vertical = "Vertical";
        private Vector2 _inputVector;
        
        private void Update()
        {
            _inputVector = new Vector2(0, Input.GetAxisRaw(Vertical));
        }

        public NetworkInputData GetNetworkInput()
        {
            NetworkInputData networkInputData = new NetworkInputData();
            networkInputData.Direction = _inputVector;

            return networkInputData;
        }
    }
}