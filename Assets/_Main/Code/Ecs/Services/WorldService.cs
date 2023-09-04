using System;
using System.Collections.Generic;
using Leopotam.EcsLite;

namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="WorldService"/> class.
    /// This service allows to store and retrieve information about used worlds.
    /// </summary>
    internal sealed class WorldService
    {
        private EcsWorld _defaultWorld;
        private Dictionary<string, EcsWorld> _worlds = new Dictionary<string, EcsWorld>();

        /// <summary> Gets the default world assigned. </summary>
        public EcsWorld Default => _defaultWorld;
        
        /// <summary> Gets the world by the name specified.
        /// </summary>
        /// <param name="name">world name.</param>
        /// <returns>world.</returns>
        public EcsWorld this[string name] => _worlds[name];

        /// <summary>
        /// Adds world and marks it as the default one.
        /// </summary>
        /// <param name="name">world name.</param>
        /// <param name="world">world.</param>
        public void AddDefaultWorld(string name, EcsWorld world)
        {
            AddWorld(name, world);
            _defaultWorld = world;
        }

        /// <summary>
        /// Adds default world.
        /// </summary>
        /// <param name="name">world name.</param>
        /// <param name="world">world.</param>
        public void AddWorld(string name, EcsWorld world)
        {
            if (_worlds.ContainsKey(name))
                throw new Exception($"WorldService already contains the world with '{name}' name!");

            _worlds.Add(name, world);
        }
    }
}
