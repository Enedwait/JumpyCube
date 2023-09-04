using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerActionSystem"/> class.
    /// This system actually performs player action based on processing of "jump" or "back" buttons clicks.
    /// </summary>
    internal sealed class PlayerActionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Player>> _players;
        private EcsFilterInject<Inc<OnPlayerPressedJumpEvent>> _jumps = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnPlayerPressedBackEvent>> _backs = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<JumpCommand> _jumpCommandPool;
        private EcsPoolInject<OnGameStateChangeEvent> _stateChangePool = GlobalIdents.Worlds.EventWorld;

        public void Run(IEcsSystems systems)
        {
            foreach (var jumpEventEntity in _jumps.Value)
            {
                foreach (var playerEntity in _players.Value)
                {
                    ref var jumpCommand = ref _jumpCommandPool.Value.GetOrAdd(playerEntity);
                    jumpCommand.jump = true;
                }

                _jumps.DelEntity(jumpEventEntity);
            }

            foreach (var backEventEntity in _backs.Value)
            {
                _stateChangePool.NewEntityImplicit().state = GameState.GoBack;
                _backs.DelEntity(backEventEntity);
            }
        }
    }
}