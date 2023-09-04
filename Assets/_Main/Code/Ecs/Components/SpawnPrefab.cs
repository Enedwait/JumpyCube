using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Components
{
    /// <summary>
    /// The <see cref="SpawnPrefab"/> struct.
    /// This component represents the prefab with data for instantiation.
    /// </summary>
    struct SpawnPrefab
    {
        public GameObject prefab;
        public Vector3 position;
        public Quaternion rotation;
        public Transform parent;
    }
}