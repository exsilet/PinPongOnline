﻿using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.LevelLogic
{
    public class LoadMenuState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _curtain;
        private readonly IGameFactory _gameFactory;

        public LoadMenuState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingCurtain curtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _curtain = curtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string sceneName)
        {
            _curtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        public void Exit() =>
            _curtain.Hide();

        private void OnLoaded()
        {
            GameObject hubMenu = _gameFactory.CreateHubMenu();
            
            InitHud(hubMenu);
            
            _stateMachine.Enter<GameLoopState>();
        }

        private void InitHud(GameObject hubMenu)
        {
            GameObject uiRoot = _gameFactory.CreateHudMenuPlayer();
            
            hubMenu.GetComponent<PlayerSkin>().Construct(uiRoot.GetComponent<PlayerPanelSkin>());
            uiRoot.GetComponent<ExitStatisticsMenu>().Construct(hubMenu.GetComponent<PlayerMoney>());
        }
    }
}