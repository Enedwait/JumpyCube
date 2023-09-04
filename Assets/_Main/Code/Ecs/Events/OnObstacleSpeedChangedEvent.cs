using System;

namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnObstacleSpeedChangedEvent"/> struct.
    /// This event occurs when the obstacle speed changed.
    /// </summary>
    [Serializable]
    struct OnObstacleSpeedChangedEvent
    {
        public float speed;
    }
}