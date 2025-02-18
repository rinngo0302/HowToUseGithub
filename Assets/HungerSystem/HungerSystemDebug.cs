using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerSystemDebug : MonoBehaviour
{
    [SerializeField] private GameObject _hungerObj;
    private HungerSystem _hungerSystem;

    // Start is called before the first frame update
    void Start()
    {
        _hungerSystem = _hungerObj.GetComponent<HungerSystem>();
        _hungerSystem.Hunger = 50;

        _hungerSystem.PlusHunger(30);
        _hungerSystem.MinusHunger(10);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
