using Leopotam.EcsLite;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using UnityEngine;
using Zenject;

namespace OKRT.JumpyCube.Main.Code.Ecs.Providers
{
    /// <summary>
    /// The <see cref="EntityProvider"/> class.
    /// This class allows to create an entity in the specified world and then to attach the required components to this entity.
    /// </summary>
    internal class EntityProvider : MonoBehaviour
    {
        [SerializeField] private EcsWorlds _world;
        [SerializeField] private string _entityId;
        [SerializeField, Tooltip("Determines if the entity provider should use predefined set of mono providers or should scan for them during initialization stage.")] 
        private bool _usePredefinedMonoProviders = false;
        [SerializeField] private MonoProviderBase[] _monoProviders;
        [SerializeField] private bool _initOnAwake = false;
        [SerializeField] private bool _initOnStart = true;

        private WorldService _worldService;

        /// <summary> Gets the entity initialization flag. </summary>
        public bool IsInitialized { get; protected set; }

        /// <summary> Gets the packed entity. </summary>
        public EcsPackedEntityWithWorld Entity { get; protected set; }

        protected virtual void Awake()
        {
            if (_initOnAwake)
                InitializeEntity();
        }

        protected virtual void Start()
        {
            if (_initOnStart)
                InitializeEntity();
        }

        [Inject]
        private void Construct(WorldService worldService)
        {
            _worldService = worldService;
        }

        /// <summary>
        /// Creates an entity of this object.
        /// </summary>
        internal virtual void InitializeEntity()
        {
            if (IsInitialized)
                return;

            EcsWorld world = _worldService[_world.Name()];

            int entity = world.NewEntity();
            Entity = world.PackEntityWithWorld(entity);

            if (!_usePredefinedMonoProviders)
                _monoProviders = GetComponents<MonoProviderBase>();

            foreach (var monoProvider in _monoProviders)
                monoProvider.Convert(entity, world);

            IsInitialized = true;
        }
    }
}
