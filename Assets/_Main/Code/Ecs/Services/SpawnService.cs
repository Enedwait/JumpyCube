using OKRT.JumpyCube.Main.Code.Ecs.Components;
using Zenject;

namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="SpawnService"/> class.
    /// This service allows to spawn objects.
    /// </summary>
    internal sealed class SpawnService
    {
        private DiContainer _container;

        [Inject]
        private void Construct(DiContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Instantiates the specified prefab.
        /// </summary>
        /// <param name="spawnPrefab">prefab.</param>
        public void Spawn(SpawnPrefab spawnPrefab)
        {
            _container.InstantiatePrefab(spawnPrefab.prefab, spawnPrefab.position, spawnPrefab.rotation, spawnPrefab.parent);
        }
    }
}

