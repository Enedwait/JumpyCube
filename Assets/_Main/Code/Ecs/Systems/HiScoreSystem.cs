using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="HiScoreSystem"/> class.
    /// This system updates and saves the hi score value.
    /// </summary>
    internal sealed class HiScoreSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsFilterInject<Inc<PlayerScore>> _scores;

        private EcsPoolInject<HiScore> _hiScorePool = GlobalIdents.Worlds.StaticWorld;
        private EcsPoolInject<OnUpdateHiScoreEvent> _hiScoreChangePool = GlobalIdents.Worlds.EventWorld;
        
        private EcsCustomInject<PlayerDataService> _playerDataService;

        private int _hiScoreEntity;
        private bool _isInitialized = false;

        private PlayerDataService PlayerDataService => _playerDataService.Value;

        public void Init(IEcsSystems systems)
        {
            ref HiScore score = ref _hiScorePool.NewEntity(out _hiScoreEntity);
            score.hiScore = PlayerDataService.PlayerData.hiScore;
        }

        public void Run(IEcsSystems systems)
        {
            ref HiScore hiScore = ref _hiScorePool.Value.Get(_hiScoreEntity);

            if (!_isInitialized)
            {
                _hiScoreChangePool.NewEntityImplicit().hiScore = hiScore.hiScore;
                _isInitialized = true;
            }

            foreach (var entity in _scores.Value)
            {
                ref PlayerScore score = ref _scores.Pools.Inc1.Get(entity);
                if (score.score > hiScore.hiScore)
                {
                    hiScore.hiScore = score.score;
                    _hiScoreChangePool.NewEntityImplicit().hiScore = hiScore.hiScore;
                }
            }
        }

        public void Destroy(IEcsSystems systems)
        {
            ref HiScore hiScore = ref _hiScorePool.Value.Get(_hiScoreEntity);
            PlayerDataService.PlayerData.hiScore = hiScore.hiScore;
            PlayerDataService.Save();
        }
    }
}