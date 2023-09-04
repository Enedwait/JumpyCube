using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Services;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="SkyMoveSystem"/> class.
    /// This system rotates the sky.
    /// </summary>
    internal sealed class SkyMoveSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<GameComplexity>> _gameComplexity;

        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<PauseService> _pauseService;

        private SceneData SceneData => _sceneData.Value;

        public void Run(IEcsSystems systems)
        {
            if (_pauseService.Value.IsPaused)
                return;

            float factor = 1f;
            foreach (var gameComplexityEntity in _gameComplexity.Value)
                factor = _gameComplexity.Pools.Inc1.Get(gameComplexityEntity).levelOfSpeed;

            float current = SceneData.SkyBoxMaterial.GetFloat("_Rotation");
            SceneData.SkyBoxMaterial.SetFloat("_Rotation", current + SceneData.SkyRotationSpeed * _timeService.Value.deltaTime * factor);
        }
    }
}