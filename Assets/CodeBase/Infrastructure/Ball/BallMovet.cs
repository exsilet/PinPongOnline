using System;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.UI;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Ball
{
    public class BallMovet : MonoBehaviour
    {
        [SerializeField] private float _startSpeed = 6;
        [SerializeField] private float _extraSpeed;
        [SerializeField] private int _maxSpeed;

        private Rigidbody2D _rigidbody;
        private readonly float _startPosition = -1.2f;
        private Vector2 _direction;
        private float _currentSpeed;

        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _currentSpeed = _startSpeed;
            StartMovement();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = _direction.normalized * _currentSpeed;
        }

        private void ResetBall(int moved)
        {
            Vector2 direction = new Vector2(moved, 0);
            transform.position = new Vector2(0, _startPosition);
            _rigidbody.velocity = direction * _currentSpeed;
            _currentSpeed = _startSpeed;
        }

        private void StartMovement()
        {
            _direction = new Vector2(Random.Range(-1, -0.5f), Random.Range(-0.5f, 0.5f));
            transform.position = new Vector2(0, _startPosition);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                _direction.x = -_direction.x;

                if (_currentSpeed <= _maxSpeed)
                {
                    _currentSpeed += _extraSpeed;
                }
            }

            if (collision.gameObject.GetComponent<ZoneBoundary>())
            {
                _direction.y = -_direction.y;
            }
            
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