using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="HasParticleSystem"/> struct.
    /// This component contains the data of the <see cref="ParticleSystem"/>.
    /// </summary>
    [Serializable]
    struct HasParticleSystem
    {
        public ParticleSystem system;
    }
}