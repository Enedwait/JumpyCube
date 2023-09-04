using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="HasRigidbody"/> struct.
    /// This component contains a <see cref="Rigidbody"/> inside.
    /// </summary>
    [Serializable]
    struct HasRigidbody
    {
        public Rigidbody rigidbody;
    }
}