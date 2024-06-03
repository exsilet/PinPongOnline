using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Skill
{
    [CreateAssetMenu(fileName = "SkillActive", menuName = "StaticData/Skill/MirrorSkill")]
    public class MirrorSkill : SkillStaticData
    {
        private bool _positionRight;
        
        public override void Activate(GameObject parent, bool positionRight)
        {
            _positionRight = positionRight;
        }
    }
}