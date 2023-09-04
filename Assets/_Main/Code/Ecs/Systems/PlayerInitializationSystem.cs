using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerInitializationSystem"/> class.
    /// This system initializes player data after the player object being spawned.
    /// </summary>
    internal sealed class PlayerInitializationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HasPlayerView>, Exc<Initialized>> _players;

        private EcsPoolInject<Player> _playerPool;
        private EcsPoolInject<Initialized> _initializedPool;
        private EcsPoolInject<OnUpdateScoreEvent> _updateScorePool = GlobalIdents.Worlds.EventWorld;

        public void Run (IEcsSystems systems)
        {
            foreach (var playerEntity in _players.Value)
            {
                ref HasPlayerView playerView = ref _players.Pools.Inc1.Get(playerEntity);
                ref Player player = ref _playerPool.Value.Add(playerEntity);

                player.transform = playerView.view.transform;
                player.jumpForce = playerView.view.JumpForce;
                player.width = playerView.view.Width;
                player.audioSource = playerView.view.AudioSource;

                _initializedPool.Value.Add(playerEntity);
                _updateScorePool.NewEntityImplicit().score = 0;
            }
        }
    }
}