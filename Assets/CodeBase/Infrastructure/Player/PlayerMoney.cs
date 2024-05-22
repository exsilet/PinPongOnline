using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Player
{
    public class PlayerMoney : MonoBehaviour
    {
        [SerializeField] private int _currentMoney = 500;
        
        public int CurrentMoney => _currentMoney;
        
        public event UnityAction<int> CurrentSoulChanged;
        
        private void Start()
            => CurrentSoulChanged?.Invoke(_currentMoney);
        
        public void BuySkill(SkillStaticData data)
        {
            if (_currentMoney >= data.Price)
            {
                _currentMoney -= data.Price;
                CurrentSoulChanged?.Invoke(_currentMoney);
            }
        }
        
        public void AddMoney(int reward)
        {
            _currentMoney += reward;
            CurrentSoulChanged?.Invoke(_currentMoney);
        }
    }
}