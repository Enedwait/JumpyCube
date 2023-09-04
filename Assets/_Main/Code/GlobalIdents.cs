using UnityEngine;

namespace OKRT.JumpyCube.Main.Code
{
    /// <summary>
    /// The <see cref="GlobalIdents"/> class.
    /// This class contains different global data for the ease of use in game.
    /// </summary>
    internal static class GlobalIdents
    {
        /// <summary>
        /// The <see cref="Worlds"/> class
        /// This class contains predefined world names.
        /// </summary>
        public static class Worlds
        {
            public const string EventWorld = "Events";
            public const string StaticWorld = "Static";
        }

        /// <summary>
        /// The <see cref="Tags"/> class
        /// This class contains predefined tag names.
        /// </summary>
        public static class Tags
        {
            public const string Ceiling = "Ceiling";
            public const string Ground = "Ground";
        }

        /// <summary>
        /// The <see cref="UI"/> class
        /// This class contains predefined names of the UI controls / widgets or actions.
        /// </summary>
        public static class UI
        {
            public const string Jump = "Jump";
            public const string Restart = "Restart";
            public const string Resume = "Resume";
            public const string Settings = "Settings";
            public const string About = "About";
            public const string Close = "Close";
            public const string Start = "Start";
            public const string Exit = "Exit";
            public const string Menu = "Menu";
            public const string Default = "Default";
            public const string GameSoundsSlider = "GameSoundsSlider";
            public const string InterfaceSoundsSlider = "InterfaceSoundsSlider";
            public const string MusicSlider = "MusicSlider";
        }

        /// <summary>
        /// The <see cref="PlayerPrefsKeys"/> class
        /// This class contains predefined names of the <see cref="PlayerPrefs"/> keys.
        /// </summary>
        public static class PlayerPrefsKeys
        {
            public const string PlayerData = "PlayerData";
        }
    }

    /// <summary>
    /// The <see cref="EcsWorlds"/> enumeration.
    /// </summary>
    public enum EcsWorlds { Default, Events, Static }

    /// <summary>
    /// The <see cref="EcsWorldsExtensions"/> enumeration.
    /// This class contains different extension methods for the ease of work with <see cref="EcsWorlds"/> enumeration.
    /// </summary>
    public static class EcsWorldsExtensions
    {
        /// <summary>
        /// Retrieves the name of the specified world. 
        /// </summary>
        /// <param name="world">world.</param>
        /// <returns>name of the world.</returns>
        public static string Name(this EcsWorlds world)
        {
            switch (world)
            {
                case EcsWorlds.Events: return GlobalIdents.Worlds.EventWorld;
                case EcsWorlds.Static: return GlobalIdents.Worlds.StaticWorld;
                case EcsWorlds.Default:
                default: return "";
            }
        }
    }
}
