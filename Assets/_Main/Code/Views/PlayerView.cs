using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Views
{
    /// <summary>
    /// The <see cref="PlayerView"/> class.
    /// This class represents the player.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 7f;
        [SerializeField] private float _width = 1f;
        [SerializeField] private AudioSource _audioSource;

        /// <summary> Gets the jump force of the player. </summary>
        public float JumpForce => _jumpForce;

        /// <summary> Gets the width of the player. </summary>
        public float Width => _width;

        /// <summary> Gets the audio source of the player. </summary>
        public AudioSource AudioSource => _audioSource;
    }
}
