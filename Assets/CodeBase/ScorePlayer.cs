using UnityEngine;
using UnityEngine.Events;

namespace CodeBase
{
    public class ScorePlayer : MonoBehaviour
    {
        [SerializeField] private BallMovement _ball;

        private int _scoreEnemy;
        public int ScoreEnemy => _scoreEnemy;

        public event UnityAction<int> TextChanged;

        public void Construct(BallMovement ball)
        {
            _ball = ball;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<BallBounce>())
            {
                _scoreEnemy++;
                _ball.PlayerStart = false;
                TextChanged?.Invoke(_scoreEnemy);
                StartCoroutine(_ball.Launch());
            }
        }
    }
}
