using System.Collections.Generic;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Infrastructure.UI
{
    public class PlayerPanelSkin : MonoBehaviour
    {
        [SerializeField] private List<PlayerStaticData> _playerDatas;
        [SerializeField] private PlayerView _playerViewPrefab;
        [SerializeField] private Transform _container;
        [SerializeField] private Image _playerIcon;
        [SerializeField] private Image _playerIcon2;
        
        private PlayerSkin _playerIcon3;
        private List<PlayerView> _playerViews = new();
        
        [Inject] private PlayerStaticData _currentSkin;
        
        private void Start()
        {
            foreach (var playerData in _playerDatas)
            {
                PlayerView newItem = Instantiate(_playerViewPrefab, _container);
                newItem.InitializeView(playerData, this);
                _playerViews.Add(newItem);
            }
            
            if (_playerIcon.sprite == null)
            {
                _playerIcon.sprite = _currentSkin.Icon;
                _playerIcon2.sprite = _currentSkin.Icon;
            }
        }


        public void Construct(PlayerSkin playerSkin, PlayerStaticData currentPlayerData)
        {
            _playerIcon3 = playerSkin;
            _currentSkin = currentPlayerData;
        }

        public void ChooseSkin(PlayerStaticData data)
        {
            _currentSkin = data;
            _playerIcon3.CurrentPlayerSkin(data);
            _playerIcon.sprite = _currentSkin.Icon;
            _playerIcon2.sprite = _currentSkin.Icon;
        }
    }
}