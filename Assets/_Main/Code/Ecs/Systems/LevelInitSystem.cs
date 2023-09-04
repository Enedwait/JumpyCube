using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="LevelInitSystem"/> class.
    /// This system initializes level data and objects.
    /// </summary>
    internal sealed class LevelInitSystem : IEcsInitSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<ScreenSizeService> _screenSizeService;

        private SceneData SceneData => _sceneData.Value;

        public void Init (IEcsSystems systems)
        {
            float H = _screenSizeService.Value.Height + 1;
            float W = _screenSizeService.Value.Width + 1;

            // modify and reposition the ceiling
            SceneData.Ceiling.localScale = new Vector3(W, 1, 1);
            SceneData.Ceiling.position = new Vector3(0, H / 2f, 0);

            // modify and reposition the ground
            SceneData.Ground.localScale = SceneData.Ceiling.localScale;
            SceneData.Ground.position = new Vector3(0, -H / 2f, 0);

            // modify and reposition the obstacle killer
            SceneData.ObstacleKiller.localScale = new Vector3(1, H, 1);
            SceneData.ObstacleKiller.position = new Vector3(-W, 0, 0);

            // modify and reposition the obstacle spawn point
            SceneData.ObstacleSpawnPoint.position = new Vector3(W, 0, 0);

            // modify and reposition the player spawn point
            SceneData.PlayerSpawnPoint.position = new Vector3(-W / 2 + 2, 0, 0);
        }
    }
}