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

        private SkillStaticData _skillData1;
        private SkillStaticData _skillData2;
        private PlayerStaticData _playerData;

        public void Construct(PlayerStaticData playerData, SkillStaticData skillData)
        {
            _playerData = playerData;
            _skillData1 = playerData.SkillData;
            _skillData2 = skillData;
        }

        private void OnEnable()
        {
            _skill1.Add(OnClick);
            _skill2.Add(OnClick);
        }

        private void OnDisable()
        {
            _skill1.Remove(OnClick);
            _skill2.Remove(OnClick);
        }
        
        private void OnClick()
        {
            
        }
    }
}