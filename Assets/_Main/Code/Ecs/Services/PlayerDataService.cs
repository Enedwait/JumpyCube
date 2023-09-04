using System;
using OKRT.JumpyCube.Main.Code.Data;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="PlayerDataService"/> class.
    /// This class operates with player data.
    /// </summary>
    internal sealed class PlayerDataService
    {
        private bool _notFound = false;

        /// <summary> Gets the player data. </summary>
        public PlayerData PlayerData { get; private set; }

        /// <summary> Gets the default player data. </summary>
        public PlayerData Default { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerDataService"/> class.
        /// </summary>
        public PlayerDataService()
        {
            Load();

            Default = new PlayerData();
        }

        /// <summary>
        /// Loads the player data from the player preferences.
        /// </summary>
        public void Load()
        {
            try
            {
                PlayerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(GlobalIdents.PlayerPrefsKeys.PlayerData));
                if (PlayerData == null)
                {
                    _notFound = true;
                    PlayerData = new PlayerData();
                }
            }
            catch (Exception ex)
            {
                _notFound = true;
                PlayerData = new PlayerData();
                Debug.LogError(ex);
            }
        }

        /// <summary>
        /// Saves the player data to the player preferences.
        /// </summary>
        public void Save()
        {
            PlayerPrefs.SetString(GlobalIdents.PlayerPrefsKeys.PlayerData, JsonUtility.ToJson(PlayerData));
            PlayerPrefs.Save();
        }

        /// <summary>
        /// Restores the default data.
        /// </summary>
        public void RestoreDefaults()
        {
            PlayerData.gameVolume = Default.gameVolume;
            PlayerData.uiVolume = Default.uiVolume;
            PlayerData.musicVolume = Default.musicVolume;
        }

        /// <summary>
        /// Sets the default volumes.
        /// </summary>
        /// <param name="gameVolume">game volume.</param>
        /// <param name="uiVolume">interface volume.</param>
        /// <param name="musicVolume">music volume.</param>
        public void SetDefaultVolume(float gameVolume, float uiVolume, float musicVolume)
        {
            SetVolume(Default, gameVolume, uiVolume, musicVolume);

            if (_notFound)
                RestoreDefaults();
        }

        /// <summary>
        /// Sets the volumes.
        /// </summary>
        /// <param name="gameVolume">game volume.</param>
        /// <param name="uiVolume">interface volume.</param>
        /// <param name="musicVolume">music volume.</param>
        public void SetVolume(float gameVolume, float uiVolume, float musicVolume) => SetVolume(PlayerData, gameVolume, uiVolume, musicVolume);

        /// <summary>
        /// Sets the volumes of the specified data.
        /// </summary>
        /// <param name="data">data.</param>
        /// <param name="gameVolume">game volume.</param>
        /// <param name="uiVolume">interface volume.</param>
        /// <param name="musicVolume">music volume.</param>
        private void SetVolume(PlayerData data, float gameVolume, float uiVolume, float musicVolume)
        {
            data.gameVolume = gameVolume;
            data.uiVolume = uiVolume;
            data.musicVolume = musicVolume;
        }
    }
}
