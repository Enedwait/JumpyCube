using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Helpers
{
    /// <summary>
    /// The <see cref="LayerHelper"/> class.
    /// This class contains game specific layer related data.
    /// </summary>
    internal static class LayerHelper
    {
        /// <summary> The name of the "Obstacle" layer used in Editor / game. </summary>
        public const string ObstacleName = "Obstacle";

        /// <summary> The name of the "DestroyArea" layer used in Editor / game. </summary>
        public const string DestroyAreaName = "DestroyArea";

        /// <summary> Gets the numeric value of the "Obstacle" layer. </summary>
        public static int Obstacle => LayerMask.NameToLayer(ObstacleName);

        /// <summary> Gets the numeric value of the "DestroyArea" layer. </summary>
        public static int DestroyArea => LayerMask.NameToLayer(DestroyAreaName);
    }
}
