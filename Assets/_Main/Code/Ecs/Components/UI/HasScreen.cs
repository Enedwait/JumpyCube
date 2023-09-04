using System;
using OKRT.JumpyCube.Main.Code.UI;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="HasScreen"/> struct.
    /// This component contains the screen data.
    /// </summary>
    [Serializable]
    struct HasScreen
    {
        public UIScreenBase screen;
    }
}