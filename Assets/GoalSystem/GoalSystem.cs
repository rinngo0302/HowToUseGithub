using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalSystem : MonoBehaviour
{
    [SerializeField] private HungerSystem _hungerSystem;

    // Start is called before the first frame update
    void Start()
    {
        if (_hungerSystem == null)
        {
            Debug.LogError("HungerSystem is not attached");
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFinish())
        {
            SceneManager.LoadScene("end");
        }
    }

    private bool IsFinish()
    {
        int hunger = _hungerSystem.Hunger;

        Debug.Log(hunger);

        return hunger <= 0;
    }
}
