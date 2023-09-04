using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="GameSettingsSystem"/> class.
    /// This system controls the game settings data.
    /// </summary>
    internal sealed class GameSettingsSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<GameSettings>> _settings = GlobalIdents.Worlds.StaticWorld;
        private EcsFilterInject<Inc<OnLoadSettingsEvent>> _load = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnSaveSettingsEvent>> _save = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnRestoreSettingsEvent>> _restore = GlobalIdents.Worlds.EventWorld;

        private EcsPoolInject<OnSoundVolumeChangeEvent> _volumeChangedPool = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<SoundData> _soundData;
        private EcsCustomInject<PlayerDataService> _playerDataService;

        private SoundData SoundData => _soundData.Value;
        private PlayerDataService PlayerDataService => _playerDataService.Value;

        /// <summary> Gets the value indicating whether the settings were initialized or not. </summary>
        internal bool IsInitialized { get; private set; }

        public void Run(IEcsSystems systems)
        {
            if (Initialize(systems))
            {
                LoadSettings(systems);
                SaveSettings(systems);
                RestoreSettings(systems);
            }
        }

        /// <summary>
        /// Initializes the settings at the 1st time.
        /// Sets the default settings (from UI) and applies the current user settings.
        /// </summary>
        /// <param name="systems">systems.</param>
        /// <returns><value>true</value> if the setings were initialized; otherwise <value>false</value>.</returns>
        private bool Initialize(IEcsSystems systems)
        {
            if (IsInitialized) return true;

            foreach (var settingsEntity in _settings.Value)
            {
                ref GameSettings settings = ref _settings.Pools.Inc1.Get(settingsEntity);
                PlayerDataService.SetDefaultVolume(SoundData.GameVolume, SoundData.InterfaceVolume, SoundData.MusicVolume);

                ChangeVolume(SoundTarget.Game, PlayerDataService.PlayerData.gameVolume);
                ChangeVolume(SoundTarget.Interface, PlayerDataService.PlayerData.uiVolume);
                ChangeVolume(SoundTarget.Music, PlayerDataService.PlayerData.musicVolume);

                return IsInitialized = true;
            }

            return false;
        }

        /// <summary>
        /// Loads the settings from the player data and applies them to UI.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void LoadSettings(IEcsSystems systems)
        {
            foreach (var loadEntity in _load.Value)
            {
                foreach (var settingsEntity in _settings.Value)
                {
                    ref GameSettings settings = ref _settings.Pools.Inc1.Get(settingsEntity);
                    settings.gameVolume.ChangeValue(PlayerDataService.PlayerData.gameVolume);
                    settings.uiVolume.ChangeValue(PlayerDataService.PlayerData.uiVolume);
                    settings.musicVolume.ChangeValue(PlayerDataService.PlayerData.musicVolume);
                    break;
                }

                _load.Pools.Inc1.Del(loadEntity);
            }
        }

        /// <summary>
        /// Saves the settings from UI to the player data and applies them.
        /// </summary>
        /// <param name="systems"></param>
        private void SaveSettings(IEcsSystems systems)
        {
            foreach (var saveEntity in _save.Value)
            {
                foreach (var settingsEntity in _settings.Value)
                {
                    ref GameSettings settings = ref _settings.Pools.Inc1.Get(settingsEntity);
                    PlayerDataService.SetVolume(settings.gameVolume.Value, settings.uiVolume.Value, settings.musicVolume.Value);
                    break;
                }

                PlayerDataService.Save();

                _save.Pools.Inc1.Del(saveEntity);
            }
        }

        /// <summary>
        /// Restores the default settings and applies them to UI.
        /// </summary>
        /// <param name="systems"></param>
        private void RestoreSettings(IEcsSystems systems)
        {
            foreach (var restoreEntity in _restore.Value)
            {
                PlayerDataService.RestoreDefaults();

                foreach (var settingsEntity in _settings.Value)
                {
                    ref GameSettings settings = ref _settings.Pools.Inc1.Get(settingsEntity);
                    settings.gameVolume.ChangeValue(PlayerDataService.PlayerData.gameVolume);
                    settings.uiVolume.ChangeValue(PlayerDataService.PlayerData.uiVolume);
                    settings.musicVolume.ChangeValue(PlayerDataService.PlayerData.musicVolume);
                    break;
                }

                _restore.Pools.Inc1.Del(restoreEntity);
            }
        }

        /// <summary>
        /// Sends request to change the specified volume.
        /// </summary>
        /// <param name="target">target.</param>
        /// <param name="volume">volume.</param>
        private void ChangeVolume(SoundTarget target, float volume)
        {
            ref OnSoundVolumeChangeEvent volumeChangeEvent = ref _volumeChangedPool.NewEntityImplicit();
            volumeChangeEvent.target = target;
            volumeChangeEvent.value = volume;
        }
    }
}