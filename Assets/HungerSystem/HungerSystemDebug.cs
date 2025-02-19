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
        _hungerSystem.Hunger = 30;
    }

    // Update is called once per frame
    void Update()
    {
        if (_hungerSystem == null)
        {
            Debug.LogError("Hunger Obj is not attached");
        }
    }
}
