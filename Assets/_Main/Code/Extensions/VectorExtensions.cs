using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Extensions
{
    /// <summary>
    /// The <see cref="VectorExtensions"/> class.
    /// This class contains different helper methods for the ease of work with <see cref="Vector2"/> and <see cref="Vector3"/> data.
    /// Also other related extensions are present here.
    /// </summary>
    internal static class VectorExtensions
    {
        #region Distance

        /// <summary>
        /// Computes the difference between vectors A and B by X coordinate.
        /// </summary>
        /// <param name="a">vector A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by X</returns>
        public static float DistanceByX(this Vector2 a, Vector2 b) => Mathf.Abs(a.x - b.x);

        /// <summary>
        /// Computes the difference between vectors A and B by X coordinate.
        /// </summary>
        /// <param name="a">vector A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by X</returns>
        public static float DistanceByX(this Vector3 a, Vector3 b) => Mathf.Abs(a.x - b.x);

        /// <summary>
        /// Computes the difference between vectors A and B by Y coordinate.
        /// </summary>
        /// <param name="a">vector A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Y</returns>
        public static float DistanceByY(this Vector2 a, Vector2 b) => Mathf.Abs(a.y - b.y);

        /// <summary>
        /// Computes the difference between vectors A and B by Y coordinate.
        /// </summary>
        /// <param name="a">vector A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Y</returns>
        public static float DistanceByY(this Vector3 a, Vector3 b) => Mathf.Abs(a.y - b.y);

        /// <summary>
        /// Computes the difference between vectors A and B by Z coordinate.
        /// </summary>
        /// <param name="a">vector A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Z</returns>
        public static float DistanceByZ(this Vector3 a, Vector3 b) => Mathf.Abs(a.z - b.z);

        /// <summary>
        /// Computes the difference between transform A and vector B by X coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by X</returns>
        public static float DistanceByX(this Transform a, Vector2 b) => a.position.DistanceByX(b);

        /// <summary>
        /// Computes the difference between transform A and vector B by X coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by X</returns>
        public static float DistanceByX(this Transform a, Vector3 b) => a.position.DistanceByX(b);

        /// <summary>
        /// Computes the difference between transform A and transform B by X coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">transform B</param>
        /// <returns>difference / distance by X</returns>
        public static float DistanceByX(this Transform a, Transform b) => a.position.DistanceByX(b.position);

        /// <summary>
        /// Computes the difference between transform A and vector B by Y coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Y</returns>
        public static float DistanceByY(this Transform a, Vector2 b) => a.position.DistanceByY(b);

        /// <summary>
        /// Computes the difference between transform A and vector B by Y coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Y</returns>
        public static float DistanceByY(this Transform a, Vector3 b) => a.position.DistanceByY(b);

        /// <summary>
        /// Computes the difference between transform A and transform B by Y coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">transform B</param>
        /// <returns>difference / distance by Y</returns>
        public static float DistanceByY(this Transform a, Transform b) => a.position.DistanceByY(b.position);

        /// <summary>
        /// Computes the difference between transform A and transform B by Z coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">vector B</param>
        /// <returns>difference / distance by Z</returns>
        public static float DistanceByZ(this Transform a, Vector3 b) => a.position.DistanceByZ(b);

        /// <summary>
        /// Computes the difference between transform A and transform B by Z coordinate.
        /// </summary>
        /// <param name="a">transform A</param>
        /// <param name="b">transform B</param>
        /// <returns>difference / distance by Z</returns>
        public static float DistanceByZ(this Transform a, Transform b) => a.position.DistanceByZ(b.position);

        #endregion
    }
}
