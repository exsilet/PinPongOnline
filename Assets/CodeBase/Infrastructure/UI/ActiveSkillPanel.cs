using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.Skill;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class ActiveSkillPanel : MonoBehaviour
    {
        [SerializeField] private Button _skill1;
        [SerializeField] private Button _skill2;
        [SerializeField] private ActivateSkill _activateSkill1;
        [SerializeField] private ActivateSkill _activateSkill2;

        private SkillStaticData _skillData1;
        private SkillStaticData _skillData2;
        private PlayerStaticData _playerData;
        private BallMovet _ball;

        public void Construct(PlayerStaticData playerData, SkillStaticData skillData, BallMovet ball,
            Fighter fighter)
        {
            _playerData = playerData;
            _skillData1 = playerData.SkillData;
            _skillData2 = skillData;
            _ball = ball;
            
            _activateSkill1.Construct(playerData.SkillData, ball, fighter, fighter.TargetPosition);
            _activateSkill2.Construct(skillData, ball, fighter, fighter.TargetPosition);
        }

        private void OnEnable()
        {
            _skill1.Add(ActiveSkill1);
            _skill2.Add(ActiveSkill2);
        }

        private void OnDisable()
        {
            _skill1.Remove(ActiveSkill1);
            _skill2.Remove(ActiveSkill2);
        }
        
        private void ActiveSkill1()
        {
            _activateSkill1.Activated();
        }
        
        private void ActiveSkill2()
        {
            _activateSkill2.Activated();
        }
    }
}