using CodeBase.Infrastructure.Logic;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services
{
    public class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SceneLoader>().AsSingle();
        }

        public override void Start()
        {
            Container.Resolve<SceneLoader>().Load(SceneID.MainMenu);
        }
    }
}