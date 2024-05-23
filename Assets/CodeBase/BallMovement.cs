using System.Collections;
using UnityEngine;

namespace CodeBase
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] private int _startSpeed;
        [SerializeField] private int _extraSpeed;
        [SerializeField] private int _maxSpeed;

        private int _hitCounter = 0;
        private Rigidbody2D _rigidbody;
        private bool _player1Start;
        private readonly float _startPosition = -1.2f;

        public bool PlayerStart
        {
            get => _player1Start;
            set => _player1Start = value;
        }

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            StartCoroutine(Launch());
        }

        public void IncreaseHitCounter()
        {
            if (_hitCounter * _extraSpeed < _maxSpeed)
            {
                _hitCounter++;
            }
        }

        private void RestartBall()
        {
            _rigidbody.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, _startPosition);
        }

        public void MoveBoll(Vector2 direction)
        {
            direction = direction.normalized;

            float bollSpeed = _startSpeed + _hitCounter * _extraSpeed;

            _rigidbody.velocity = direction * bollSpeed;
        }

        public IEnumerator Launch()
        {
            RestartBall();
            _hitCounter = 0;

            yield return new WaitForSeconds(2);

            MoveBoll(_player1Start ? new Vector2(1, 0) : new Vector2(-1, 0));
        }
    }
}