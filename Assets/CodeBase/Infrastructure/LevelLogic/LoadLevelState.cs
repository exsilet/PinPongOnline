using CodeBase.Infrastructure.Logic;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.LevelLogic
{
    public class LoadLevelState : IPayloadedState1<string, PlayerStaticData, SkillStaticData>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;
        private PlayerStaticData _playerData;
        private SkillStaticData _skillData;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingCurtain loadingCurtain,
            IGameFactory gameFactory, IStaticDataService staticData)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticData = staticData;
        }

        public void EnterTwoParameters(string sceneName, PlayerStaticData playerData, SkillStaticData skillData)
        {
            _loadingCurtain.Show();
            _playerData = playerData;
            _skillData = skillData;
            _sceneLoader.Load(sceneName, OnLoaded);
        }

        private void OnLoaded()
        {
            InitGameWorld();
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit() =>
            _loadingCurtain.Hide();

        private void InitGameWorld()
        {
            _gameFactory.CreateHudGame();
            _gameFactory.CreatePlayingField();
            //CreateHeroWorld(_playerData, _skillData);
        }

        private void CreateHeroWorld(PlayerStaticData playerData, SkillStaticData skillData)
        {
            GameObject hero = _gameFactory.CreateHero(playerData, skillData);
        }
        
        private void CreateOffline(PlayerStaticData playerData, SkillStaticData skillData, PlayerStaticData botData)
        {
            GameObject hero2 = _gameFactory.CreateBot(botData, botData.SkillData);
            GameObject hero1 = _gameFactory.CreateHeroOffline(playerData, skillData);
        } 
    }
}