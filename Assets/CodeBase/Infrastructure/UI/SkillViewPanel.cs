using System.Collections.Generic;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.UI
{
    public class SkillViewPanel : MonoBehaviour
    {
        [SerializeField] private List<ViewActiveSkill> _viewActivePrefab;
        [SerializeField] private Inventory _inventory;

        private bool _isStarted;
        private List<ViewActiveSkill> _skillViews = new();
        private List<SkillStaticData> _skills = new();

        private void Start()
        {
            for (int i = 0; i < _skills.Count; i++)
            {
                _skillViews.Add(_viewActivePrefab[i]);
                _viewActivePrefab[i].Initialize(_skills[i]);
            }

            foreach (ViewActiveSkill activeSkills in _viewActivePrefab)
            {
                activeSkills.RemovedActiveSkill += RemovedSkill;
            }
        }

        private void OnDestroy()
        {
            foreach (ViewActiveSkill activeSkills in _viewActivePrefab)
            {
                activeSkills.RemovedActiveSkill -= RemovedSkill;
            }
        }

        public void Construct(SkillStaticData skillData)
        {
            _skills.Clear();
            _skills.Insert(0, skillData);
        }

        public void AddSkills(SkillStaticData data)
        {
            Debug.Log($" add skill panel {data}");
            
            foreach (ViewActiveSkill activeSkill in _viewActivePrefab)
            {
                if (activeSkill.SkillStaticData == null)
                {
                    activeSkill.Initialize(data);
                    _skills.Add(data);
                }
            }
        }

        private void RemovedSkill(SkillStaticData skillStatic, ViewActiveSkill viewActiveSkill)
        {
            for (int i = 0; i < _skills.Count; i++)
            {
                if (_skills[i] == skillStatic)
                {
                    _skills.RemoveAt(i);
                }
            }
            
            for (int i = 0; i < _skillViews.Count; i++)
            {
                if (_skillViews[i] == viewActiveSkill)
                {
                    _skillViews.RemoveAt(i);
                }
            }
        }
    }
}