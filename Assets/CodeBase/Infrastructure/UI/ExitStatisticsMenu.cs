using System;
using CodeBase.Infrastructure.Player;
using CodeBase.UIExtensions;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Infrastructure.UI
{
    public class ExitStatisticsMenu : MonoBehaviour
    {
        [SerializeField] private Button _exitMenu;
        [SerializeField] private PlayerMoney _panelMenu;
        
        private void OnEnable()
        {
            _exitMenu.Add(OnClick);
        }

        private void OnDisable()
        {
            _exitMenu.Remove(OnClick);
        }
        
        private void OnClick()
        {
            _panelMenu.gameObject.SetActive(true);
        }
    }
}
