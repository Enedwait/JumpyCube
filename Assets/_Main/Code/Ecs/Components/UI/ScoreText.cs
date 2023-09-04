using System;
using TMPro;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="ScoreText"/> struct.
    /// This component represents the text where the score value should be written.
    /// </summary>
    [Serializable]
    struct ScoreText
    {
        public TextMeshProUGUI text;
    }
}