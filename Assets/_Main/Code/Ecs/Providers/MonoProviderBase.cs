using Leopotam.EcsLite;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Providers
{
    /// <summary>
    /// The <see cref="MonoProviderBase"/> class.
    /// This is a base class for all the 'object into entity' converters.
    /// </summary>
    [RequireComponent(typeof(EntityProvider))]
    internal abstract class MonoProviderBase : MonoBehaviour
    {
        /// <summary>
        /// Converts the current object into an entity in the specified world.
        /// </summary>
        /// <param name="entity">entity</param>
        /// <param name="world">ECS world</param>
        public abstract void Convert(int entity, EcsWorld world);
    }
}
