using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="GameSoundSystem"/> class.
    /// This system allows to play game sound or music.
    /// </summary>
    internal sealed class GameSoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SoundManager>> _soundManagers = GlobalIdents.Worlds.StaticWorld;
        private EcsFilterInject<Inc<OnOneShotSoundEvent>> _sounds = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<OnGameMusicEvent>> _music = GlobalIdents.Worlds.EventWorld;

        private EcsCustomInject<SoundData> _soundData;

        private SoundData SoundData => _soundData.Value;

        public void Run(IEcsSystems systems)
        {
            ProcessSounds(systems);
            ProcessGameMusic(systems);
        }

        /// <summary>
        /// Processes the game sound events and plays sounds accordingly.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ProcessSounds(IEcsSystems systems)
        {
            foreach (var soundEntity in _sounds.Value)
            {
                bool soundManagerRequired = false;
                AudioClip clip = null;

                ref OnOneShotSoundEvent sound = ref _sounds.Pools.Inc1.Get(soundEntity);
                switch (sound.kind)
                {
                    case OneShotSoundKind.Jump:
                        ref Player playerJumped = ref _sounds.Pools.Inc1.Get(soundEntity).entity.UnpackImplicitly<Player>();
                        playerJumped.audioSource.PlayOneShot(SoundData.Jump);
                        break;
                    case OneShotSoundKind.GroundHit:
                        ref Player playerHitGround = ref _sounds.Pools.Inc1.Get(soundEntity).entity.UnpackImplicitly<Player>();
                        playerHitGround.audioSource.PlayOneShot(SoundData.GroundHit);
                        break;
                    case OneShotSoundKind.CeilingHit:
                        ref Player playerHitCeiling = ref _sounds.Pools.Inc1.Get(soundEntity).entity.UnpackImplicitly<Player>();
                        playerHitCeiling.audioSource.PlayOneShot(SoundData.CeilingHit);
                        break;
                    case OneShotSoundKind.ObstacleHit:
                        ref Player playerHitObstacle = ref _sounds.Pools.Inc1.Get(soundEntity).entity.UnpackImplicitly<Player>();
                        playerHitObstacle.audioSource.PlayOneShot(SoundData.ObstacleHit);
                        break;
                    case OneShotSoundKind.ScoreIncreased: 
                        soundManagerRequired = true;
                        clip = SoundData.ScoreIncreased;
                        break;
                    case OneShotSoundKind.Death:
                        soundManagerRequired = true;
                        clip = SoundData.GameOver;
                        break;
                    case OneShotSoundKind.Click: continue;
                }

                if (soundManagerRequired)
                {
                    foreach (var soundManagerEntity in _soundManagers.Value)
                    {
                        ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);
                        soundManager.game.ignoreListenerPause = true;
                        soundManager.game.PlayOneShot(clip);
                    }
                }
                
                _sounds.DelEntity(soundEntity);
            }
        }

        /// <summary>
        /// Processes the game music events and acts accordingly.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ProcessGameMusic(IEcsSystems systems)
        {
            foreach (var musicEntity in _music.Value)
            {
                ref OnGameMusicEvent music = ref _music.Pools.Inc1.Get(musicEntity);

                foreach (var soundManagerEntity in _soundManagers.Value)
                {
                    ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);
                    soundManager.music.ignoreListenerPause = false;
                    if (music.turnOn)
                    {
                        soundManager.music.loop = true;
                        soundManager.music.clip = SoundData.GameMusic;
                        soundManager.music.Play();
                    }
                    else
                    {
                        soundManager.music.Stop();
                    }
                }
                
                _music.DelEntity(musicEntity);
            }
        }
    }
}