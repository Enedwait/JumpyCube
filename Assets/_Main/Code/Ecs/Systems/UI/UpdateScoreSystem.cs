using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="UpdateScoreSystem"/> class.
    /// This system updates the score.
    /// </summary>
    internal sealed class UpdateScoreSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<ScoreText>> _scoreTexts = GlobalIdents.Worlds.StaticWorld;
        private EcsFilterInject<Inc<OnUpdateScoreEvent>> _updateScore = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<OnOneShotSoundEvent> _sounds = GlobalIdents.Worlds.EventWorld;

        public void Run (IEcsSystems systems)
        {
            foreach (var updateScoreEntity in _updateScore.Value)
            {
                _sounds.NewEntityImplicit().kind = OneShotSoundKind.ScoreIncreased;

                ref OnUpdateScoreEvent updateScoreEvent = ref _updateScore.Pools.Inc1.Get(updateScoreEntity);

                foreach (var scpreTextEntity in _scoreTexts.Value)
                    _scoreTexts.Pools.Inc1.Get(scpreTextEntity).text.text = $"{updateScoreEvent.score}";

                _updateScore.DelEntity(updateScoreEntity);
            }
        }
    }
}