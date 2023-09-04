using System;
using TMPro;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="VersionText"/> struct.
    /// This component represents the text where the version value should be written.
    /// </summary>
    [Serializable]
    struct VersionText
    {
        public TextMeshProUGUI text;
        public string format;
        public VersionKind kind;
    }

    /// <summary>
    /// The <see cref="VersionKind"/> enumeration.
    /// It determines what version should be used.
    /// </summary>
    internal enum VersionKind { Game, Unity }
}