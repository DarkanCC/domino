using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class ControladorMenu : MonoBehaviour
{
    public InputField nombre1, nombre2, nombre3, nombre4;
    public GameObject menuP, menuN;
    // Start is called before the first frame update
    void Start()
    {
        menuN.SetActive(false);
        menuP.SetActive(true);
    }

    public void Jugar()
    {
        menuP.SetActive(false);
        menuN.SetActive(true);
        nombre2.text = "Hugo";
        nombre3.text = "Paco";
        nombre4.text = "Luis";
    }

    public void SalirNombres()
    {
        menuP.SetActive(true);
        menuN.SetActive(false);
    }

    public void JugarNombres()
    {
        if (nombre1.text.Length < 10 && nombre2.text.Length < 10 && nombre3.text.Length < 10 && nombre4.text.Length < 10 && nombre1.text != "" && nombre2.text != "" && nombre3.text != "" && nombre4.text != "")
        {
            GameObject.Find("Nombres").GetComponent<Nombres>().N1 = nombre1.text;
            GameObject.Find("Nombres").GetComponent<Nombres>().N2 = nombre2.text;
            GameObject.Find("Nombres").GetComponent<Nombres>().N3 = nombre3.text;
            GameObject.Find("Nombres").GetComponent<Nombres>().N4 = nombre4.text;
            GameObject.Find("Nombres").GetComponent<Nombres>().Jugar();
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void CerrarJuego()
    {
        Application.Quit();
    }
}
