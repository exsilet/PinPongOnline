using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Infrastructure.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataHeroPath = "StaticData/Player";

        private Dictionary<PlayerTypeId, PlayerStaticData> _playerStatic;

        public void Load()
        {
            _playerStatic = Resources
                .LoadAll<PlayerStaticData>(StaticDataHeroPath)
                .ToDictionary(x => x.PlayerTypeId, x => x);
        }
        
        public PlayerStaticData ForPlayer(PlayerTypeId typeID) =>
            _playerStatic.TryGetValue(typeID, out PlayerStaticData staticData) 
                ? staticData 
                : null;
    }
}