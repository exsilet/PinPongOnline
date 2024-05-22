using CodeBase.Service;
using Photon.Pun;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string Vertical = "Vertical";
        
        [SerializeField] private int _racketSpeed;

        private Rigidbody2D _rigidbody;
        private Vector2 _racketDirection;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
            _inputService = Game.InputService;
        }

        private void Start()
        {
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 5;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void Update()
        {
            if (GetComponent<PhotonView>().IsMine == true)
            {

                // if (_inputService.Axis.sqrMagnitude > 0.001f)
                // {
                //     _racketDirection = _camera.transform.TransformDirection(_inputService.Axis);
                //     _racketDirection.x = 0;
                // }
            
                // _racketDirection = Vector2.zero;
                // float directionY = Input.GetAxisRaw(Vertical);
                // _racketDirection = new Vector2(0, directionY).normalized;
                PlayerControl();
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _racketDirection * _racketSpeed;
        }

        private void PlayerControl()
        {
            _racketDirection = new Vector2(0, Input.GetAxisRaw(Vertical));
        }
    }
}