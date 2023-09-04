namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnMenuMusicEvent"/> struct.
    /// This event occurs if the menu music state change requested.
    /// </summary>
    struct OnMenuMusicEvent
    {
        public bool turnOn;
    }

    /// <summary>
    /// The <see cref="OnMenuMusicEvent"/> struct.
    /// This event occurs if the game music state change requested.
    /// </summary>
    struct OnGameMusicEvent
    {
        public bool turnOn;
    }

    /// <summary>
    /// The <see cref="OnGameOverMusicEvent"/> struct.
    /// This event occurs if the game over music state change requested.
    /// </summary>
    struct OnGameOverMusicEvent
    {
        public bool turnOn;
    }
}