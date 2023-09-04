using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="ImageHueChangeSystem"/> class.
    /// This system changes (pulses) hue of the assigned <see cref="ImageHueChange"/> components.
    /// </summary>
    internal sealed class ImageHueChangeSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<ImageHueChange>> _changes = GlobalIdents.Worlds.StaticWorld;
        private EcsCustomInject<TimeService> _timeService;

        private TimeService TimeService => _timeService.Value;

        public void Run(IEcsSystems systems)
        {
            foreach (var changeEntity in _changes.Value)
            {
                ref ImageHueChange change = ref _changes.Pools.Inc1.Get(changeEntity);
                change.image.color = change.image.color.PulseHue(change.speed * TimeService.unscaledDeltaTime, change.minHue, change.maxHue, ref change.add);
            }
        }
    }
}