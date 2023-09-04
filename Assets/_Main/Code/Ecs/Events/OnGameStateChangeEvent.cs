namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnGameStateChangeEvent"/> struct.
    /// This event occurs when the game state change required.
    /// </summary>
    struct OnGameStateChangeEvent
    {
        public GameState state;
    }

    /// <summary>
    /// The <see cref="GameState"/> enumeration.
    /// This enumeration defines a set of game final states and transition states.
    /// </summary>
    internal enum GameState { None, Menu, Game, StartGame, RestartGame, ResumeGame, GameOver, About, Settings, Exit, GoBack, RestoreDefaults }
}