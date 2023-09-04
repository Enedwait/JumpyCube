namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="JumpCommand"/> struct.
    /// This component stores information about requested jump and it's previous value.
    /// </summary>
    struct JumpCommand
    {
        public bool jump;
        public bool previousJump;
    }
}