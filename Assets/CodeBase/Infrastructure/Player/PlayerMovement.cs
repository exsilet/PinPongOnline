using Photon.Pun;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private const string Vertical = "Vertical";

        [SerializeField] private int _racketSpeed;
        [SerializeField] private int _swapPosition = 10;
        [SerializeField] private float _minPosition;
        [SerializeField] private float _maxPosition;

        private Rigidbody2D _rigidbody;
        private Vector2 _racketDirection;
        private Camera _camera;

        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;

            _rigidbody = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
            
            //photonView.RPC(nameof(SyncPlayerMovement), RpcTarget.AllBuffered, _rigidbody.position, _rigidbody.velocity);
        }

        private void Update()
        {
            if (GetComponent<PhotonView>().IsMine)
            {
                if (Application.isMobilePlatform)
                {
                    ControlInput();
                }
                else
                {
                    PlayerControl();
                }
            }
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _racketDirection * _racketSpeed;
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

        private void PlayerControl()
        {
            _racketDirection = new Vector2(0, Input.GetAxisRaw(Vertical));
        }
        
        [PunRPC]
        private void SyncPlayerMovement(Vector2 position, Vector2 velocity)
        {
            _rigidbody.position = position;
            _rigidbody.velocity = velocity;
        }

        // public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        // {
        //     if (stream.IsWriting)
        //     {
        //         stream.SendNext(transform.position);
        //         stream.SendNext(transform.rotation);
        //         stream.SendNext(_rigidbody.velocity);
        //         stream.SendNext(_rigidbody.angularVelocity);
        //     }
        //     else
        //     {
        //         _racketDirection = (Vector2)stream.ReceiveNext();
        //         _rigidbody.velocity = (Vector2)stream.ReceiveNext();
        //         _rigidbody.angularVelocity = (float)stream.ReceiveNext();
        //         
        //     }
        // }
    }
}