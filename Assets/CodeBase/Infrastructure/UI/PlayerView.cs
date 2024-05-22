using CodeBase.Infrastructure.StaticData;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Button _choseSkin;

        private PlayerStaticData _playerStaticData;
        private bool _isInitialized;
        private PlayerPanelSkin _playerPanelSkin;

        public void InitializeView(PlayerStaticData playerStaticData, PlayerPanelSkin playerPanelSkin)
        {
            _playerStaticData = playerStaticData;
            _playerPanelSkin = playerPanelSkin;
            _icon.sprite = playerStaticData.Icon;
            _isInitialized = true;
            OnEnable();
        }

        private void OnEnable()
        {
            if (_isInitialized == false)
                return;
            
            _choseSkin.Add(OnClick);
        }

        private void OnDisable()
        {
            _choseSkin.Remove(OnClick);
        }

        private void OnClick()
        {
            _playerPanelSkin.ChooseSkin(_playerStaticData);
        }
    }
}