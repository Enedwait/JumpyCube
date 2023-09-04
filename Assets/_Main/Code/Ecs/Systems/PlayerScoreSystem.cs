using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerScoreSystem"/> class.
    /// This system processes the player score changes.
    /// </summary>
    internal sealed class PlayerScoreSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Player>> _players;
        private EcsFilterInject<Inc<ChangeScoreIfPassed, Initialized>, Exc<PassedObstacle>> _obstaclesInFront;

        private EcsPoolInject<PlayerScore> _playerScorePool;
        private EcsPoolInject<PassedObstacle> _passedObstaclesPool;
        private EcsPoolInject<OnUpdateScoreEvent> _updateScorePool = GlobalIdents.Worlds.EventWorld;

        public void Run(IEcsSystems systems)
        {
            foreach (var playerEntity in _players.Value)
            {
                ref Player player = ref _players.Pools.Inc1.Get(playerEntity);
                ref PlayerScore score = ref _playerScorePool.Value.GetOrAdd(playerEntity);

                foreach (var obstacleEntity in _obstaclesInFront.Value)
                {
                    ref ChangeScoreIfPassed obstacle = ref _obstaclesInFront.Pools.Inc1.Get(obstacleEntity);
                    if (obstacle.transform.position.x + obstacle.distance < player.transform.position.x - player.width / 2f)
                    {
                        score.score += obstacle.change;

                        _updateScorePool.NewEntityImplicit().score = score.score;
                        _passedObstaclesPool.Value.Add(obstacleEntity);
                    }
                }
            }
        }
    }
}