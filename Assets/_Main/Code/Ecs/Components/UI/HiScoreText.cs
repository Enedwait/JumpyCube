using System;
using TMPro;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="HiScoreText"/> struct.
    /// This component represents the text where the HiScore value should be written.
    /// </summary>
    [Serializable]
    struct HiScoreText
    {
        public TextMeshProUGUI text;
    }
}
