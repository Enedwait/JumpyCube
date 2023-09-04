using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="Player"/> struct.
    /// This component contains the player's general data.
    /// </summary>
    [Serializable]
    struct Player
    {
        public Transform transform;
        public float jumpForce;
        public float width;
        public AudioSource audioSource;
    }
}