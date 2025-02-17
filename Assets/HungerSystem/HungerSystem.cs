using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    // Hunger
    [SerializeField] private int _maxHunger;
    private int _hunger;

    // Slider
    [SerializeField] private GameObject _sliderObj;
    private Slider _slider;

    // Start is called before the first frame update
    void Start()
    {
        if (_sliderObj != null)
        {
            _slider = _sliderObj.GetComponent<Slider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider == null)
        {
            Debug.LogError("Slider obj is not attached");
            return;
        }

        // update slider UI
        _slider.value = _hunger / _maxHunger;
    }

    // Plus Hunger value
    void PlusHunger(int hunger)
    {
        if (hunger <= 0)
        {
            Debug.LogError("value is must be greater than 0");
            return;
        }

        _hunger += hunger;
    }

    // Minus Hunger value
    void MinusHunger(int hunger)
    {
        if (hunger <= 0)
        {
            Debug.LogError("value is must be greater than 0");
            return;
        }

        _hunger -= hunger;
    }

    // Hunger(Read/Write)
    public int Hunger
    {
        get
        {
            return _hunger;
        }

        set
        {
            if (value < 0 || value > _maxHunger)
            {
                Debug.LogError($"value is out of range (0 ~ _maxHunger({_maxHunger}))");
                return;
            }

            _hunger = value;
        }
    }
}
