using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Ball;
using CodeBase.Infrastructure.Player;
using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assets;

        public GameObject Hero1 { get; private set; }
        public GameObject Hero2 { get; private set; }
        public GameObject HudMenu { get; private set; }
        public GameObject GamePlayingField { get; private set; }
        public GameObject Ball { get; private set; }
        
        public GameFactory(IAssetProvider assets)
        {
            _assets = assets;
        }  
        
        public GameObject CreateHubMenu()
        {
            return _assets.Instantiate(AssetPath.HudPath);
        }

        public GameObject CreateHudMenuPlayer()
        {
            return _assets.Instantiate(AssetPath.HudMenuPlayerPath);
        }

        public GameObject CreatePlayingField()
        {
            GamePlayingField = _assets.Instantiate(AssetPath.PlayingFieldPath);
            HudMenu.GetComponent<Score>().Construct(GamePlayingField.GetComponentInChildren<ScorePlayer>(), 
                GamePlayingField.GetComponentInChildren<ScoreEnemy>());

            return GamePlayingField;
        }

        public GameObject CreateBall()
        {
            Ball = CreatePhoton(AssetPath.BallPath);
            
            GamePlayingField.GetComponentInChildren<ScorePlayer>().Construct(Ball.GetComponent<BallMovet>());
            GamePlayingField.GetComponentInChildren<ScoreEnemy>().Construct(Ball.GetComponent<BallMovet>());
            
            return Ball;
        }

        public GameObject CreateHudGame()
        {
            HudMenu = _assets.Instantiate(AssetPath.HudMenuGamePath);
            
            return HudMenu;
        }

        public GameObject CreateHero(PlayerStaticData staticData, SkillStaticData skillData)
        {
            CreateBall();
            
            return PlayerCreation(staticData, skillData, AssetPath.Spawner, AssetPath.HudBattlePlayer1Path);
            
            // if (PhotonNetwork.IsMasterClient)
            // {
            //     CreateBall();
            //     
            //     return PlayerCreation(staticData, skillData, AssetPath.Spawner, AssetPath.HudBattlePlayer1Path);
            // }
            // else
            // { 
            //    return PlayerCreation(staticData, skillData, AssetPath.Spawner1, AssetPath.HudBattlePlayer2Path);
            // }
        }

        public GameObject CreateHeroOffline(PlayerStaticData staticData, SkillStaticData skillData)
        {
           return PlayerCreation(staticData, skillData, AssetPath.Spawner, AssetPath.HudBattlePlayer1Path);
        }

        public GameObject CreateBot(PlayerStaticData staticData, SkillStaticData skillData)
        {
            Hero2 = CreatePhotonHero(staticData.Prefab.name, AssetPath.Spawner1);
            var hud = CreateHudBattle(AssetPath.HudBattlePlayer2Path, staticData, skillData, Ball, Hero2);
            ConstructEnemy(Hero2, staticData, hud);
            hud.GetComponent<Canvas>().enabled = false;

            return Hero2;
        }

        private GameObject PlayerCreation(PlayerStaticData staticData, SkillStaticData skillData, string spawnerPlayer, string path)
        {
            Hero1 = CreatePhotonHero(staticData.Prefab.name, spawnerPlayer);

            var hud = CreateHudBattle(path, staticData, skillData, Ball, Hero1);

            Construct(Hero1, staticData, Ball);

            return Hero1;
        }

        private GameObject CreatePhotonHero(string namePlayer, string spawnerPlayer)
        {
            GameObject photonHero = _assets.InstantiatePhoton(namePlayer, spawnerPlayer);

            return photonHero;
        }

        private GameObject CreatePhoton(string namePlayer)
        {
            GameObject photonObject = _assets.InstantiatePhoton(namePlayer);
            return photonObject;
        }

        private GameObject CreateHudBattle(string path, PlayerStaticData staticData, SkillStaticData skillData, GameObject ball, GameObject hero)
        {
            GameObject hud = _assets.Instantiate(path);
            
            hud.GetComponent<ActiveSkillPanel>().Construct(staticData, skillData, ball.GetComponent<BallMovet>(), hero.GetComponent<Fighter>());

            return hud;
        }

        private void Construct(GameObject hero, PlayerStaticData staticData, GameObject ball)
        {
            hero.GetComponent<Fighter>().Construct(staticData, ball.GetComponent<BallMovet>());
        }
        
        private void ConstructEnemy(GameObject hero, PlayerStaticData staticData, GameObject hud)
        {
            
        }
    }
}