using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="UISoundSystem"/> class.
    ///  This system allows to play interface sound or music.
    /// </summary>
    internal sealed class UISoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SoundManager>> _soundManagers = GlobalIdents.Worlds.StaticWorld;
        private EcsFilterInject<Inc<OnOneShotSoundEvent>> _sounds = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnMenuMusicEvent>> _music = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnGameOverMusicEvent>> _gameOverMusic = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<SoundData> _soundData;

        private SoundData SoundData => _soundData.Value;

        public void Run(IEcsSystems systems)
        {
            ProcessSounds(systems);
            ProcessMenuMusic(systems);
            ProcessGameOverMusic(systems);
        }

        /// <summary>
        /// Processes the interface sound events and plays sounds accordingly.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ProcessSounds(IEcsSystems systems)
        {
            foreach (var soundEntity in _sounds.Value)
            {
                ref OnOneShotSoundEvent sound = ref _sounds.Pools.Inc1.Get(soundEntity);
                AudioClip clip = null;

                switch (sound.kind)
                {
                    case OneShotSoundKind.Click: clip = SoundData.Click; break;
                    default: continue;
                }

                if (clip == null)
                    continue;

                foreach (var soundManagerEntity in _soundManagers.Value)
                {
                    ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);
                    soundManager.ui.ignoreListenerPause = true;
                    soundManager.ui.PlayOneShot(clip);
                }

                _sounds.Pools.Inc1.Del(soundEntity);
            }
        }

        /// <summary>
        /// Processes the menu music events and acts accordingly.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ProcessMenuMusic(IEcsSystems systems)
        {
            foreach (var musicEntity in _music.Value)
            {
                ref OnMenuMusicEvent music = ref _music.Pools.Inc1.Get(musicEntity);

                foreach (var soundManagerEntity in _soundManagers.Value)
                {
                    ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);
                    soundManager.uiMusic.ignoreListenerPause = true;
                    if (music.turnOn)
                    {
                        soundManager.uiMusic.loop = true;
                        soundManager.uiMusic.clip = SoundData.MenuMusic;
                        soundManager.uiMusic.Play();
                    }
                    else
                    {
                        soundManager.uiMusic.Stop();
                    }
                }

                _music.Pools.Inc1.Del(musicEntity);
            }
        }

        /// <summary>
        /// Processes the game over music events and acts accordingly.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ProcessGameOverMusic(IEcsSystems systems)
        {
            foreach (var musicEntity in _gameOverMusic.Value)
            {
                ref OnGameOverMusicEvent music = ref _gameOverMusic.Pools.Inc1.Get(musicEntity);

                foreach (var soundManagerEntity in _soundManagers.Value)
                {
                    ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);
                    soundManager.uiMusic.ignoreListenerPause = true;
                    if (music.turnOn)
                    {
                        soundManager.uiMusic.loop = true;
                        soundManager.uiMusic.clip = SoundData.GameOverMusic;
                        soundManager.uiMusic.PlayDelayed(0.8f);
                    }
                    else
                    {
                        soundManager.uiMusic.Stop();
                    }
                }

                _gameOverMusic.Pools.Inc1.Del(musicEntity);
            }
        }
    }
}