using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using CodeBase.Photon;
using UnityEngine;
using UnityEngine.Events;

namespace CodeBase.Infrastructure.Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private StartServer _startServer;
        [SerializeField] private SkillViewPanel _skillViewPanel;

        private List<SkillStaticData> _staticDatas = new List<SkillStaticData>();

        private PlayerStaticData _playerData;
        public PlayerStaticData PlayerData => _playerData;

        public event UnityAction<PlayerStaticData> ChangeSkinPlayer; 
        public event UnityAction<PlayerStaticData, SkillStaticData> ChangePlayer; 

        public void Construct(PlayerStaticData playerData)
        {
            if (_playerData == null)
            {
                _playerData = playerData;
                ChangeSkinPlayer?.Invoke(_playerData);
                NewSkillPlayer(playerData.SkillData);
            }
            else
            {
                RemovedSkillPlayer(playerData);
                NewSkillPlayer(playerData.SkillData);
            }
        }

        private void OnEnable()
        {
            ChangeSkinPlayer += _startServer.SetPlayerData;
        }

        private void OnDisable()
        {
            ChangeSkinPlayer -= _startServer.SetPlayerData;
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
            ChangeSkinPlayer?.Invoke(data);
        }

        private void NewSkillPlayer(SkillStaticData data)
        {
            _staticDatas.Add(data);
            _skillViewPanel.Construct(data);
        }
    }
}