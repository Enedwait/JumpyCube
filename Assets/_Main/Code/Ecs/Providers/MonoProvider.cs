using Leopotam.EcsLite;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Providers
{
    /// <summary>
    /// The <see cref="MonoProvider<T>"/> class.
    /// This is a base class for 'object into entity' converters with specific component attached.
    /// </summary>
    /// <typeparam name="T">component</typeparam>
    [RequireComponent(typeof(EntityProvider))]
    internal abstract class MonoProvider<T> : MonoProviderBase where T : struct
    {
        [SerializeField, Tooltip("Defines the component attached")] protected T value;

        /// <summary>
        /// Converts the current object into an entity in the specified world and adds the class specific component to the pool.
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="world">ECS world</param>
        public override void Convert(int entity, EcsWorld world)
        {
            var pool = world.GetPool<T>();

            pool.Add(entity) = value;
        }
    }
}
