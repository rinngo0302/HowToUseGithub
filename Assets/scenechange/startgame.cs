using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class startgame : MonoBehaviour
{
    [SerializeField] private InputActionProperty interactAction; // Use reference

    public void start_game()
    {
        SceneManager.LoadScene("Yanagimoto");

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return) || interactAction.action.WasPressedThisFrame())
        {
            SceneManager.LoadScene("Yanagimoto");
        }
    }
}

