using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="ObstacleSpawnSystem"/> class.
    /// This system spawns the obstacles.
    /// </summary>
    internal sealed class ObstacleSpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnObstacleSpawnDelayChangedEvent>> _delays = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<SpawnPrefab> _spawnPrefabPool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<StaticData> _staticData;
        private EcsCustomInject<SceneData> _sceneData;
        
        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<PauseService> _pauseService;

        private float _elapsedTime = 0f;
        private float _spawnDelay = 0f;

        private StaticData StaticData => _staticData.Value;
        private SceneData SceneData => _sceneData.Value;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            // check for the spawn delay update
            foreach (var delayEntity in _delays.Value)
            {
                _spawnDelay = _delays.Pools.Inc1.Get(delayEntity).delay;
                _delays.Pools.Inc1.Del(delayEntity);
            }

            if (_spawnDelay <= 0.0001f)
                _spawnDelay = 0.0001f;

            if (_elapsedTime >= _spawnDelay)
                _elapsedTime = 0;

            // check if we are ready to spawn a new obstacle
            if (_elapsedTime == 0)
            {
                ref SpawnPrefab spawnPrefab = ref _spawnPrefabPool.NewEntityImplicit();
                spawnPrefab.prefab = StaticData.ObstaclePrefab;
                spawnPrefab.position = SceneData.ObstacleSpawnPoint.position;
                spawnPrefab.rotation = SceneData.ObstacleSpawnPoint.rotation;
                spawnPrefab.parent = SceneData.ObstacleSpawnPoint;
            }

            if (_timeService.Value.deltaTime == 0)
                _elapsedTime += 0.01f;
            else
                _elapsedTime += _timeService.Value.deltaTime;
        }
    }
}