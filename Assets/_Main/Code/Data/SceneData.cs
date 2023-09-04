using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Data
{
    /// <summary>
    /// The <see cref="SceneData"/> class.
    /// This class serves as a container for the different scene data.
    /// </summary>
    internal sealed class SceneData : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Transform _obstacleSpawnPoint;
        [SerializeField] private Transform _obstacleKiller;
        [SerializeField] private Transform _ceiling;
        [SerializeField] private Transform _ground;
        [SerializeField] private Material _skyboxMaterial;
        [SerializeField] private float _skyRotationSpeed = 1f;
        
        /// <summary> Gets the main camera of the game. </summary>
        public Camera MainCamera => _mainCamera;

        /// <summary> Gets the player spawn point. It serves a host for player also. </summary>
        public Transform PlayerSpawnPoint => _playerSpawnPoint;

        /// <summary> Gets the obstacle spawn point. It serves a host for obstacles also. </summary>
        public Transform ObstacleSpawnPoint => _obstacleSpawnPoint;

        /// <summary> Gets the obstacle killer object. It destroys obstacles if they touch it.  </summary>
        public Transform ObstacleKiller => _obstacleKiller;

        /// <summary> Gets the ceiling. It's the top border of the game. </summary>
        public Transform Ceiling => _ceiling;

        /// <summary> Gets the ground. It's the bottom border of the game. </summary>
        public Transform Ground => _ground;

        /// <summary> Gets the sky box material. </summary>
        public Material SkyBoxMaterial => _skyboxMaterial;

        /// <summary> Gets the sky box rotation speed. </summary>
        public float SkyRotationSpeed => _skyRotationSpeed;

        /// <summary>
        /// Clears the scene from the obstacles and player.
        /// </summary>
        public void Clear()
        {
            _playerSpawnPoint.RemoveChildren();
            _obstacleSpawnPoint.RemoveChildren();
        }
    }
}
