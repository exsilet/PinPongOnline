using System;
using CodeBase.Infrastructure.Player;
using UnityEngine;

namespace CodeBase
{
    public class BallBounce : MonoBehaviour
    {
        [SerializeField] private BallMovement _ball;

        private void Bounse(Collision2D collision)
        {
            Vector3 ballPosition = transform.position;
            Vector3 racketPosition = collision.transform.position;

            float racketHeight = collision.collider.bounds.size.y;

            float positionX;

            if (collision.gameObject.GetComponent<PlayerMovement>())
            {
                positionX = 1;
            }
            else
            {
                positionX = -1;
            }

            float positionY = (ballPosition.y - racketPosition.y) / racketHeight;
            
            _ball.IncreaseHitCounter();
            _ball.MoveBoll(new Vector2(positionX, positionY));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.GetComponent<PlayerMovement>())
            {
                Bounse(collision);
            }
        }
    }
}