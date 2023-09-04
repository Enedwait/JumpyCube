using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerDiedSystem"/> class.
    /// This system processes the player death.
    /// </summary>
    internal sealed class PlayerDiedSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnPlayerDiedEvent>> _deadPlayers = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<OnGameStateChangeEvent> _stateChangePool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<PauseService> _pauseService;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            foreach (var deadPlayerEntity in _deadPlayers.Value)
            {
                _stateChangePool.NewEntityImplicit().state = GameState.GameOver;
            }
        }
    }
}