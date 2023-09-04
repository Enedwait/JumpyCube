using Leopotam.EcsLite.Unity.Ugui;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace OKRT.JumpyCube.Main.Code.UI
{
    /// <summary>
    /// The <see cref="SliderWithText"/> class.
    /// This class represents the custom slider.
    /// </summary>
    internal sealed class SliderWithText : MonoBehaviour
    {
        [SerializeField] private string _widgetName;
        [SerializeField] private EcsUguiSliderAction _sliderAction;
        [SerializeField] private Slider _slider;
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _value;
        [SerializeField] private TextMeshProUGUI _textName;
        [SerializeField] private string _name;
        [SerializeField] private TextMeshProUGUI _textValue;
        [SerializeField] private string format = @"{0}";

        /// <summary> Gets the slider value. </summary>
        public float Value => _slider.value;

#if UNITY_EDITOR
        private void OnValidate()
        {
            ChangeValue(_value);
            _textName.text = _name;
        }
#endif

        private void Awake()
        {
            _slider.onValueChanged.AddListener(_ChangeValue);
            _textName.text = _name;
            _sliderAction.SetWidgetName(_widgetName);
        }

        /// <summary>
        /// Changes the internal value and text.
        /// </summary>
        /// <param name="value">value.</param>
        private void _ChangeValue(float value)
        {
            _value = value;
            _textValue.text = string.Format(format, value);
        }

        /// <summary>
        /// Changes the value of the slider entirely.
        /// </summary>
        /// <param name="value">value.</param>
        public void ChangeValue(float value)
        {
            value = Mathf.Clamp(value, _minValue, _maxValue);
            _slider.value = value;
            _value = value;
            _textValue.text = string.Format(format, value);
        }
    }
}
