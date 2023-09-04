using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerJumpSystem"/> class.
    /// This system processes the player jumps.
    /// </summary>
    internal sealed class PlayerJumpSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Player, JumpCommand>> _players;

        private EcsPoolInject<HasRigidbody> _rigidbodyPool;
        private EcsPoolInject<HasParticleSystem> _particleSystemPool;
        private EcsPoolInject<OnOneShotSoundEvent> _soundsPool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<PauseService> _pauseService;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            foreach (var playerEntity in _players.Value)
            {
                ref Player player = ref _players.Pools.Inc1.Get(playerEntity);
                ref JumpCommand jumpCommand = ref _players.Pools.Inc2.Get(playerEntity);

                if (jumpCommand.jump && !jumpCommand.previousJump)
                {
                    if (_rigidbodyPool.Value.Has(playerEntity))
                    {
                        ref HasRigidbody hasRigidbody = ref _rigidbodyPool.Value.Get(playerEntity);
                        hasRigidbody.rigidbody.AddForce(new Vector3(0, player.jumpForce, 0), ForceMode.Impulse);

                        ref OnOneShotSoundEvent sound = ref _soundsPool.NewEntityImplicit();
                        sound.entity = _players.Value.GetWorld().PackEntityWithWorld(playerEntity);
                        sound.kind = OneShotSoundKind.Jump;
                    }

                    if (_particleSystemPool.Value.Has(playerEntity))
                    {
                        ref HasParticleSystem hasParticleSystem = ref _particleSystemPool.Value.Get(playerEntity);
                        hasParticleSystem.system.time = 0;
                        hasParticleSystem.system.Play();
                    }
                }

                jumpCommand.previousJump = jumpCommand.jump;
                jumpCommand.jump = false;
            }
        }
    }
}