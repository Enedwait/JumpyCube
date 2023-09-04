using UnityEngine;
using UnityEngine.UI;

namespace OKRT.JumpyCube.Main.Code.UI
{
    /// <summary>
    /// The <see cref="MenuScreen"/> class.
    /// This class represents the menu screen of the game.
    /// </summary>
    internal sealed class MenuScreen : UIScreenBase
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _restartButton;

        /// <summary>
        /// Sets menu in a visual state as if it was called from the game screen.
        /// </summary>
        public void SetIfCalledFromGame()
        {
            _resumeButton.gameObject.SetActive(true);
            _startButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(true);
        }

        /// <summary>
        /// Sets menu in a visual state as if it was called from the game over screen.
        /// </summary>
        public void SetIfCalledFromGameOver()
        {
            _resumeButton.gameObject.SetActive(false);
            _startButton.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(false);
        }
    }
}
