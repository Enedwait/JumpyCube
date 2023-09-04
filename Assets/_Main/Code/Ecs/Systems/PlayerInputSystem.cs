using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using OKRT.JumpyCube.Main.Code.Ecs.Events;
using OKRT.JumpyCube.Main.Code.Extensions;
using OKRT.JympyCube.Main.Code.Controls;

namespace OKRT.JumpyCube.Main.Code.Ecs.Systems
{
    /// <summary>
    /// The <see cref="PlayerInputSystem"/> class.
    /// This system processes the player input.
    /// </summary>
    internal sealed class PlayerInputSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private EcsPoolInject<OnPlayerPressedJumpEvent> _jumpsPool = GlobalIdents.Worlds.EventWorld;
        private EcsPoolInject<OnPlayerPressedBackEvent> _backsPool = GlobalIdents.Worlds.EventWorld;

        private InputActions _inputActions;

        public void Init(IEcsSystems systems)
        {
            _inputActions = new InputActions();
            _inputActions.Enable();

            _inputActions.Default.Jump.started += JumpStarted;
            _inputActions.Default.Back.started += BackStarted;
        }

        private void JumpStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _jumpsPool.NewEntityImplicit();
        }

        private void BackStarted(UnityEngine.InputSystem.InputAction.CallbackContext obj)
        {
            _backsPool.NewEntityImplicit();
        }

        public void Destroy(IEcsSystems systems)
        {
            if (_inputActions != null)
            {
                _inputActions.Default.Jump.started -= JumpStarted;
                _inputActions.Default.Back.started -= BackStarted;

                _inputActions.Disable();
                _inputActions.Dispose();
                _inputActions = null;
            }
        }
    }
}