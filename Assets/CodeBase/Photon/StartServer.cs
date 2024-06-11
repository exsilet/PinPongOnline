using System;
using System.Collections;
using CodeBase.Infrastructure.LevelLogic;
using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

namespace CodeBase.Photon
{
    public class StartServer : MonoBehaviourPunCallbacks
    {
        [SerializeField] private byte _maxPlayer;
        [SerializeField] private TMP_Text _connectionStatus;
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
            PhotonNetwork.AutomaticallySyncScene = true;
            _stateMachine = AllServices.Container.Single<IGameStateMachine>();
        }

        private void Start()
        {
            ConnectToPhotonServer();
            _playerData = _playerSkin.PlayerStaticData;
        }

        private void ConnectToPhotonServer()
        {
            _connectionStatus.text = "Connecting...";
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
        
        public void SetPlayerData(PlayerStaticData staticData)
        {
            _playerData = staticData;
            Debug.Log("1 change player ");
        }

        public override void OnConnected()
        {
            base.OnConnected();

            _connectionStatus.text = "Connected to Photon!";
            _connectionStatus.color = Color.green;
        }
        
        public void QuickMatch()
        {
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
        }
        
        private void CreateRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.LocalPlayer.NickName = _playerName;
                RoomOptions roomOptions = new RoomOptions()
                {
                    CleanupCacheOnLeave = false,
                    MaxPlayers = _maxPlayer
                };

                if (roomOptions.MaxPlayers >= _maxPlayer)
                {
                    PhotonNetwork.CreateRoom(null, roomOptions);
                }
            }
        }
        
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            CreateRoom();
        }
        
        public override void OnJoinedRoom()
        {
            PhotonNetwork.SendRate = 60;
            PhotonNetwork.SerializationRate = 60;
            
            Debug.Log("Connected to room");
            StartCoroutine(ActivePlayer());
        }
        
        private IEnumerator ActivePlayer()
        {
            while (PhotonNetwork.CurrentRoom.PlayerCount != _maxPlayer)
            {
                SearchTime();
                yield return null;
            }
            
            EnterTowPlayers();
        }

        private void EnterTowPlayers()
        {
            StopCoroutine(ActivePlayer());
            //SceneManager.LoadScene(_sceneIndex);
            _stateMachine.Enter<LoadLevelState, string>(GameScene, _playerData, _skillData);
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