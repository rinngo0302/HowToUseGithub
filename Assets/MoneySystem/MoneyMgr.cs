using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyMgr : MonoBehaviour
{
    private int _money;
    [SerializeField] private int _defaultMoney;
    [SerializeField] private MoneyView _moneyView;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Plus current money
    /// </summary>
    /// <param name="money"> money(greater than 0) </param>
    public void PlusMoney(int money)
    {
        if (money < 0)
        {
            Debug.LogError("value is greater than 0");
            return;
        }

        _money += money;
        UpdateMoneyView(_money);
    }

    /// <summary>
    /// Minus current money
    /// </summary>
    /// <param name="money"> money(greater than 0) </param>
    public void MinusMoney(int money)
    {
        if (money < 0)
        {
            Debug.LogError("value is greater than 0");
            return;
        }

        _money -= money;

        UpdateMoneyView(_money);
    }

    /// <summary>
    /// Current Money
    /// </summary>
    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            if (_money < 0)
            {
                Debug.LogError("Money is greater than 0");
                return;
            }

            _money = value;
            UpdateMoneyView(_money);
        }
    }

    private void UpdateMoneyView(int money)
    {
        _moneyView.SetMoneyView(money);
    }
}
