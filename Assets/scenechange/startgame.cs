using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startgame : MonoBehaviour
{

    public void start_game()
    {
        SceneManager.LoadScene("Yanagimoto");

    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Yanagimoto");
        }
    }
}

