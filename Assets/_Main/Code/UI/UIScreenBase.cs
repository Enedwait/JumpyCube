using Leopotam.EcsLite;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.UI
{
    /// <summary>
    /// The <see cref="IUIScreen"/> interface. 
    /// </summary>
    public interface IUIScreen
    {
        /// <summary>
        /// Shows the screen.
        /// </summary>
        /// <param name="world">world.</param>
        public void Show(EcsWorld world);

        /// <summary>
        /// Hides the screen.
        /// </summary>
        /// <param name="world">world.</param>
        public void Hide(EcsWorld world);
    }

    /// <summary>
    /// The <see cref="UIScreenBase"/> class.
    /// This class is used as a base class for all the UI screens.
    /// </summary>
    internal abstract class UIScreenBase : MonoBehaviour, IUIScreen
    {
        /// <summary>
        /// Shows the screen.
        /// </summary>
        /// <param name="world">world.</param>
        public virtual void Show(EcsWorld world)
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the screen.
        /// </summary>
        /// <param name="world">world.</param>
        public virtual void Hide(EcsWorld world)
        {
            gameObject.SetActive(false);
        }
    }
}
