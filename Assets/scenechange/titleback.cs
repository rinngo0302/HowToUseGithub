using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backtitle : MonoBehaviour
{

    public void back_title()
    {
        SceneManager.LoadScene("title");
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("Yanagimoto");
        }
    }
}

