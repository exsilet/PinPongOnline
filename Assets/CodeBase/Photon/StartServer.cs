using System;
using System.Collections;
using CodeBase.Infrastructure.LevelLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using Mirror;
using TMPro;
using UnityEngine;

namespace CodeBase.Photon
{
    public class StartServer : MonoBehaviour
    {
        [SerializeField] private byte _maxPlayer;
        [SerializeField] private float _timerStart;
        [SerializeField] private TMP_Text _textTimer;
        [SerializeField] private GameObject _panel;
        [SerializeField] private PlayerSkin _playerSkin;

        private string _playerName;
        private const string GameScene = "GameScene";
        private IGameStateMachine _stateMachine;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillData;

        private void Awake()
        {
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

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
            //PhotonNetwork.JoinRandomRoom();
            EnterTowPlayers();
        }

        private void CreateRoom()
        {
            // if (PhotonNetwork.IsConnected)
            // {
            //     PhotonNetwork.LocalPlayer.NickName = _playerName;
            //     RoomOptions roomOptions = new RoomOptions()
            //     {
            //         CleanupCacheOnLeave = false,
            //         MaxPlayers = _maxPlayer
            //     };
            //
            //     if (roomOptions.MaxPlayers >= _maxPlayer)
            //     {
            //         PhotonNetwork.CreateRoom(null, roomOptions);
            //     }
            // }
        }

        public void OnJoinedRoom()
        {
            Debug.Log("Connected to room");
            //StartCoroutine(ActivePlayer());
        }
        
        private IEnumerator ActivePlayer()
        {
            while (NetworkManager.singleton.maxConnections != _maxPlayer)
            {
                SearchTime();
                yield return null;
            }
            
            EnterTowPlayers();
        }

        private void EnterTowPlayers()
        {
            if (NetworkServer.active && NetworkClient.isConnected)
            {
                NetworkManager.singleton.StartHost();
            }
            else if (NetworkClient.isConnected)
            {
                NetworkManager.singleton.StartServer();
            }
            else if (NetworkServer.active)
            {
                NetworkManager.singleton.StartClient();
            }
            //_stateMachine.Enter<LoadLevelState, string>(GameScene, _playerData, _skillData);
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