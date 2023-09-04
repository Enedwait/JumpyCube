using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Data
{
    /// <summary>
    /// The <see cref="SoundData"/> class.
    /// This class contains different sounds data for the ease of use in game.
    /// </summary>
    [CreateAssetMenu(menuName = "Game/Data/Sound Data", fileName = "New Sound Data", order = 1)]
    public class SoundData : ScriptableObject
    {
        [SerializeField] private float _gameVolume = 1f;
        [SerializeField] private float _interfaceVolume = 0.9f;
        [SerializeField] private float _musicVolume = 0.75f;

        [SerializeField] private AudioClip _click;
        [SerializeField] private AudioClip _jump;
        [SerializeField] private AudioClip _obstacleHit;
        [SerializeField] private AudioClip _groundHit;
        [SerializeField] private AudioClip _ceilingHit;
        [SerializeField] private AudioClip _scoreIncreased;
        [SerializeField] private AudioClip _gameOver;
        [SerializeField] private AudioClip _gameOverMusic;
        [SerializeField] private AudioClip _menuMusic;
        [SerializeField] private AudioClip _gameMusic;

        /// <summary> Gets the game volume. [0, 1] </summary>
        public float GameVolume => _gameVolume;

        /// <summary> Gets the interface volume. [0, 1] </summary>
        public float InterfaceVolume => _interfaceVolume;

        /// <summary> Gets the music volume. [0, 1] </summary>
        public float MusicVolume => _musicVolume;

        /// <summary> Gets the button click sound. </summary>
        public AudioClip Click => _click;

        /// <summary> Gets the player jump sound. </summary>
        public AudioClip Jump => _jump;

        /// <summary> Gets the obstacle hit sound (when the player touches the obstacle). </summary>
        public AudioClip ObstacleHit => _obstacleHit;

        /// <summary> Gets the ground hit sound (when the player lands). </summary>
        public AudioClip GroundHit => _groundHit;

        /// <summary> Gets the ceiling hit sound (when the player reaches the sky). </summary>
        public AudioClip CeilingHit => _ceilingHit;

        /// <summary> Gets the score increase sound (when the player score increases). </summary>
        public AudioClip ScoreIncreased => _scoreIncreased;

        /// <summary> Gets the game over sound. </summary>
        public AudioClip GameOver => _gameOver;

        /// <summary> Gets the game over music. </summary>
        public AudioClip GameOverMusic => _gameOverMusic;

        /// <summary> Gets the menu music. </summary>
        public AudioClip MenuMusic => _menuMusic;
        
        /// <summary> Gets the game music. </summary>
        public AudioClip GameMusic => _gameMusic;
    }
}