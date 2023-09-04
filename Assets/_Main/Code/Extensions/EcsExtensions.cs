using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace OKRT.JumpyCube.Main.Code.Extensions
{
    /// <summary>
    /// The <see cref="EcsExtensions"/> class.
    /// This class contains different helper methods for the ease of work with ECS classes.
    /// </summary>
    internal static class EcsExtensions
    {
        #region New Entity

        /// <summary>
        /// Creates a new entity in the specified pool and returns only component.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="pool">pool.</param>
        /// <returns>component.</returns>
        public static ref T NewEntityImplicit<T>(this EcsPool<T> pool) where T : struct
        {
            int entity = pool.GetWorld().NewEntity();
            return ref pool.Add(entity);
        }

        /// <summary>
        /// Creates a new entity in the specified pool and returns only component.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="poolInject">injected pool.</param>
        /// <returns>component.</returns>
        public static ref T NewEntityImplicit<T>(this in EcsPoolInject<T> poolInject) where T : struct => ref poolInject.Value.NewEntityImplicit();

        #endregion

        #region Get or Add Entity

        /// <summary>
        /// Gets an existing or creates a new entity (if not found) in the specified pool and then return the component.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="pool">pool.</param>
        /// <param name="entity">entity.</param>
        /// <returns>component.</returns>
        public static ref T GetOrAdd<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
                return ref pool.Get(entity);

            return ref pool.Add(entity);
        }

        #endregion

        #region Delete Entity

        /// <summary>
        /// Deletes the specified entity entirely.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="pool">pool.</param>
        /// <param name="entity">entity.</param>
        public static void DelEntity<T>(this EcsPool<T> pool, int entity) where T : struct => pool.GetWorld().DelEntity(entity);

        /// <summary>
        /// Deletes the specified entity entirely.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="pool">injected pool.</param>
        /// <param name="entity">entity.</param>
        public static void DelEntity<T>(this EcsPoolInject<T> pool, int entity) where T : struct => pool.Value.DelEntity(entity);

        /// <summary>
        /// Deletes the specified entity entirely.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="inc">include.</param>
        /// <param name="entity">entity.</param>
        public static void DelEntity<T>(this Inc<T> inc, int entity) where T : struct => inc.Inc1.DelEntity(entity);

        /// <summary>
        /// Deletes the specified entity entirely.
        /// </summary>
        /// <param name="filter">filter.</param>
        /// <param name="entity">entity.</param>
        public static void DelEntity(this EcsFilter filter, int entity) => filter.GetWorld().DelEntity(entity);

        /// <summary>
        /// Deletes the specified entity entirely.
        /// </summary>
        /// <typeparam name="T">type of component.</typeparam>
        /// <param name="filter">injected filter.</param>
        /// <param name="entity">entity.</param>
        public static void DelEntity<T>(this EcsFilterInject<Inc<T>> filter, int entity) where T : struct => filter.Value.DelEntity(entity);

        #endregion

        #region Remove Entities

        /// <summary>
        /// Removes all of the entities of the specified world.
        /// </summary>
        /// <param name="world">world to be cleaned up.</param>
        public static void RemoveEntities(this EcsWorld world)
        {
            int[] entitties = new int[world.GetEntitiesCount()];
            world.GetAllEntities(ref entitties);

            for (int i = 0; i < entitties.Length; i++)
                world.DelEntity(entitties[i]);
        }

        #endregion

        #region Unpack

        /// <summary>
        /// Tries to unpack the packed entity with return of the specific component (if available).
        /// </summary>
        /// <remarks>CAN FAIL! NO SAFETY CHECKS!</remarks>
        /// <typeparam name="T">component type of the packed entity</typeparam>
        /// <param name="packedEntity">packed entity.</param>
        /// <returns>component of the packed entity.</returns>
        public static ref T UnpackImplicitly<T>(this EcsPackedEntityWithWorld packedEntity) where T : struct
        {
            packedEntity.Unpack(out EcsWorld world, out int entity);
            ref var component = ref world.GetPool<T>().Get(entity);
            return ref component;
        }

        #endregion
    }
}
