using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Data
{
    /// <summary>
    /// The <see cref="StaticData"/> class.
    /// This class contains different data for the ease of use in game.
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Data/Static Data", fileName = "New Static Data", order = 0)]
    public class StaticData : ScriptableObject
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _obstaclePrefab;
        [SerializeField] private float _obstacleSpawnDelay = 1f;
        [SerializeField] private float _obstacleSpeed = 1f;
        [SerializeField] private float _changeSpeedDelay = 10f;
        [SerializeField] private float _changeSpeedFactor = 1.1f;
        [SerializeField] private float _changeObstacleDelayFactor = 1.05f;
        [SerializeField] private float _changeScoreFactor = 1.025f;

        /// <summary> Gets the player prefab. </summary>
        public GameObject PlayerPrefab => _playerPrefab;

        /// <summary> Gets the obstacle prefab. </summary>
        public GameObject ObstaclePrefab => _obstaclePrefab;

        /// <summary> Gets the obstacle spawn delay. </summary>
        public float ObstacleSpawnDelay => _obstacleSpawnDelay;

        /// <summary> Gets the obstacle speed. </summary>
        public float ObstacleSpeed => _obstacleSpeed;
        
        /// <summary> Gets the delay until the speed should be changed. </summary>
        public float ChangeSpeedDelay => _changeSpeedDelay;

        /// <summary> Gets the speed change factor. </summary>
        public float ChangeSpeedFactor => _changeSpeedFactor;

        /// <summary> Gets the delay until next obstacle should be spawned. </summary>
        public float ChangeObstacleDelayFactor => _changeObstacleDelayFactor;

        /// <summary> Gets the score change factor. </summary>
        public float ChangeScoreFactor => _changeScoreFactor;
    }
}
