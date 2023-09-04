using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="SoundManagementSystem"/> class.
    /// This system controls sound parameters (e.g. volume).
    /// </summary>
    internal sealed class SoundManagementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<SoundManager>> _soundManagers = GlobalIdents.Worlds.StaticWorld;
        private EcsFilterInject<Inc<OnSoundVolumeChangeEvent>> _volumeChanged = GlobalIdents.Worlds.EventWorld;

        public void Run(IEcsSystems systems)
        {
            foreach (var volumeChangedEntity in _volumeChanged.Value)
            {
                ref OnSoundVolumeChangeEvent changeEvent = ref _volumeChanged.Pools.Inc1.Get(volumeChangedEntity);
                if (changeEvent.value < 0.001f)
                    changeEvent.value = 0.001f;

                float volume = Mathf.Log(changeEvent.value) * 20; // conversion from (0, 1] to (-80, 0] (e.g. from linear to logarithmic scale)

                foreach (var soundManagerEntity in _soundManagers.Value)
                {
                    ref SoundManager soundManager = ref _soundManagers.Pools.Inc1.Get(soundManagerEntity);

                    switch (changeEvent.target)
                    {
                        case SoundTarget.Game: soundManager.game.outputAudioMixerGroup.audioMixer.SetFloat("GameVolume", volume); break;
                        case SoundTarget.Interface: soundManager.ui.outputAudioMixerGroup.audioMixer.SetFloat("UIVolume", volume); break;
                        case SoundTarget.Music: soundManager.music.outputAudioMixerGroup.audioMixer.SetFloat("MusicVolume", volume); break;
                    }
                }

                _volumeChanged.Pools.Inc1.Del(volumeChangedEntity);
            }
        }
    }
}