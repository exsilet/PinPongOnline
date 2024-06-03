using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Skill
{
    [CreateAssetMenu(fileName = "SkillActive", menuName = "StaticData/Skill/DirectBlow")]
    public class DirectBlow : SkillStaticData
    {
        private bool _positionRight;
        
        public override void Activate(GameObject parent, bool positionRight)
        {
            var ball = parent.GetComponent<BallMovet>();

            _positionRight = positionRight;

            if (_positionRight)
                ball.SkillPowerDamage(1);
            else
                ball.SkillPowerDamage(-1);
        }
    }
}