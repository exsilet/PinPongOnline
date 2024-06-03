using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Skill
{
    [CreateAssetMenu(fileName = "SkillActive", menuName = "StaticData/Skill/FireBall")]
    public class FireBall : SkillStaticData
    {
        public override void Activate(GameObject parent)
        {
            var ball = parent.GetComponent<BallMovet>();
            ball.SkillFire();
        }
    }
}