using Leopotam.EcsLite;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.UI;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems.UI
{
    /// <summary>
    /// The <see cref="ScreenInitSystem"/> class.
    /// This system pre-sets up the screens on the 1st initialization.
    /// </summary>
    internal sealed class ScreenInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            EcsWorld eventWorld = systems.GetWorld(GlobalIdents.Worlds.EventWorld);

            // show initial screen
            eventWorld.ShowScreen<MenuScreen>();

            // hide other screens
            eventWorld.HideScreen<GameHUDScreen>();
            eventWorld.HideScreen<GameOverScreen>();
            eventWorld.HideScreen<AboutScreen>();
            eventWorld.HideScreen<SettingsScreen>();
        }
    }
}