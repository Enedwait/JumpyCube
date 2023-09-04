using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="VersionInitSystem"/> class.
    /// This system initializes all the texts where the version info should be written.
    /// </summary>
    internal sealed class VersionInitSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<VersionText>> _versions = GlobalIdents.Worlds.StaticWorld;

        public void Run(IEcsSystems systems)
        {
            foreach (var versionEntity in _versions.Value)
            {
                if (!_versions.Pools.Inc1.Has(versionEntity))
                    continue;

                ref VersionText version = ref _versions.Pools.Inc1.Get(versionEntity);
                if (version.kind == VersionKind.Game) 
                    version.text.text = string.Format(version.format, Application.version);
                else 
                    version.text.text = string.Format(version.format, "2022");

                _versions.Pools.Inc1.Del(versionEntity);
            }
        }
    }
}