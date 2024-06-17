using System;
using System.Collections;
using System.Linq;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Photon
{
    public class StartServer : MonoBehaviour
    {
        [SerializeField] private byte _maxPlayer;
        [SerializeField] private float _timerStart;
        [SerializeField] private TMP_Text _textTimer;
        [SerializeField] private GameObject _panel;
        [SerializeField] private PlayerSkin _playerSkin;
        [SerializeField] private string _gameScenePath;
        [SerializeField] private NetworkRunner _networkRunnerPrefab;

        private string _playerName;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillData;
        private NetworkRunner _runnerInstance;

        private void Start()
        {
            _playerData = _playerSkin.PlayerStaticData;
        }
        
        public void SetPlayerData(PlayerStaticData staticData)
        {
            _playerData = staticData;
            Debug.Log(" change player ");
        }
        
        public void QuickMatch()
        {
            StartGame(GameMode.Shared);
            OnJoinedRoom();
        }

        private void OnJoinedRoom()
        {
            Debug.Log("Connected to room");
            StartCoroutine(ActivePlayer());
        }
        
        private async void StartGame(GameMode mode)
        {
            if (_runnerInstance == null)
            {
                _runnerInstance = Instantiate(_networkRunnerPrefab);
            }
            
            _runnerInstance.ProvideInput = true;

            var startGameArgs = new StartGameArgs()
            {
                GameMode = mode,
                //SessionName = roomName,
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(_gameScenePath)),
                //ObjectProvider = _runnerInstance.GetComponent<NetworkObjectPoolDefault>(),
            };
            
            await _runnerInstance.StartGame(startGameArgs);
        }
        
        private IEnumerator ActivePlayer()
        {
            while ( _runnerInstance.ActivePlayers.Count() != _maxPlayer)
            {
                SearchTime();
                yield return null;
            }
            
            EnterTowPlayers();
            //yield return null;
        }

        private void EnterTowPlayers()
        {
            StopCoroutine(ActivePlayer());
            
            if (_runnerInstance.IsServer)
            {
                _runnerInstance.LoadScene(_gameScenePath);
            }
        }
        
        private void SearchTime()
        {
            _panel.SetActive(true);
            _timerStart += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(_timerStart);
            _textTimer.text = time.Minutes.ToString("00") + ":" + time.Seconds.ToString("00");            
        }
    }
}