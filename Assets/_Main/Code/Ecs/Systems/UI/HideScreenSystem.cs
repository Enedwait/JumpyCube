using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Events;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="HideScreenSystem"/> class.
    /// This system processes <see cref="OnHideScreenEvent"/> events and allows to hide screens accordingly.
    /// </summary>
    internal sealed class HideScreenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnHideScreenEvent>> _hideRequests = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<HasScreen>> _screens = GlobalIdents.Worlds.StaticWorld;

        private EcsWorld _world;

        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var showEntity in _hideRequests.Value)
            {
                if (!_hideRequests.Pools.Inc1.Has(showEntity))
                    continue;

                ref OnHideScreenEvent hideScreen = ref _hideRequests.Pools.Inc1.Get(showEntity);

                foreach (var screenEntity in _screens.Value)
                {
                    ref HasScreen hasScreen = ref _screens.Pools.Inc1.Get(screenEntity);
                    if (hasScreen.screen.GetType() == hideScreen.type)
                        hasScreen.screen.Hide(_world);
                }

                _hideRequests.Pools.Inc1.Del(showEntity);
            }
        }
    }
}