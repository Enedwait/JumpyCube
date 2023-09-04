using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="SpawnSystem"/> class.
    /// This systems instantiates prefabs when requested.
    /// </summary>

    internal sealed class SpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SpawnPrefab>> _spawnPrefabs = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<SpawnService> _spawnService;

        public void Run(IEcsSystems systems)
        {
            foreach (var spawnEntity in _spawnPrefabs.Value)
            {
                ref SpawnPrefab spawnPrefab = ref _spawnPrefabs.Pools.Inc1.Get(spawnEntity);
                _spawnService.Value.Spawn(spawnPrefab);
                _spawnPrefabs.DelEntity(spawnEntity);
            }
        }
    }
}