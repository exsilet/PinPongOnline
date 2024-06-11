using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Photon;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.Infrastructure.UI
{
    public class PlayerSkin : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _icon2;
        [SerializeField] private PlayerStaticData _defaultPlayerData;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Button _openPanelStatic;
        [SerializeField] private Button _openPanelStatic2;
        [SerializeField] private StartServer _startServer;
        
        private PlayerStaticData _currentPlayerData;
        private PlayerPanelSkin _panelSkin;

        public PlayerStaticData PlayerStaticData => _currentPlayerData;

        [Inject]
        private void ChoseSkins(PlayerStaticData playerData) => 
            _currentPlayerData = playerData;
        
        public void Construct(PlayerPanelSkin panelSkin) => 
            _panelSkin = panelSkin;

        private void Start()
        {
            if (_icon.sprite == null)
            {
                _icon.sprite = _defaultPlayerData.Icon;
                _icon2.sprite = _defaultPlayerData.Icon;
                _currentPlayerData = _defaultPlayerData;
                _inventory.Construct(_currentPlayerData);
            }
            else
            {
                _icon.sprite = _currentPlayerData.Icon;
            }
        }

        private void OnEnable()
        {
            _openPanelStatic.Add(OnClick);
            _openPanelStatic2.Add(OnClick);
        }

        private void OnDisable()
        {
            _openPanelStatic.Remove(OnClick);
            _openPanelStatic2.Remove(OnClick);
        }

        public void CurrentPlayerSkin(PlayerStaticData data)
        {
            _currentPlayerData = data;
            _icon.sprite = data.Icon;
            _icon2.sprite = data.Icon;
            _inventory.Construct(data);
            _startServer.SetPlayerData(data);
        }

        private void OnClick()
        {
            _panelSkin.gameObject.SetActive(true);
            _panelSkin.Construct(this, _currentPlayerData);
        }
    }
}