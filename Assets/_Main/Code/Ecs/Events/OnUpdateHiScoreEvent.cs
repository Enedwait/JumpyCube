namespace OKRT.JumpyCube.Main.Code.Ecs.Events
{
    /// <summary>
    /// The <see cref="OnUpdateHiScoreEvent"/> component.
    /// This event occurs if the hi score update is required.
    /// </summary>
    struct OnUpdateHiScoreEvent
    {
        public long hiScore;
    }
}