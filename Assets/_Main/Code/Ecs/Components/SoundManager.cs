using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="SoundManager"/> struct.
    /// This component contains data about global audio sources.
    /// </summary>
    [Serializable]
    struct SoundManager
    {
        public AudioSource game;
        public AudioSource music;
        public AudioSource ui;
        public AudioSource uiMusic;
    }
}