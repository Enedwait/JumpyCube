using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Services;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="UpdateElapsedTimeSystem"/> class.
    /// This system updates the elapsed time UI element.
    /// </summary>
    internal sealed class UpdateElapsedTimeSystem : IEcsRunSystem
    {
        private EcsCustomInject<TimeService> _timeService;
        private EcsFilterInject<Inc<ElapsedTimeText>> _texts = GlobalIdents.Worlds.StaticWorld;
        
        private const string DefaultFormat = "{0:hh\\:mm\\:ss\\.f}";

        public void Run(IEcsSystems systems)
        {
            foreach (var textEntity in _texts.Value)
            {
                ref ElapsedTimeText text = ref _texts.Pools.Inc1.Get(textEntity);
                text.text.text = string.Format(string.IsNullOrWhiteSpace(text.format) ? DefaultFormat : text.format, TimeSpan.FromSeconds(_timeService.Value.elapsedGameplayTime));
            }
        }
    }
}