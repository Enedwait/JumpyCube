using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Events;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="HiScoreChangedSystem"/> class.
    /// This system updates the hi score texts.
    /// </summary>
    internal sealed class HiScoreChangedSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnUpdateHiScoreEvent>> _hiScoreChanges = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<HiScoreText>> _hiScoreTexts = GlobalIdents.Worlds.StaticWorld;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _hiScoreChanges.Value)
            {
                ref OnUpdateHiScoreEvent change = ref _hiScoreChanges.Pools.Inc1.Get(entity);
                foreach (var textEntity in _hiScoreTexts.Value)
                {
                    ref HiScoreText text = ref _hiScoreTexts.Pools.Inc1.Get(textEntity);
                    text.text.text = $"{change.hiScore}";
                }
            }
        }
    }
}