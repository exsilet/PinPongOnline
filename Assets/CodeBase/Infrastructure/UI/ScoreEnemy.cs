using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.UI
{
    public class ScoreEnemy : MonoBehaviour
    {
        [SerializeField] private BallMovement _ball;

        private int _scorePlayer;

        public int ScorePlayer => _scorePlayer;
        
        public event UnityAction<int> TextChanged;

        public void Construct(BallMovement ballMovement)
        {
            _ball = ballMovement;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallBounce>())
            {
                _scorePlayer++;
                _ball.PlayerStart = true;
                TextChanged?.Invoke(_scorePlayer);
                StartCoroutine(_ball.Launch());
            }
        }
    }
}
