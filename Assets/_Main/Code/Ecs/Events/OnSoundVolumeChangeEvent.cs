namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="OnSoundVolumeChangeEvent"/> struct.
    /// This event occurs when the specified sound volume change requested.
    /// </summary>
    struct OnSoundVolumeChangeEvent
    {
        public SoundTarget target;
        public float value;
    }

    /// <summary>
    /// The <see cref="SoundTarget"/> enumeration.
    /// This enumeration defines all the possible kinds of game sound targets.
    /// </summary>
    internal enum SoundTarget { Game, Interface, Music }
}