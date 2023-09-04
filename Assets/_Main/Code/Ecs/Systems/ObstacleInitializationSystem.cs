using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.UI;
using OKRT.JumpyCube.Main.Code.Views;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="ObstacleInitializationSystem"/> class.
    /// This system initializes the obstacles with the size, speed and score.
    /// </summary>
    internal sealed class ObstacleInitializationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HasObstacleView>, Exc<Initialized>> _obstacles;
        private EcsFilterInject<Inc<OnObstacleSpeedChangedEvent>> _speeds = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnScoreFactorChangedEvent>> _scoreFactors = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<HasRigidbody> _rididbodyPool;
        private EcsPoolInject<HasSpeed> _speedPool;
        private EcsPoolInject<Initialized> _initializedPool;
        private EcsPoolInject<ChangeScoreIfPassed> _scorePool;
        
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<ScreenSizeService> _screenSizeService;

        private float _obstacleSpeed = 0f;
        private float _scoreFactor = 1;

        private SceneData SceneData => _sceneData.Value;

        public void Run(IEcsSystems systems)
        {
            foreach (var obstacleEntity in _obstacles.Value)
            {
                ref HasObstacleView hasObstacleView = ref _obstacles.Pools.Inc1.Get(obstacleEntity);

                ObstacleView obstacleView = hasObstacleView.view;

                foreach (var speedEntity in _speeds.Value)
                {
                    _obstacleSpeed = _speeds.Pools.Inc1.Get(speedEntity).speed;
                    _speeds.Pools.Inc1.Del(speedEntity);
                }

                foreach (var scoreLevelEntity in _scoreFactors.Value)
                {
                    _scoreFactor = _scoreFactors.Pools.Inc1.Get(scoreLevelEntity).factor;
                    _scoreFactors.Pools.Inc1.Del(scoreLevelEntity);
                }

                // determine random Y offset for obstacle
                float y = Random.Range(_screenSizeService.Value.MinY + obstacleView.GapLength / 2f, _screenSizeService.Value.MaxY - obstacleView.GapLength / 2f);
                Vector3 position = SceneData.ObstacleSpawnPoint.position;
                position = new Vector3(position.x, y, position.z);

                if (_rididbodyPool.Value.Has(obstacleEntity))
                {
                    ref HasRigidbody hasRigidbody = ref _rididbodyPool.Value.Get(obstacleEntity);
                    hasRigidbody.rigidbody.position = position;
                }
                else
                {
                    obstacleView.transform.position = position;
                }

                if (_speedPool.Value.Has(obstacleEntity))
                {
                    ref HasSpeed hasSpeed = ref _speedPool.Value.Get(obstacleEntity);
                    hasSpeed.speed = _obstacleSpeed;
                }

                if (_scorePool.Value.Has(obstacleEntity))
                {
                    ref ChangeScoreIfPassed score = ref _scorePool.Value.Get(obstacleEntity);
                    score.change = (long)(score.change * _scoreFactor);
                }

                _initializedPool.Value.Add(obstacleEntity);
            }
        }
    }
}