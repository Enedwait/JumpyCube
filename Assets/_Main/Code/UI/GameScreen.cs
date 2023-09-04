using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.UI
{
    /// <summary>
    /// The <see cref="GameScreen"/> class.
    /// This class represent the overall Game UI.
    /// </summary>
    internal sealed class GameScreen : UIScreenBase
    {
        [SerializeField] private GameHUDScreen _hudScreen;
        [SerializeField] private MenuScreen _menuScreen;
        [SerializeField] private AboutScreen _aboutScreen;
        [SerializeField] private SettingsScreen _settingsScreen;
        [SerializeField] private GameOverScreen _gameOverScreen;

        /// <summary>
        /// Activates all the screens to create all the required entity providers respectfully.
        /// </summary>
        private void Awake()
        {
            _hudScreen.Show(null);
            _menuScreen.Show(null);
            _aboutScreen.Show(null);
            _settingsScreen.Show(null);
            _gameOverScreen.Show(null);
        }
    }
}
