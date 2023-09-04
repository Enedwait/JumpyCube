using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Extensions
{
    /// <summary>
    /// The <see cref="VectorExtensions"/> class.
    /// This class contains different helper methods for the ease of work with <see cref="Transform"/> objects.
    /// </summary>
    internal static class TransformExtensions
    {
        /// <summary>
        /// Removes all the children of the specified target transform.
        /// </summary>
        /// <param name="target">target transform.</param>
        public static void RemoveChildren(this Transform target)
        {
            for(int i = target.childCount - 1; i >= 0; i--)
                Object.Destroy(target.GetChild(i).gameObject);
        }
    }
}
