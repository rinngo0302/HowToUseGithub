using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystemDebug : MonoBehaviour
{
    [SerializeField] private MoneyMgr _moneyMgr;

    // Start is called before the first frame update
    void Start()
    {
        _moneyMgr.Money = 15;
        _moneyMgr.PlusMoney(100);
        _moneyMgr.MinusMoney(50);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
