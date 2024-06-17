using CodeBase.Infrastructure.Ball;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.UI
{
    public class ScoreEnemy : MonoBehaviour
    {
        [SerializeField] private BallMovet _ball;

        private int _scorePlayer;

        public int ScorePlayer => _scorePlayer;
        
        public event UnityAction<int> TextChanged;

        public void Construct(BallMovet ball)
        {
            _ball = ball;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallMovet>())
            {
                _scorePlayer++;
                TextChanged?.Invoke(_scorePlayer);
            }
        }
    }
}
