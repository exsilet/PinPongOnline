using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private SkillViewPanel _skillViewPanel;

        private List<SkillStaticData> _staticDatas = new List<SkillStaticData>();

        private PlayerStaticData _playerData;
        public PlayerStaticData PlayerData => _playerData;

        public void Construct(PlayerStaticData playerData)
        {
            if (_playerData == null)
            {
                _playerData = playerData;
                NewSkillPlayer(playerData.SkillData);
            }
            else
            {
                RemovedSkillPlayer(playerData);
                NewSkillPlayer(playerData.SkillData);
            }
        }

        public void AddSkillToPanel(SkillStaticData data)
        {
            _skillViewPanel.AddSkills(data);
        }

        public void BuySkill(SkillStaticData data) => 
            _staticDatas.Add(data);

        private void RemovedSkillPlayer(PlayerStaticData data)
        {
            _staticDatas.Clear();
            _playerData = data;
        }

        private void NewSkillPlayer(SkillStaticData data)
        {
            _staticDatas.Add(data);
            _skillViewPanel.Construct(data);
        }
    }
}