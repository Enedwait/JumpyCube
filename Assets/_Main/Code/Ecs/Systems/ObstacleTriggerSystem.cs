using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Extensions;
using OKRT.JumpyCube.Main.Code.Helpers;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="ObstacleTriggerSystem"/> class.
    /// This system processes the obstacle triggers.
    /// </summary>
    internal sealed class ObstacleTriggerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<HasObstacleView, Initialized>> _obstacles;
        private EcsFilterInject<Inc<OnTriggerEnterEvent>> _triggers = GlobalIdents.Worlds.EventWorld;

        public void Run (IEcsSystems systems)
        {
            foreach (var triggerEntity in _triggers.Value)
            {
                ref OnTriggerEnterEvent trigger = ref _triggers.Pools.Inc1.Get(triggerEntity);

                if (trigger.collider.gameObject.layer == LayerHelper.DestroyArea)
                {
                    foreach (var obstacleEntity in _obstacles.Value)
                    {
                        ref HasObstacleView hasObstacleView = ref _obstacles.Pools.Inc1.Get(obstacleEntity);
                        if (hasObstacleView.view.transform == trigger.senderGameObject.transform)
                        {
                            _obstacles.Value.DelEntity(obstacleEntity);
                            Object.Destroy(trigger.senderGameObject);
                        }
                    }
                }

                _triggers.DelEntity(triggerEntity);
            }
        }
    }
}