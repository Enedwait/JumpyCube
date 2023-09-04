using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="TimeSystem"/> class.
    /// This systems collects the time information.
    /// </summary>
    internal sealed class TimeSystem : IEcsRunSystem
    {
        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<PauseService> _pauseService;

        public void Run(IEcsSystems systems)
        {
            _timeService.Value.time = Time.time;
            _timeService.Value.unscaledTime = Time.unscaledTime;
            _timeService.Value.deltaTime = Time.deltaTime;
            _timeService.Value.unscaledDeltaTime = Time.unscaledDeltaTime;

            _timeService.Value.elapsedTime += Time.deltaTime;
            _timeService.Value.framesSinceStart += Time.frameCount;

            if (!_pauseService.Value.IsPaused)
            {
                _timeService.Value.elapsedGameplayTime += Time.deltaTime;
            }
        }
    }
}