using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class Nombres : MonoBehaviour
{
    public string N1;
    public string N2;
    public string N3;
    public string N4;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true, 60);
    }

    public void Jugar()
    {
        SceneManager.LoadScene(1);
    }
}
