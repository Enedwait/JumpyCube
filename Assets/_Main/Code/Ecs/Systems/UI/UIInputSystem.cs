using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using OKRT.JumpyCube.Main.Code.Ecs.Components;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine.Scripting;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="UIInputSystem"/> class.
    /// This system processes the user interface input.
    /// </summary>
    sealed class UIInputSystem : EcsUguiCallbackSystem
    {
        private EcsPoolInject<OnPlayerPressedJumpEvent> _jumpsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnOneShotSoundEvent> _soundsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnSoundVolumeChangeEvent> _volumeChangedPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnGameStateChangeEvent> _requestsPool = GlobalIdents.Worlds.EventWorld;

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Jump, GlobalIdents.Worlds.EventWorld)]
        private void OnClickJump(in EcsUguiClickEvent e)
        {
            _soundsPool.NewEntityImplicit().kind = OneShotSoundKind.Click;
            _jumpsPool.NewEntityImplicit();
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Menu, GlobalIdents.Worlds.EventWorld)]
        private void OnMenuClick(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.Menu;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Resume, GlobalIdents.Worlds.EventWorld)]
        private void OnClickResume(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.ResumeGame;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Start, GlobalIdents.Worlds.EventWorld)]
        private void OnClickStart(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.StartGame;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Restart, GlobalIdents.Worlds.EventWorld)]
        private void OnClickRestart(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.RestartGame;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Settings, GlobalIdents.Worlds.EventWorld)]
        private void OnClickSettings(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.Settings;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.About, GlobalIdents.Worlds.EventWorld)]
        private void OnClickAbout(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.About;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Close, GlobalIdents.Worlds.EventWorld)]
        private void OnCloseClick(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.Menu;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Default, GlobalIdents.Worlds.EventWorld)]
        private void OnDefaultClick(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.RestoreDefaults;
        }

        [Preserve]
        [EcsUguiClickEvent(GlobalIdents.UI.Exit, GlobalIdents.Worlds.EventWorld)]
        private void OnExitClick(in EcsUguiClickEvent e)
        {
            _requestsPool.NewEntityImplicit().state = GameState.Exit;
        }

        [Preserve]
        [EcsUguiSliderChangeEvent(GlobalIdents.UI.GameSoundsSlider, GlobalIdents.Worlds.EventWorld)]
        private void OnGameVolumeChanged(in EcsUguiSliderChangeEvent e)
        {
            ChangeVolume(SoundTarget.Game, e.Value);
        }

        [Preserve]
        [EcsUguiSliderChangeEvent(GlobalIdents.UI.InterfaceSoundsSlider, GlobalIdents.Worlds.EventWorld)]
        private void OnInterfaceVolumeChanged(in EcsUguiSliderChangeEvent e)
        {
            ChangeVolume(SoundTarget.Interface, e.Value);
        }

        [Preserve]
        [EcsUguiSliderChangeEvent(GlobalIdents.UI.MusicSlider, GlobalIdents.Worlds.EventWorld)]
        private void OnGameMusicVolumeChanged(in EcsUguiSliderChangeEvent e)
        {
            ChangeVolume(SoundTarget.Music, e.Value);
        }

        /// <summary>
        /// Raises the event to change the volume of the specified target.
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