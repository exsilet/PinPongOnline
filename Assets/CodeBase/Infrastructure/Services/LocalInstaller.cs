using CodeBase.Infrastructure.StaticData;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
    public class LocalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BuildingPlayer();
        }

        private void BuildingPlayer()
        {
            
        }
    }
}