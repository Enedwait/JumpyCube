using System;

namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnObstacleSpawnDelayChangedEvent"/> struct.
    /// This event occurs when the obstacle spawn delay changed.
    /// </summary>
    [Serializable]
    struct OnObstacleSpawnDelayChangedEvent
    {
        public float delay;
    }
}