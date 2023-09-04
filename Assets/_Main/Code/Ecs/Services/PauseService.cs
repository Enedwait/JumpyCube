using System;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="PauseService"/> class.
    /// This service allows to control the pause/resume states of the game.
    /// </summary>
    internal sealed class PauseService
    {
        public event Action<bool> StateChanged;

        /// <summary> Gets the value indicating whether the game is paused or not. </summary>
        public bool IsPaused { get; private set; }

        /// <summary> Gets the original time scale which was used before game paused. </summary>
        public float TimeScale { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseService"/> class.
        /// </summary>
        public PauseService()
        {
            if (Time.timeScale <= 0)
            {
                Pause();
                TimeScale = 1f;
            }
            else
            {
                TimeScale = Time.timeScale;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PauseService"/> class.
        /// </summary>
        /// <param name="paused">initial paused state.</param>
        public PauseService(bool paused) : this()
        {
            if (paused) Pause();
            else Resume();
        }

        /// <summary>
        /// Pauses the game (if not already paused).
        /// </summary>
        public void Pause()
        {
            if (IsPaused)
                return;

            IsPaused = true;

            TimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = IsPaused;

            StateChanged?.Invoke(IsPaused);
        }

        /// <summary>
        /// Resumes the paused game.
        /// </summary>
        public void Resume()
        {
            if (!IsPaused)
                return;

            IsPaused = false;
            
            Time.timeScale = TimeScale;
            AudioListener.pause = IsPaused;

            StateChanged?.Invoke(IsPaused);
        }
    }
}
