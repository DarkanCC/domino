using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class FichaJ : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    public Ficha nodo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nodo != null)
        {
            CambiarLado1(nodo.retornaLado1());
            CambiarLado2(nodo.retornaLado2());
        }
    }

    public void setNodo(Ficha nodo)
    {
        this.nodo = nodo;
    }

    public void AutoDestruir()
    {
        GameObject.Find("Jugador").GetComponent<Jugador>().nodo.getJugadorLigaD().BorrarFicha(nodo.retornaLado1(), nodo.retornaLado2());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        transform.position = gameObject.GetComponent<Gestures2DCollider>().posIni;
        gameObject.GetComponent<Gestures2DCollider>().enabled = false;
        GameObject.Find("Jugador").GetComponent<Jugador>().TerminarTurno();
    }

    public void AutoReconstruir()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
        gameObject.GetComponent<Gestures2DCollider>().enabled = true;
    }

    public void CambiarLado1(int num)
    {
        switch (num)
        {
            case 0:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                break;
            case 1:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 4:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
            case 5:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite5;
                break;
            case 6:
                gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite6;
                break;
        }
    }

    public void CambiarLado2(int num)
    {
        switch (num)
        {
            case 0:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
                break;
            case 1:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite1;
                break;
            case 2:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite2;
                break;
            case 3:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite3;
                break;
            case 4:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite4;
                break;
            case 5:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite5;
                break;
            case 6:
                gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sprite6;
                break;
        }
    }
}
