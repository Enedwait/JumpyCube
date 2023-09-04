using System.Collections.Generic;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Unity.Ugui;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Extensions;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using OKRT.JumpyCube.Main.Code.Ecs.Systems;
using OKRT.JumpyCube.Main.Code.Ecs.Systems.UI;
using UnityEngine;
using Zenject;

namespace OKRT.JumpyCube.Main.Code 
{
    /// <summary>
    /// The <see cref="EcsStartup"/> class.
    /// The main ECS initialization class.
    /// </summary>
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private EcsUguiEmitter _uguiEmitter;
        [SerializeField] private SceneData _sceneData;
        [SerializeField] private StaticData _staticData;
        [SerializeField] private SoundData _soundData;

        private EcsWorld _world;
        private EcsWorld _eventsWorld;
        private EcsWorld _staticWorld;

        private IEcsSystems _systems;
        private IEcsSystems _fixedSystems;

        private WorldService _worldService;
        private SpawnService _spawnService;
        private PauseService _pauseService;
        private TimeService _timeService;
        private PlayerDataService _playerDataService;
        private ScreenSizeService _screenSizeService;

        private List<IEcsSystem> _debugSystems;

        [Inject]
        private void Construct(WorldService worldService, SpawnService spawnService)
        {
            _worldService = worldService;
            _spawnService = spawnService;
        }

        private void Awake()
        {
            // initialize worlds
            _world = new EcsWorld();
            _eventsWorld = new EcsWorld();
            _staticWorld = new EcsWorld();

            // set the world of physics events
            EcsPhysicsEvents.ecsWorld = _eventsWorld;

            _worldService.AddDefaultWorld("", _world);
            _worldService.AddWorld(GlobalIdents.Worlds.EventWorld, _eventsWorld);
            _worldService.AddWorld(GlobalIdents.Worlds.StaticWorld, _staticWorld);
        }

        void Start()
        {
#if (UNITY_EDITOR)
            _debugSystems = new List<IEcsSystem>();
            _debugSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
            _debugSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(GlobalIdents.Worlds.EventWorld));
            _debugSystems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(GlobalIdents.Worlds.StaticWorld));
#endif

            // initialize services
            _playerDataService = new PlayerDataService();
            _screenSizeService = new ScreenSizeService();
            _timeService = new TimeService();
            _pauseService = new PauseService(true);

            // =========================================================================================================================
            // initialize Update systems
            _systems = new EcsSystems(_world);
            _systems
                .AddWorld(_eventsWorld, GlobalIdents.Worlds.EventWorld)
                .AddWorld(_staticWorld, GlobalIdents.Worlds.StaticWorld)
                .Add(new TimeSystem())
                .Add(new ScreenSizeInitSystem())
                .Add(new ScreenInitSystem())
                .Add(new LevelInitSystem())
                .Add(new GameSettingsSystem())
                .Add(new GameStateManagementSystem())
                .Add(new GameComplexitySystem())

                // Player [update] systems
                .Add(new PlayerInputSystem())
                .Add(new UIInputSystem())
                .Add(new PlayerActionSystem())
                .Add(new PlayerSpawnSystem())
                .Add(new PlayerInitializationSystem())
                .Add(new PlayerScoreSystem())
                .Add(new PlayerDiedSystem())

                // Obstacle systems
                .Add(new ObstacleSpawnSystem())
                .Add(new ObstacleInitializationSystem())

                // Spawn system
                .Add(new SpawnSystem())

                // UI systems
                .Add(new HiScoreSystem())
                .Add(new HiScoreChangedSystem())
                .Add(new UpdateScoreSystem())
                .Add(new SkyMoveSystem())
                .Add(new UpdateElapsedTimeSystem())
                .Add(new ImageHueChangeSystem())
                .Add(new VersionInitSystem())
                .Add(new GameSoundSystem())
                .Add(new UISoundSystem())
                .Add(new SoundManagementSystem())
                .Add(new ShowScreenSystem())
                .Add(new HideScreenSystem())
#if UNITY_EDITOR
                .AddGroup("DebugSystems", true, GlobalIdents.Worlds.EventWorld, _debugSystems.ToArray())
#endif
                .Inject(GetInjects())
                .InjectUgui(_uguiEmitter, GlobalIdents.Worlds.EventWorld)
                .Init();

            // =========================================================================================================================
            // initialize Physics systems
            _fixedSystems = new EcsSystems(_world);
            _fixedSystems
                .AddWorld(_eventsWorld, GlobalIdents.Worlds.EventWorld)
                .AddWorld(_staticWorld, GlobalIdents.Worlds.StaticWorld)

                // Player [fixed update] systems
                .Add(new PlayerJumpSystem())
                .Add(new PlayerCollisionSystem())

                // Obstacle [fixed update] systems
                .Add(new ObstacleMoveSystem())
                .Add(new ObstacleTriggerSystem())
#if UNITY_EDITOR
                .AddGroup("DebugSystems", true, GlobalIdents.Worlds.EventWorld, _debugSystems.ToArray())
#endif
                .Inject(GetInjects())
                .Init();
        }

        private object[] GetInjects() => new object[] { this, _playerDataService, _timeService, _pauseService, _screenSizeService, _sceneData, _staticData, _soundData, _spawnService };

        void Update()
        {
            _systems?.Run();
        }

        void FixedUpdate()
        {
            _fixedSystems?.Run();
        }

        /// <summary>
        /// Clears all the worlds.
        /// </summary>
        internal void Clear()
        {
            _world.RemoveEntities();
            _eventsWorld.RemoveEntities();
            _timeService.Reset();
        }

        void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;

            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_fixedSystems != null)
            {
                _fixedSystems.Destroy();
                _fixedSystems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}