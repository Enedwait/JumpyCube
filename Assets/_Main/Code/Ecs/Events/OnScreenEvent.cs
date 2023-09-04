using System;
using Leopotam.EcsLite;
using OKRT.JumpyCube.Main.Code.UI;

namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnShowScreenEvent"/> struct.
    /// This event occurs if the specified screen should be shown.
    /// </summary>
    internal struct OnShowScreenEvent
    {
        public Type type;
    }

    /// <summary>
    /// The <see cref="OnHideScreenEvent"/> struct.
    /// This event occurs if the specified screen should be hidden.
    /// </summary>
    internal struct OnHideScreenEvent
    {
        public Type type;
    }

    /// <summary>
    /// The <see cref="ScreenEventExtensions"/> class.
    /// This class contains different helper methods for the ease of work with Show/Hide screen functionality in ECS.
    /// </summary>
    internal static class ScreenEventExtensions
    {
        public static void ShowScreen<T>(this EcsWorld world) where T : UIScreenBase
        {
            int entity = world.NewEntity();
            var pool = world.GetPool<OnShowScreenEvent>();
            pool.Add(entity).type = typeof(T);
        }

        public static void ShowScreen<T>(this EcsPool<OnShowScreenEvent> pool) where T : UIScreenBase
        {
            var world = pool.GetWorld();
            var entity = world.NewEntity();
            pool.Add(entity).type = typeof(T);
        }

        public static void HideScreen<T>(this EcsWorld world) where T : UIScreenBase
        {
            int entity = world.NewEntity();
            var pool = world.GetPool<OnHideScreenEvent>();
            pool.Add(entity).type = typeof(T);
        }

        public static void HideScreen<T>(this EcsPool<OnHideScreenEvent> pool) where T : UIScreenBase
        {
            var world = pool.GetWorld();
            var entity = world.NewEntity();
            pool.Add(entity).type = typeof(T);
        }
    }
}