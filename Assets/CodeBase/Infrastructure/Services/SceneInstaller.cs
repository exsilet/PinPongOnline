using CodeBase.Infrastructure.StaticData;
using CodeBase.Infrastructure.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private PlayerStaticData _defaultPlayerData;

        public override void InstallBindings()
        {
            Container.Bind<PlayerStaticData>().FromInstance(_defaultPlayerData);
            Container.Bind<PlayerSkin>().AsSingle();
        }
    }
}