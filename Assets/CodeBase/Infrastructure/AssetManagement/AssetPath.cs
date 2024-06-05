using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace CodeBase.Infrastructure.AssetManagement
{
    public static class AssetPath
    {
        public const string PlayingFieldPath = "PlayingField/Borders";
        public const string BallPath = "Ball";
        public const string HudPath = "Hud/HudMenu";
        public const string HudMenuPlayerPath = "Hud/HudMenuPlayer";
        public const string HudMenuGamePath = "Hud/HudMenuGame";
        public const string HudBattlePlayer1Path = "Hud/PlayerUISkill";
        public const string HudBattlePlayer2Path = "Hud/TwoPlayerUISkill";
        public const string Spawner = "SpawnPoints/SpawnPoint1";
        public const string Spawner1 = "SpawnPoints/SpawnPoint2";

        public static Vector3 GetSpawner()
        {
            var prefab = Resources.Load<GameObject>(Spawner);
            Vector3 vector = prefab.transform.position;
            return vector;
        }
        
        public static Vector3 GetSpawner1()
        {
            var prefab = Resources.Load<GameObject>(Spawner1);
            Vector3 vector = prefab.transform.position;
            return vector;
        }
    }
}