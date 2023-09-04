using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="ObstacleMoveSystem"/> class.
    /// This system moves the obstacles.
    /// </summary>
    internal sealed class ObstacleMoveSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HasRigidbody, HasSpeed>> _rigidbodiesWithSpeed;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _rigidbodiesWithSpeed.Value)
            {
                if (!_rigidbodiesWithSpeed.Pools.Inc1.Has(entity))
                    continue;

                ref HasRigidbody hasRigidbody = ref _rigidbodiesWithSpeed.Pools.Inc1.Get(entity);
                ref HasSpeed hasSpeed = ref _rigidbodiesWithSpeed.Pools.Inc2.Get(entity);

                hasRigidbody.rigidbody.velocity = new Vector3(-hasSpeed.speed, 0, 0);
            }
        }
    }
}