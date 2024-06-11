using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.UI
{
    public class Shop : MonoBehaviour
    {
        [SerializeField] private List<SkillStaticData> _staticDatas;
        [SerializeField] private List<SkillView> _skillViewPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private OpenSkillView _openSkillView;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private PlayerMoney _playerMoney;
        
        private int _currentCostOfGold;
        private int _currentMoney;

        private List<SkillView> _skillViews = new();

        private void Start()
        {
            _staticDatas.Insert(0, _inventory.PlayerData.SkillData);

            for (int i = 0; i < _staticDatas.Count; i++)
            {
                _skillViews.Add(_skillViewPrefab[i]);
                _skillViewPrefab[i].InitializeViewShop(_staticDatas[i], _openSkillView);
            }
            
            foreach (SkillView view in _skillViews)
            {
                view.AddSkillToInventory += TrySellBuy;
                view.AddSkillPanel += OnAddSkillPanel;
            }
        }

        private void OnAddSkillPanel(SkillStaticData skillStatic, SkillView skillView)
        {
            _inventory.AddSkillToPanel(skillStatic);
        }

        private void Update()
        {
            _currentMoney = _playerMoney.CurrentMoney;
        }

        private void OnDestroy()
        {
            _staticDatas.RemoveAt(0);
            _skillViews.Clear();
            
            foreach (SkillView view in _skillViews)
            {
                view.AddSkillToInventory -= TrySellBuy;
            }
        }

        public void TrySellBuy(SkillStaticData data, SkillView skillView)
        {
            Debug.Log($"skill data {data}");
            
            if (data == null)
                return;

            _currentCostOfGold = data.Price;

            if (_playerMoney.CurrentMoney <= 0)
                return;

            if (_currentCostOfGold <= _playerMoney.CurrentMoney)
            {
                _playerMoney.BuySkill(data);
                _inventory.BuySkill(data);
                skillView.AddSkillInventory();
                skillView.AddSkillBattle();
            }
        }
    }
}