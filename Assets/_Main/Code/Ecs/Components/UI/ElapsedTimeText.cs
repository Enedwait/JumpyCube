using System;
using TMPro;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="ElapsedTimeText"/> struct.
    /// This component provides access to the elapsed time text.
    /// </summary>
    [Serializable]
    struct ElapsedTimeText
    {
        public TextMeshProUGUI text;
        public string format;
    }
}