using CodeBase.Infrastructure.Ball;
using UnityEngine;

namespace CodeBase.Infrastructure.Skill
{
    public class MirrorActivated : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.TryGetComponent(out BallMovet ball))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
