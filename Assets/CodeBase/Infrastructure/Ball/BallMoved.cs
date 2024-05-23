using System;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.UI;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Infrastructure.Ball
{
    public class BallMoved : MonoBehaviour
    {
        [SerializeField] private float _startSpeed = 10;
        [SerializeField] private float _speedIncrease = 0.25f;
        
        private int _hitCounter = 0;
        private Rigidbody2D _rigidbody;
        private Vector2 _direction = Vector2.left;
        private readonly float _startPosition = -1.2f;
        private float _minDirection = 0.5f;

        private void Start()
        {
            PhotonNetwork.SendRate = 20;
            PhotonNetwork.SerializationRate = 15;
            
            _rigidbody = GetComponent<Rigidbody2D>();
            _direction = Vector2.left;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _direction * _startSpeed * Time.fixedDeltaTime);
        }

        private void StartBall()
        {
            _rigidbody.velocity = new Vector2(-1,0) * (_startSpeed + _speedIncrease * _hitCounter);
        }

        private void ResetBall()
        {
            _rigidbody.velocity = new Vector2(0, 0);
            transform.position = new Vector2(0, _startPosition);
            _hitCounter = 0;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponent<PlayerMovement>())
            {
                Vector2 newDirection = (transform.position - col.transform.position).normalized;

                newDirection.x = Math.Sign(newDirection.x) * Math.Max(Math.Abs(newDirection.x), _minDirection);
                newDirection.y = Math.Sign(newDirection.y) * Math.Max(Math.Abs(newDirection.y), _minDirection);

                _direction = newDirection;
            }

            if (col.GetComponent<ScoreEnemy>())
            {
                ResetBall();
            }
            else if (col.GetComponent<ScorePlayer>())
            {
                ResetBall();
            }
        }
    }
}