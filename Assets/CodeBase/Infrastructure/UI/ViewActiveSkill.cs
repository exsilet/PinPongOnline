using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class ViewActiveSkill : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _active;

        private int _currentCount;
        private SkillStaticData _skillStaticData;
        private bool _isInitialized;
        
        public event UnityAction<SkillStaticData, ViewActiveSkill> RemovedActiveSkill;

        public SkillStaticData SkillStaticData => _skillStaticData;

        private void Start()
        {
            _active.Add(OnClick);
        }

        private void OnDestroy()
        {
            _active.Remove(OnClick);
        }

        public void Initialize(SkillStaticData skillStaticData)
        {
            _skillStaticData = skillStaticData;
            _icon.sprite = skillStaticData.UIIcon;
            _currentCount = 1;

            _isInitialized = true;
        }

        public void CountSkill()
        {
            if (_currentCount == 0)
                return;
        
            _currentCount -= 1;
        }

        private void OnClick()
        {
            RemovedActiveSkill?.Invoke(_skillStaticData, this);
            _icon.sprite = null;
            _skillStaticData = null;
            _currentCount = 0;
            _isInitialized = false;
        }
    }
}