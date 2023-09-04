using System;
using OKRT.JumpyCube.Main.Code.Views;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="HasPlayerView"/> struct.
    /// This component contains the data of the <see cref="PlayerView"/>.
    /// </summary>
    [Serializable]
    struct HasPlayerView
    {
        public PlayerView view;
    }
}