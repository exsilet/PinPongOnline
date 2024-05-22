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
        private int _currentMoney = 350;

        private List<SkillView> _skillViews = new();

        private void OnEnable()
        {
            _currentMoney = _playerMoney.CurrentMoney;
            
            _staticDatas.Insert(0, _inventory.PlayerData.SkillData);

            for (int i = 0; i < _staticDatas.Count; i++)
            {
                _skillViews.Add(_skillViewPrefab[i]);
                _skillViewPrefab[i].InitializeViewShop(_staticDatas[i], _openSkillView);
                
                // SkillView newItem = Instantiate(_skillViewPrefab, _container);
                // newItem.InitializeViewShop(skillData, _openSkillView);
                // _skillViews.Add(newItem);
            }
            
            foreach (SkillView view in _skillViews)
            {
                view.AddSkillToInventory += TrySellBuy;
            }
        }

        private void OnDisable()
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
            _currentCostOfGold = data.Price;

            if (_currentMoney <= 0)
                return;

            if (_currentCostOfGold <= _currentMoney)
            {
                _playerMoney.BuySkill(data);
                _inventory.BuySkill(data);
                skillView.AddSkillBattle();
                skillView.AddSkillInventory();
            }
        }
    }
}