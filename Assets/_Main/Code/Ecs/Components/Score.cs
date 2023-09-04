namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="PlayerScore"/> struct.
    /// This component contains the player score data.
    /// </summary>
    struct PlayerScore
    {
        public long score;
    }

    /// <summary>
    /// The <see cref="HiScore"/> struct.
    /// This component contains the highest score data.
    /// </summary>
    struct HiScore
    {
        public long hiScore;
    }
}