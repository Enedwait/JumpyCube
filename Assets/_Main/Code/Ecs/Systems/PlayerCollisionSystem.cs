using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;
using OKRT.JumpyCube.Main.Code.Helpers;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerCollisionSystem"/> class.
    /// This system processes player collisions.
    /// </summary>
    internal sealed class PlayerCollisionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Player>> _players;
        private EcsFilterInject<Inc<OnCollisionEnterEvent>> _collisions = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<OnPlayerDiedEvent> _deathPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnOneShotSoundEvent> _soundsPool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<PauseService> _pauseService;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            foreach (var collisionEntity in _collisions.Value)
            {
                if (!_collisions.Pools.Inc1.Has(collisionEntity))
                    continue;

                ref OnCollisionEnterEvent collision = ref _collisions.Pools.Inc1.Get(collisionEntity);

                foreach (var playerEntity in _players.Value)
                {
                    ref Player player = ref _players.Pools.Inc1.Get(playerEntity);
                    if (player.transform == collision.senderGameObject.transform)
                        ProcessPlayerCollision(playerEntity, ref player, ref collision);
                }
                
                _collisions.DelEntity(collisionEntity);
            }
        }

        /// <summary>
        /// Processes the player collision.
        /// If player hit obstacle -> then he should die.
        /// If player hit ceiling or ground -> then there should occur just the proper sound.
        /// </summary>
        /// <param name="playerEntity">player entity.</param>
        /// <param name="player">player component.</param>
        /// <param name="collision">collision event.</param>
        private void ProcessPlayerCollision(int playerEntity, ref Player player, ref OnCollisionEnterEvent collision)
        {
            EcsPackedEntityWithWorld playerPacked = _players.Value.GetWorld().PackEntityWithWorld(playerEntity);

            ref OnOneShotSoundEvent sound = ref _soundsPool.NewEntity(out int soundEntity);
            sound.entity = playerPacked;

            if (collision.collider.gameObject.layer == LayerHelper.Obstacle)
            {
                _deathPool.NewEntityImplicit().playerEntity = playerPacked;
                sound.kind = OneShotSoundKind.ObstacleHit;
            }
            else
            {
                if (collision.collider.gameObject.CompareTag(GlobalIdents.Tags.Ceiling))
                    sound.kind = OneShotSoundKind.CeilingHit;
                else if (collision.collider.gameObject.CompareTag(GlobalIdents.Tags.Ground))
                    sound.kind = OneShotSoundKind.GroundHit;
            }

            if (sound.kind == OneShotSoundKind.None)
                _soundsPool.DelEntity(soundEntity);
        }
    }
}