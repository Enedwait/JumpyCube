using System;
using OKRT.JumpyCube.Main.Code.UI;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="GameSettings"/> struct.
    /// This component represents the game settings data.
    /// </summary>
    [Serializable]
    struct GameSettings
    {
        public SliderWithText gameVolume;
        public SliderWithText uiVolume;
        public SliderWithText musicVolume;
    }
}