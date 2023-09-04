using Leopotam.EcsLite;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="OnPlayerDiedEvent"/> struct.
    /// This event occurs if the player died.
    /// </summary>
    struct OnPlayerDiedEvent
    {
        public EcsPackedEntityWithWorld playerEntity;
    }
}