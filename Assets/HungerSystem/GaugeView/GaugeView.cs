using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GaugeView : MonoBehaviour
{
    [SerializeField] private GameObject _maskObj;
    [SerializeField] private float _maxNumber;
    [SerializeField] private float _defaultNumber;
    [SerializeField] private float _widthMask;

    [SerializeField] private ePaddingDirection _paddingDirection;

    private float _currentNumber;

    private RectMask2D _mask;


    private enum ePaddingDirection
    { 
        Top,
        Bottom,
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        if (_maskObj == null)
        {
            Debug.LogError("MaskObj is null");
            return;
        }

        _mask = _maskObj.GetComponent<RectMask2D>();
        if (_widthMask == 0)
            _widthMask = _mask.GetComponent<RectTransform>().rect.width;

        _currentNumber = _defaultNumber;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateGauge()
    {
        float width = _widthMask * (_currentNumber / _maxNumber);
        width = (width >= _widthMask) ? _widthMask : width;

        Vector4 padding = _mask.padding;

        switch (_paddingDirection)
        {
            case ePaddingDirection.Top:
                padding = new Vector4(padding.x, padding.y, padding.z, width);
                break;

            case ePaddingDirection.Bottom:
                padding = new Vector4(padding.x, width, padding.z, padding.w);
                break;

            case ePaddingDirection.Left:
                padding = new Vector4(width, padding.y, padding.z, padding.w);
                break;

            case ePaddingDirection.Right:
                padding = new Vector4(padding.x, padding.y, width, padding.w);
                break;
        }

        _mask.padding = padding;
    }

    /// <summary>
    /// Set Gauge Value
    /// </summary>
    /// <param name="number"> value </param>
    public void SetGaugeNumber(float number)
    {
        if (number > _maxNumber || number < 0)
        {
            Debug.LogError("CurrentNumber is out of range");
            return;
        }
        _currentNumber = number;
        UpdateGauge();
    }

    /// <summary>
    /// Max gauge value
    /// </summary>
    public float MaxNumber
    {
        get { return _maxNumber; }
        set { 
            if (value <= 0)
            {
                Debug.LogError("Value must be greater than 0");
                return;
            }

            _maxNumber = value;
        }
    }
}
