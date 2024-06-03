using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.UI;
using Fusion;
using UnityEngine;

namespace CodeBase.Infrastructure.Ball
{
    public class BallMovement : NetworkBehaviour
    {
        [SerializeField] private float _startSpeed = 6;
        [SerializeField] private float _extraSpeed;
        [SerializeField] private int _maxSpeed;
        
        private float _currentSpeed;
        private bool _ballInGoal = false;
        private Rigidbody2D _rigidbody;
        private Vector2 _startPosition;
        private Vector2 _goalDirection;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentSpeed = _startSpeed;
            _startPosition = _rigidbody.position;
        }

        private void Update()
        {
            if (_ballInGoal)
            {
                ResetBall();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                _currentSpeed += _extraSpeed;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.GetComponent<ScoreEnemy>())
            {
                GoalBall(col);
            }
            else if (col.gameObject.GetComponent<ScorePlayer>())
            {
                GoalBall(col);
            }
        }

        private void GoalBall(Collider2D col)
        {
            _ballInGoal = true;
            _goalDirection = col.gameObject.transform.position - transform.position;
            _rigidbody.velocity = _goalDirection.normalized * _startSpeed;
        }

        private void ResetBall()
        {
            transform.position = _startPosition;
            _rigidbody.velocity = Vector3.zero;
            Invoke("LaunchBall", 2f);
        }

        private void LaunchBall()
        {
            _rigidbody.velocity = _goalDirection.normalized * _startSpeed;
            _ballInGoal = false;
        }
    }
}