using CodeBase.Infrastructure.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Ball
{
    public class BallMovet : MonoBehaviour
    {
        [SerializeField] private float _startSpeed = 6;

        private Rigidbody2D _rigidbody;
        private int _hitCounter = 0;
        private readonly float _startPosition = -1.2f;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            StartMovement();
        }

        private void ResetBall(int moved)
        {
            Vector2 direction = new Vector2(moved, 0);
            transform.position = new Vector2(0, _startPosition);
            _rigidbody.velocity = direction * _startSpeed;
            _hitCounter = 0;
        }

        private void StartMovement()
        {
            Vector2 direction = Vector2.left;
            transform.position = new Vector2(0, _startPosition);
            _rigidbody.velocity = direction * _startSpeed;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<ScoreEnemy>())
            {
                ResetBall(1);
            }
            
            if (collision.gameObject.GetComponent<ScorePlayer>())
            {
                ResetBall(-1);
            }
        }
    }
}