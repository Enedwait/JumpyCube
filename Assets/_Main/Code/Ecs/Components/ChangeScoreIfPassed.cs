using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="ChangeScoreIfPassed"/> struct.
    /// This component represents an object which should change the player score if being passed by player.
    /// </summary>
    [Serializable]
    struct ChangeScoreIfPassed
    {
        public Transform transform;
        public long change;
        public float distance;
    }
}