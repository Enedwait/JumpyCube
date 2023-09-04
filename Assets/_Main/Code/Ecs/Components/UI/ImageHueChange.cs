using System;
using UnityEngine.UI;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components.UI
{
    /// <summary>
    /// The <see cref="ImageHueChange"/> struct.
    /// This component contains value for the image hue change.
    /// </summary>
    [Serializable]
    struct ImageHueChange
    {
        public Image image;
        public float speed;
        public float minHue;
        public float maxHue;
        public bool add;
    }
}