using OKRT.JumpyCube.Main.Code.Ecs.Services;
using UnityEngine;
using Zenject;

namespace OKRT.JumpyCube.Main.Code.Installers
{
    /// <summary>
    /// The <see cref="SceneInstaller"/> class.
    /// This class installs some Zenject scene dependencies.
    /// </summary>
    internal sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private EcsStartup _ecsStartup;

        public override void InstallBindings()
        {
            Container.Bind<WorldService>().AsSingle();
            Container.Bind<SpawnService>().AsSingle();
            Container.BindInstance(_ecsStartup);
        }
    }
}
