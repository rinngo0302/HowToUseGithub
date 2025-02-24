using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HungerSystem : MonoBehaviour
{
    // Hunger
    [SerializeField] private int _maxHunger;
    private int _hunger;

    [SerializeField] private GaugeView _hungerGauge;

    // Start is called before the first frame update
    void Start()
    {
        if (_hungerGauge == null)
        {
            Debug.LogError("hunger gauge is not attached");
            return;
        }

        _hungerGauge.MaxNumber = _maxHunger;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hungerGauge == null)
        {
            Debug.LogError("hunger gauge is not attached");
            return;
        }

        // update Gauge number
        _hungerGauge.SetGaugeNumber((float)_hunger);

        //Debug.Log($"_hunger: {(float)_hunger}, _maxHunger: {_maxHunger}");
    }

    /// <summary>
    /// Plus Hunger Value
    /// </summary>
    /// <param name="hunger"> plus </param>
    public void PlusHunger(int hunger)
    {
        if (hunger <= 0)
        {
            Debug.LogError("value is must be greater than 0");
            return;
        }

        _hunger += hunger;

        CheckIsOverMax();
    }

    /// <summary>
    /// Minus Hunger Value
    /// </summary>
    /// <param name="hunger"> minus </param>
    public void MinusHunger(int hunger)
    {
        if (hunger <= 0)
        {
            Debug.LogError("value is must be greater than 0");
            return;
        }

        _hunger -= hunger;
        CheckIsOverMax();
    }

    /// <summary>
    /// If Hunger is over max, set to max
    /// </summary>
    private void CheckIsOverMax()
    {
        _hunger = (_hunger > _maxHunger) ? _maxHunger : _hunger;
    }

    /// <summary>
    /// Hunger(Read/Write)
    /// </summary>
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
            CheckIsOverMax();
        }
    }
}
