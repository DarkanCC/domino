using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class FichaT : MonoBehaviour
{
    public Sprite sprite1;
    public Sprite sprite2;
    public Sprite sprite3;
    public Sprite sprite4;
    public Sprite sprite5;
    public Sprite sprite6;

    public NodoDoble nodo;
    Partida partida;

    // Start is called before the first frame update
    void Start()
    {
        partida = GameObject.Find("Partida").GetComponent<Partida>();
    }

    // Update is called once per frame
    void Update()
    {
        if (nodo != null) { 
            CambiarLado1(nodo.getLado1());
            CambiarLado2(nodo.getLado2());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != partida.extremoD && other.gameObject != partida.extremoI) {
            if (partida.extremoD == gameObject && partida.extremoI == gameObject)
            {
                if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1() == partida.numExtremoI)
                {
                    partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), true);
                    other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                }
                else if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2() == partida.numExtremoI)
                {
                    partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), true);
                    other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                }
            }
            else
            {
                if (partida.extremoD == gameObject)
                {
                    if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1() == partida.numExtremoD)
                    {
                        partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), false);
                        other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                    }
                    else if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2() == partida.numExtremoD)
                    {
                        partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), false);
                        other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                    }
                }
                if (partida.extremoI == gameObject)
                {
                    if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1() == partida.numExtremoI)
                    {
                        partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), true);
                        other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                    }
                    else if (other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2() == partida.numExtremoI)
                    {
                        partida.AgregarFichaTablero(other.gameObject.GetComponent<FichaJ>().nodo.retornaLado2(), other.gameObject.GetComponent<FichaJ>().nodo.retornaLado1(), true);
                        other.gameObject.GetComponent<FichaJ>().AutoDestruir();
                    }
                }
            }
        }
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
