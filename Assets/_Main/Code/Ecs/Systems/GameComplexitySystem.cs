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
    /// The <see cref="GameComplexitySystem"/> class.
    /// This system modifies the game complexity.
    /// </summary>
    internal sealed class GameComplexitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<GameComplexity>> _gameComplexity;

        private EcsPoolInject<OnObstacleSpawnDelayChangedEvent> _delayChangedPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnObstacleSpeedChangedEvent> _speedChangedPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnScoreFactorChangedEvent> _scoreFactorChangedPool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<StaticData> _staticData;

        private int? _gameComplexityEntity;
        
        internal StaticData StaticData => _staticData.Value;

        public void Run(IEcsSystems systems)
        {
            CheckEntity();

            if (!_gameComplexityEntity.HasValue)
            {
                _gameComplexityEntity = systems.GetWorld().NewEntity();
                ref GameComplexity def = ref _gameComplexity.Pools.Inc1.Add(_gameComplexityEntity.Value);

                def.levelOfSpeed = 1;

                // set initial values
                _scoreFactorChangedPool.NewEntityImplicit().factor = 1;
                _delayChangedPool.NewEntityImplicit().delay = StaticData.ObstacleSpawnDelay;
                _speedChangedPool.NewEntityImplicit().speed = StaticData.ObstacleSpeed;
            }

            ref GameComplexity gameComplexity = ref _gameComplexity.Pools.Inc1.Get(_gameComplexityEntity.Value);

            // check if we should change the speed level based on the time played
            if (_timeService.Value.elapsedGameplayTime > StaticData.ChangeSpeedDelay * gameComplexity.levelOfSpeed)
            {
                gameComplexity.levelOfSpeed++;
                _scoreFactorChangedPool.NewEntityImplicit().factor = Mathf.Pow(StaticData.ChangeScoreFactor, gameComplexity.levelOfSpeed - 1);
                _delayChangedPool.NewEntityImplicit().delay = StaticData.ObstacleSpawnDelay / Mathf.Pow(StaticData.ChangeObstacleDelayFactor, gameComplexity.levelOfSpeed - 1);
                _speedChangedPool.NewEntityImplicit().speed = StaticData.ObstacleSpeed * Mathf.Pow(StaticData.ChangeSpeedFactor, gameComplexity.levelOfSpeed - 1);
            }
        }

        /// <summary>
        /// Checks if the game complexity entity exists or not. If not then new game complexity entity should be created.
        /// </summary>
        private void CheckEntity()
        {
            _gameComplexityEntity = null;
            foreach (var gameComplexityEntity in _gameComplexity.Value)
            {
                _gameComplexityEntity = gameComplexityEntity;
                break;
            }
        }
    }
}