using OKRT.JumpyCube.Main.Code.Extensions;
using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Ecs.Services
{
    /// <summary>
    /// The <see cref="ScreenSizeService"/> class.
    /// This service stores the data about physical screen the game is shown on.
    /// </summary>
    internal sealed class ScreenSizeService
    {
        /// <summary> Gets the top-left coordinates of the screen. </summary>
        public Vector2 TopLeft { get; private set; }

        /// <summary> Gets the bottom-right coordinates of the screen. </summary>
        public Vector2 BottomRight { get; private set; }

        /// <summary> Gets the width of the screen. </summary>
        public float Width { get; private set; }

        /// <summary> Gets the height of the screen. </summary>
        public float Height { get; private set; }

        /// <summary> Gets the min X coordinate of the screen. </summary>
        public float MinX { get; private set; }

        /// <summary> Gets the max X coordinate of the screen. </summary>
        public float MaxX { get; private set; }

        /// <summary> Gets the min Y coordinate of the screen. </summary>
        public float MinY { get; private set; }

        /// <summary> Gets the max Y coordinate of the screen. </summary>
        public float MaxY { get; private set; }

        /// <summary>
        /// Initializes data of the screen from the specified values.
        /// </summary>
        /// <param name="topLeft">top-left coordinates.</param>
        /// <param name="bottomRight">bottom-right coordinates.</param>
        public void Init(Vector2 topLeft, Vector2 bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;

            Width = TopLeft.DistanceByX(BottomRight);
            Height = TopLeft.DistanceByY(BottomRight);
            
            MinX = TopLeft.x;
            MaxX = BottomRight.x;
            MinY = BottomRight.y;
            MaxY = TopLeft.y;
        }
    }
}
