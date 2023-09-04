using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Components.UI;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Extensions;
using OKRT.JumpyCube.Main.Code.UI;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="GameStateManagementSystem"/> class.
    /// This system allows to change game states accordingly both logical and visual.
    /// </summary>
    internal sealed class GameStateManagementSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnGameStateChangeEvent>> _stateChanges = GlobalIdents.Worlds.EventWorld;
        private EcsFilterInject<Inc<HasScreen>> _screens = GlobalIdents.Worlds.StaticWorld;

        private EcsPoolInject<OnOneShotSoundEvent> _soundsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnGameMusicEvent> _gameMusicPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnMenuMusicEvent> _menuMusicPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnGameOverMusicEvent> _gameOverMusicPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnRestoreSettingsEvent> _restoreSettingsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnLoadSettingsEvent> _loadSettingsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnSaveSettingsEvent> _saveSettingsPool = GlobalIdents.Worlds.EventWorld;
        
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<PauseService> _pauseService;
        private EcsCustomInject<EcsStartup> _ecsStartup;

        private EcsWorld _eventWorld;
        private bool _delEntity;
        private MenuScreen _menu;
        
        /// <summary> Gets the current game state of the game. </summary>
        internal GameState State { get; private set; }

        /// <summary> Gets the value indicating whether the game is started or not. </summary>
        internal bool IsGameStarted { get; private set; }

        public void Init(IEcsSystems systems)
        {
            _stateChanges.Pools.Inc1.NewEntityImplicit().state = GameState.Menu;
            _eventWorld = systems.GetWorld(GlobalIdents.Worlds.EventWorld);
        }

        public void Run(IEcsSystems systems)
        {
            GetMenu();

            foreach (var entity in _stateChanges.Value)
            {
                _delEntity = true;
                ref OnGameStateChangeEvent stateChange = ref _stateChanges.Pools.Inc1.Get(entity);

                switch (stateChange.state)
                {
                    case GameState.RestoreDefaults: RestoreDefaults(systems); break;
                    case GameState.GoBack: GoBack(systems); break;
                    case GameState.Exit: ToExit(systems); break;
                    case GameState.Menu: ToMenu(systems); break;
                    case GameState.ResumeGame: case GameState.StartGame:
                    case GameState.RestartGame: case GameState.Game: ToGame(systems, stateChange.state); break;
                    case GameState.Settings: ToSettings(systems); break;
                    case GameState.About: ToAbout(systems); break;
                    case GameState.GameOver: ToGameOver(systems); break;
                    default: Debug.LogError($"Unexpected game state {stateChange.state}"); break;
                }

                if (_delEntity)
                    _stateChanges.Pools.Inc1.Del(entity);
            }
        }

        /// <summary>
        /// Acquires the menu if it's not set already.
        /// </summary>
        private void GetMenu()
        {
            if (!_menu)
            {
                foreach (var screenEntity in _screens.Value)
                {
                    UIScreenBase screenBase = _screens.Pools.Inc1.Get(screenEntity).screen;
                    if (screenBase is MenuScreen menuScreen)
                        _menu = menuScreen;

                }
            }
        }

        /// <summary>
        /// Restores the default settings from the Settings screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void RestoreDefaults(IEcsSystems systems)
        {
            if (State != GameState.Settings)
                return;

            _restoreSettingsPool.NewEntityImplicit();
        }

        /// <summary>
        /// Goes back from the current state if possible.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void GoBack(IEcsSystems systems)
        {
            switch (State)
            {
                case GameState.Game: ToMenu(systems); break;
                case GameState.GameOver: ToMenu(systems); break;
                case GameState.Settings: ToMenu(systems); break;
                case GameState.About: ToMenu(systems); break;
                case GameState.Menu: if (IsGameStarted) ToGame(systems, GameState.ResumeGame); break;
            }
        }

        /// <summary>
        /// Changes the state to the 'Menu' state if possible.
        /// Shows 'Menu' screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToMenu(IEcsSystems systems)
        {
            if (State == GameState.Menu)
                return;

            if (State == GameState.Game)
            {
                MakeClick();

                _menu.SetIfCalledFromGame();
                ShowMenu();
            }
            else if (State == GameState.GameOver)
            {
                MakeClick();

                _menu.SetIfCalledFromGameOver();
                ShowMenu();
            }
            else if (State == GameState.Settings)
            {
                MakeClick();

                Show<MenuScreen>();
                Hide<SettingsScreen>();

                _saveSettingsPool.NewEntityImplicit();
            }
            else if (State == GameState.About)
            {
                MakeClick();

                Show<MenuScreen>();
                Hide<AboutScreen>();
            }
            else if (State == GameState.None)
            {
                Hide<GameHUDScreen>();
                Hide<GameOverScreen>();
                Hide<AboutScreen>();
                Hide<SettingsScreen>();

                ShowMenu();
            }

            State = GameState.Menu;
        }

        /// <summary>
        /// Changes the state to the 'Game' state if possible.
        /// Shows 'Game' screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToGame(IEcsSystems systems, GameState auxState)
        {
            if (State == GameState.Game || State == GameState.About || State == GameState.Settings)
                return;

            if (auxState == GameState.ResumeGame)
            {
                MakeClick();

                Show<GameHUDScreen>();
                HideMenu();
            }
            else if (auxState == GameState.StartGame)
            {
                MakeClick();

                StartGame();
            }
            else if (auxState == GameState.RestartGame)
            {
                MakeClick();

                StartGame();

                Hide<GameOverScreen>();
            }

            IsGameStarted = true;
            State = GameState.Game;
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        private void StartGame()
        {
            _ecsStartup.Value.Clear();
            _sceneData.Value.Clear();
            _delEntity = false;

            _pauseService.Value.Resume();

            Show<GameHUDScreen>();
            HideMenu();

            _gameMusicPool.NewEntityImplicit().turnOn = true;
        }


        /// <summary>
        /// Changes the state to the 'Game Over' state if possible.
        /// Shows 'Game Over' screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToGameOver(IEcsSystems systems)
        {
            if (State != GameState.Game)
                return;

            _pauseService.Value.Pause();

            _soundsPool.NewEntityImplicit().kind = OneShotSoundKind.Death;
            _gameOverMusicPool.NewEntityImplicit().turnOn = true;

            Hide<GameHUDScreen>();
            Show<GameOverScreen>();

            IsGameStarted = false;
            State = GameState.GameOver;
        }

        /// <summary>
        /// Changes the state to the 'Settings' state if possible.
        /// Shows 'Settings' screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToSettings(IEcsSystems systems)
        {
            if (State != GameState.Menu)
                return;

            MakeClick();

            _loadSettingsPool.NewEntityImplicit();

            Show<SettingsScreen>();
            Hide<MenuScreen>();

            State = GameState.Settings;
        }

        /// <summary>
        /// Changes the state to the 'About' state if possible.
        /// Shows 'About' screen.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToAbout(IEcsSystems systems)
        {
            if (State != GameState.Menu)
                return;

            MakeClick();

            Show<AboutScreen>();
            Hide<MenuScreen>();

            State = GameState.About;
        }

        /// <summary>
        /// Changes the state to the 'Exit' state.
        /// Closes the game.
        /// </summary>
        /// <param name="systems">systems.</param>
        private void ToExit(IEcsSystems systems)
        {
            MakeClick();

            Application.Quit();

            State = GameState.Exit;
        }

        /// <summary>
        /// Performs a button click.
        /// </summary>
        private void MakeClick()
        {
            _soundsPool.NewEntityImplicit().kind = OneShotSoundKind.Click;
        }

        /// <summary>
        /// Shows menu and pauses the game.
        /// </summary>
        private void ShowMenu()
        {
            _pauseService.Value.Pause();
            _menuMusicPool.NewEntityImplicit().turnOn = true;

            Show<MenuScreen>();
            Hide<GameHUDScreen>();
            Hide<GameOverScreen>();
        }

        /// <summary>
        /// Hides menu and resumes the game.
        /// </summary>
        private void HideMenu()
        {
            _pauseService.Value.Resume();
            _menuMusicPool.NewEntityImplicit().turnOn = false;
            Hide<MenuScreen>();
        }

        /// <summary>
        /// Just requests to show the specified screen.
        /// </summary>
        /// <typeparam name="T">type of screen.</typeparam>
        private void Show<T>() where T : UIScreenBase
        {
            _eventWorld.ShowScreen<T>();
        }

        /// <summary>
        /// Just requests to hide the specified screen.
        /// </summary>
        /// <typeparam name="T">type of screen.</typeparam>
        private void Hide<T>() where T : UIScreenBase
        {
            _eventWorld.HideScreen<T>();
        }
    }
}