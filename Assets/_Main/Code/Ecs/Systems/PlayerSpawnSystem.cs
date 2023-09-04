using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerSpawnSystem"/> class.
    /// This system spawns a single player (if no player spawned yet).
    /// </summary>
    internal sealed class PlayerSpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnPlayerSpawnedEvent>> _spawnPlayer = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<SpawnPrefab> _spawnPrefabPool = GlobalIdents.Worlds.EventWorld;
        
        private EcsCustomInject<PauseService> _pauseService;
        private EcsCustomInject<StaticData> _staticData;
        private EcsCustomInject<SceneData> _sceneData;
        
        private StaticData StaticData => _staticData.Value;
        private SceneData SceneData => _sceneData.Value;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            // check if the player has been spawned already
            if (_spawnPlayer.Value.GetEntitiesCount() > 0)
                return;

            // mark that we've spawned a new player
            _spawnPlayer.Pools.Inc1.NewEntityImplicit();

            ref SpawnPrefab spawnPrefab = ref _spawnPrefabPool.NewEntityImplicit();
            spawnPrefab.prefab = StaticData.PlayerPrefab;
            spawnPrefab.position = SceneData.PlayerSpawnPoint.position;
            spawnPrefab.rotation = SceneData.PlayerSpawnPoint.rotation;
            spawnPrefab.parent = SceneData.PlayerSpawnPoint;
        }
    }
}