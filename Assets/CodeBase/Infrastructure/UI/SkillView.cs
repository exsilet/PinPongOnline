using System;
using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class SkillView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _countText;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cover;
        [SerializeField] private Image _icon2;
        [SerializeField] private Button _addSkill;
        [SerializeField] private Button _buySkill;
        [SerializeField] private Button _openPanelViewSkill;
        
        public Button BuySkill => _buySkill;
        
        private bool _isInitialized;
        private bool _isBuy;
        private int _currentCount;
        
        private OpenSkillView _openViewShop;
        private SkillStaticData _skillStaticData;
        
        public int CurrentCount => _currentCount;
        public SkillStaticData SkillStaticData => _skillStaticData;
        
        public event UnityAction<SkillStaticData, SkillView> AddSkillToInventory;
        public event UnityAction<SkillStaticData, SkillView> AddSkillPanel;

        private void Start()
        {
            _buySkill.Add(OnClick);
            _addSkill.Add(AddSkillToPanel);
            _openPanelViewSkill.Add(OpenView);
        }

        private void OnDestroy()
        {
            _buySkill.Remove(OnClick);
            _addSkill.Remove(AddSkillToPanel);
            _openPanelViewSkill.Remove(OpenView);
        }

        public void InitializeViewShop(SkillStaticData skillStaticData, OpenSkillView openSkillView)
        {
            
            _skillStaticData = skillStaticData;
            _icon.sprite = skillStaticData.UIIcon;
            _currentCount = skillStaticData.Count;
            _isBuy = skillStaticData.IsByu;
            _countText.text = _currentCount.ToString();
            _price.text = skillStaticData.Price.ToString();
            _openViewShop = openSkillView;
            
            CountSkill();
            IsBuySkill(skillStaticData);
            
            _isInitialized = true;
        }

        public void OpenView()
        {
            _openViewShop.Activate();
            _openViewShop.Initialize(_skillStaticData, this);
        }

        public void AddSkillBattle()
        {
            _buySkill.Deactivate();
            _addSkill.Activate();
            _isBuy = true;
            
            CountSkill();
        }

        public void AddSkillInventory()
        {
            _isBuy = true;
            _currentCount += 1;
            _countText.text = _currentCount.ToString();
        }

        private void CountSkill()
        {
            if (_currentCount == 0)
            {
                _cover.Deactivate();
                _countText.Deactivate();
            }
            else
            {
                _cover.Activate();
                _countText.Activate();
            }
        }

        private void IsBuySkill(SkillStaticData skillData)
        {
            if (skillData.IsByu == false)
                IsBuySkill();
            else
                AddSkillBattle();
        }

        private void IsBuySkill()
        {
            _buySkill.Activate();
            _addSkill.Deactivate();
        }

        private void AddSkillToPanel()
        {
            AddSkillPanel?.Invoke(_skillStaticData, this);
        }

        private void OnClick()
        {
            Debug.Log(" buy skill");
            AddSkillToInventory?.Invoke(_skillStaticData, this);
        }
    }
}