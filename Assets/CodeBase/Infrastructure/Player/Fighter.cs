using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    [RequireComponent(typeof(PlayerMoney))]
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private PlayerMoney _playerMoney;
        
        private Inventory _inventory;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillPlayerData;
        private SkillStaticData _skillData;

        public void Construct(PlayerStaticData data)
        {
            _playerData = data;
            _skillPlayerData = data.SkillData;
        }
        
        private void Start()
        {
        }
    }
}