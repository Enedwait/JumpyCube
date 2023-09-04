using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Data;
using OKRT.JumpyCube.Main.Code.Ecs.Services;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="ScreenSizeInitSystem"/> class.
    /// This system determines the screen size.
    /// </summary>
    internal sealed class ScreenSizeInitSystem : IEcsInitSystem
    {
        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<ScreenSizeService> _screenSizeService;

        private SceneData SceneData => _sceneData.Value;

        public void Init(IEcsSystems systems) 
        {
            float z = -SceneData.MainCamera.transform.position.z;

            Vector3 topLeft = SceneData.MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, z));
            Vector3 bottomRight = SceneData.MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, z));

            _screenSizeService.Value.Init(topLeft, bottomRight);
        }
    }
}