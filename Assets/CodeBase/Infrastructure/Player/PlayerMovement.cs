using Fusion;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : NetworkBehaviour
    {
        //private const string Vertical = "Vertical";

        [SerializeField] private int _racketSpeed;
        [SerializeField] private int _swapPosition = 10;
        [SerializeField] private float _minPosition;
        [SerializeField] private float _maxPosition;

        private Rigidbody2D _rigidbody;
        private Vector2 _racketDirection;
        private Camera _camera;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            
            //photonView.RPC(nameof(SyncPlayerMovement), RpcTarget.AllBuffered, _rigidbody.position, _rigidbody.velocity);
        }

        public override void FixedUpdateNetwork()
        {
            // if (HasStateAuthority == false)
            //     return;

            if (GetInput(out NetworkInputData data))
            {
                if (Application.isMobilePlatform)
                {
                    ControlInput();
                }
                else
                {
                    PlayerControl(data.Direction);
                }
            }
            
            _rigidbody.velocity = _racketDirection * _racketSpeed;
        }

        private void Update()
        {
            // if (Application.isMobilePlatform)
            // {
            //     ControlInput();
            // }
            // else
            // {
            //     PlayerControl();
            // }
            
            // if (GetComponent<PhotonView>().IsMine)
            // {
            //     if (Application.isMobilePlatform)
            //     {
            //         ControlInput();
            //     }
            //     else
            //     {
            //         PlayerControl();
            //     }
            // }
        }

        private void ControlInput()
        {
            foreach (Touch touch in Input.touches)
            {
                Vector2 touchPosition = _camera.ScreenToWorldPoint(touch.position);
                Vector2 myPosition = _rigidbody.position;

                if (Mathf.Abs(touchPosition.x - myPosition.x) <= 2)
                {
                    myPosition.y = Mathf.Lerp(myPosition.y, touchPosition.y, _swapPosition);
                    myPosition.y = Mathf.Clamp(myPosition.y, _minPosition, _maxPosition);

                    _rigidbody.position = myPosition;
                }
            }
        }

        private void PlayerControl(Vector2 direction)
        {
            _racketDirection = direction;
            //_racketDirection = new Vector2(0, Input.GetAxisRaw(Vertical));
        }
        
        // [PunRPC]
        // private void SyncPlayerMovement(Vector2 position, Vector2 velocity)
        // {
        //     _rigidbody.position = position;
        //     _rigidbody.velocity = velocity;
        // }
    }
}