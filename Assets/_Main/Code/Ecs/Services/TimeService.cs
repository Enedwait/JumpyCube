namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="TimeService"/> class.
    /// This class allows to store and process time information.
    /// </summary>
    internal sealed class TimeService
    {
        public float time;
        public float deltaTime;
        public float unscaledDeltaTime;
        public float unscaledTime;

        public float elapsedTime;
        public float elapsedGameplayTime;

        public int framesSinceStart;

        public void Reset()
        {
            time = 0;
            deltaTime = 0;
            unscaledDeltaTime = 0;
            unscaledTime = 0;
            elapsedTime = 0;
            elapsedGameplayTime = 0;
        }
    }
}
