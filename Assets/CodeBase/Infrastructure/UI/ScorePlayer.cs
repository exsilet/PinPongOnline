using CodeBase.Infrastructure.Ball;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.UI
{
    public class ScorePlayer : MonoBehaviour
    {
        [SerializeField] private BallMovet _ball;

        private int _scoreEnemy;
        public int ScoreEnemy => _scoreEnemy;

        public event UnityAction<int> TextChanged;

        public void Construct(BallMovet ball)
        {
            _ball = ball;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallMovet>())
            {
                _scoreEnemy++;
                //_ball.PlayerStart = false;
                TextChanged?.Invoke(_scoreEnemy);
                //StartCoroutine(_ball.Launch());
            }
        }
    }
}
