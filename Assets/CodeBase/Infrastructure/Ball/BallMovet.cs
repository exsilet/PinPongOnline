using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.UI;
using Fusion;
using Fusion.Addons.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallMovet : NetworkBehaviour
    {
        [SerializeField] private float _startSpeed = 6;
        [SerializeField] private float _extraSpeed;
        [SerializeField] private int _maxSpeed;
        [SerializeField] private int _fireSpeed;
        [SerializeField] private int _swiperSpeed;
        //[SerializeField] private PhotonView _photonView;

        private Rigidbody2D _rigidbody;
        private NetworkRigidbody2D _networkRigidbody;
        private float _currentSpeed;
        private Vector2 _startPosition;
        private Vector2 _goalDirection;
        private bool _ballInGoal = false;

        public void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _networkRigidbody = GetComponent<NetworkRigidbody2D>();
            //_photonView = GetComponent<PhotonView>();

            _startPosition = _rigidbody.position;

            StartMove();

            // if (_photonView.IsMine)
            //     _photonView.RPC(nameof(SyncBallStart), RpcTarget.All, _rigidbody.position, _rigidbody.velocity);
        }

        public void FixedUpdate()
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * _currentSpeed;
        }

        public void Update()
        {
            if (_ballInGoal)
            {
                _rigidbody.velocity = _goalDirection * _currentSpeed;
                _ballInGoal = false;

                // if (_photonView.IsMine)
                //     photonView.RPC(nameof(SyncBallLaunchInGoalDirection), RpcTarget.AllBuffered, _rigidbody.velocity);
            }
        }

        public override void Spawned()
        {
            _networkRigidbody.Teleport(transform.position);
            _rigidbody.position = transform.position;
            _rigidbody.velocity = Vector2.zero;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                Vector2 reflectDirection =
                    Vector2.Reflect(_rigidbody.velocity, collision.contacts[0].normal).normalized;

                _currentSpeed += _extraSpeed;

                _rigidbody.velocity = reflectDirection * _currentSpeed;

                // if (_photonView.IsMine)
                //     _photonView.RPC(nameof(SyncBallReflection), RpcTarget.All, _rigidbody.velocity);
            }

            if (collision.gameObject.GetComponent<ScoreEnemy>())
            {
                ResetBall();
            }
            else if (collision.gameObject.GetComponent<ScorePlayer>())
            {
                ResetBall();
            }
        }

        // [PunRPC]
        // private void SyncBallStart(Vector2 position, Vector2 velocity)
        // {
        //     _rigidbody.position = position;
        //     _rigidbody.velocity = velocity;
        // }

        // [PunRPC]
        // private void SyncBallLaunchInGoalDirection(Vector2 launchDirection)
        // {
        //     _rigidbody.velocity = launchDirection;
        // }

        private void StartMove()
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            _rigidbody.velocity = randomDirection * _startSpeed;
            _currentSpeed = _startSpeed;
        }

        // [PunRPC]
        // private void SyncBallReflection(Vector2 velocity)
        // {
        //     _rigidbody.velocity = velocity;
        // }

        private void ResetBall()
        {
            _goalDirection = -transform.position.normalized;
            _ballInGoal = true;
            _currentSpeed = _startSpeed;

            _rigidbody.position = _startPosition;
            _rigidbody.velocity = Vector2.zero;

            // if (_photonView.IsMine)
            //     photonView.RPC(nameof(SyncBallResetPosition), RpcTarget.AllBuffered, _rigidbody.position);
        }

        // [PunRPC]
        // private void SyncBallResetPosition(Vector2 position)
        // {
        //     _rigidbody.position = position;
        // }

        public void SkillFire()
        {
            _currentSpeed = _fireSpeed;
        }

        public void SkillPowerDamage(int direction)
        {
            _currentSpeed = _swiperSpeed;
            Vector2 forwardDirection = new Vector2( direction, 0);
            _rigidbody.velocity = forwardDirection * _currentSpeed;
        }
    }
}