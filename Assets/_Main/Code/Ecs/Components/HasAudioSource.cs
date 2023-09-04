using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="HasAudioSource"/> struct.
    /// This component contains the data of the <see cref="AudioSource"/>.
    /// </summary>
    [Serializable]
    struct HasAudioSource
    {
        public AudioSource source;
    }
}