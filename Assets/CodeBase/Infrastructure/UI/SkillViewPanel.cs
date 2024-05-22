using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.UI
{
    public class SkillViewPanel : MonoBehaviour
    {
        [SerializeField] private List<ViewActiveSkills> _viewActivePrefab;
        [SerializeField] private Inventory _inventory;

        private bool _isStarted;
        private List<ViewActiveSkills> _skillViews = new();
        private List<SkillStaticData> _skills = new();

        public void Construct(SkillStaticData skillData)
        {
            _skills.Clear();
            _skills.Insert(0, skillData);
        }

        private void OnEnable()
        {
            for (int i = 0; i < _skills.Count; i++)
            {
                _skillViews.Add(_viewActivePrefab[i]);
                _viewActivePrefab[i].Initialize(_skills[i]);
            }
        }

        private void OnDisable()
        {
            _skillViews.Clear();
        }

        public void AddSkills(SkillStaticData data)
        {
            foreach (ViewActiveSkills activeSkill in _viewActivePrefab)
            {
                if (activeSkill.SkillStaticData == null)
                {
                    activeSkill.AddSkillPanel(data);
                }
            }
        }
    }
}