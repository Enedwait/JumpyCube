using UnityEngine;

namespace OKRT.JumpyCube.Main.Code.Views
{
    /// <summary>
    /// The <see cref="ObstacleView"/> class.
    /// This class represents the obstacle.
    /// </summary>
    [DisallowMultipleComponent]
    internal sealed class ObstacleView : MonoBehaviour
    {
        [SerializeField] private Transform _upperPart;
        [SerializeField] private Transform _lowerPart;
        [SerializeField] private float _gapLength = 5f;
        [SerializeField] private float _width = 1f;
        
        /// <summary> Gets the width of the obstacle. </summary>
        public float Width => _width;

        /// <summary> Gets the gap length of the obstacle. </summary>
        public float GapLength => _gapLength;

#if (UNITY_EDITOR)
        private void OnValidate()
        {
            SetGapLength(_gapLength);
        }
#endif

        /// <summary>
        /// Sets the gap length.
        /// </summary>
        /// <param name="length">length.</param>
        public void SetGapLength(float length)
        {
            if (length < 0)
                length = 0;

            _gapLength = length;
            float h = _gapLength / 2f;
            _upperPart.localPosition = new Vector3(_upperPart.localPosition.x, h, _upperPart.localPosition.z);
            _lowerPart.localPosition = new Vector3(_lowerPart.localPosition.x, -h, _lowerPart.localPosition.z);
        }
    }
}
