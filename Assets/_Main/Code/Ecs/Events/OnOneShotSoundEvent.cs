using Leopotam.EcsLite;

namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnOneShotSoundEvent"/> struct.
    /// This event occurs when it's required to play OneShot audio clip.
    /// </summary>
    struct OnOneShotSoundEvent
    {
        public EcsPackedEntityWithWorld entity;
        public OneShotSoundKind kind;
    }

    /// <summary>
    /// The <see cref="OneShotSoundKind"/> enumeration.
    /// It determines what kind of sound should be used.
    /// </summary>
    internal enum OneShotSoundKind { None, Jump, ObstacleHit, CeilingHit, GroundHit, Death, ScoreIncreased, Click }
}