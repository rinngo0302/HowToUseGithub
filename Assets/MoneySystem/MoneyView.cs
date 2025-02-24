using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentMoneyView;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// Set Money View
    /// </summary>
    /// <param name="money"> money </param>
    public void SetMoneyView(int money)
    {
        _currentMoneyView.text = money.ToString();
    }
}
