using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;

public class Bot : MonoBehaviour
{
    private int puntos;
    public NodoDoble nodo;
    public GameObject sigJugador;
    Text text;
    
    // Start is called before the first frame update
    void Start()
    {
        puntos = 0;
        text = gameObject.transform.GetChild(0).GetComponent<Text>();
    }

    public int getPuntos()
    {
        return puntos;
    }

    void Update()
    {
        if (nodo != null)
        {
            gameObject.transform.GetChild(0).GetComponent<Text>().text = nodo.getNombre();
        }
        if (puntos != 0)
        {
            gameObject.transform.GetChild(1).GetComponent<Text>().text = "" + puntos;
        }
        else
        {
            gameObject.transform.GetChild(1).GetComponent<Text>().text = "";
        }

    }

    public void setPuntos(int points)
    {
        puntos = points;
    }

    public IEnumerator JugarTurno()
    {
        text.color = new Color(0, 255, 0, 255);
        yield return new WaitForSeconds(4f);
        bool pasa = true;
        Ficha fic;
        fic = nodo.getJugadorLigaD().retornarPrimeraFicha();
        for (int j = 0; j < nodo.getJugadorLigaD().retornarTama(); j++)
        {
            if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == fic.retornaLado1() || GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == fic.retornaLado2())
            {
                j = nodo.getJugadorLigaD().retornarTama();
                pasa = false;
                AnadirFichaTablero(fic, true);
            }
            else if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == fic.retornaLado1() || GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == fic.retornaLado2())
            {
                j = nodo.getJugadorLigaD().retornarTama();
                pasa = false;
                AnadirFichaTablero(fic, false);
            }
            fic = fic.retornaLiga();
        }
        if (pasa)
        {
            GameObject.Find("Partida").GetComponent<Partida>().numPasa++;
        }
        else
        {
            GameObject.Find("Partida").GetComponent<Partida>().numPasa = 0;
        }
        TerminarTurno();
    }

    public void ComenzarPrimero()
    {
        StartCoroutine(PrimerTurno());
    }

    public void ComenzarTurno()
    {
        StartCoroutine(JugarTurno());
    }

    public IEnumerator PrimerTurno()
    {
        text.color = new Color(0, 255, 0, 255);
        Ficha ficha;
        ficha = nodo.getJugadorLigaD().retornarPrimeraFicha();
        yield return new WaitForSeconds(2f);
        for (int i = 1; i < 8; i++)
        {
            if (ficha.retornaLado1() == 6 && ficha.retornaLado2() == 6)
            {
                AnadirFichaTablero(ficha, false);
                i = 8;
            }
            ficha = ficha.retornaLiga();
        }
        TerminarTurno();
    }

    public void TerminarTurno()
    {
        text.color = new Color(255, 255, 255, 255);
        if (nodo.getJugadorLigaD().esVacio() == true || GameObject.Find("Partida").GetComponent<Partida>().numPasa == 4)
        {
            GameObject.Find("Partida").GetComponent<Partida>().TerminarRonda(gameObject);
        }
        else
        {
            if (sigJugador == GameObject.Find("Jugador"))
            { 
                sigJugador.GetComponent<Jugador>().JugarTurno();
            }
            else
            {
                sigJugador.GetComponent<Bot>().ComenzarTurno();
            }
        }
    }

    public void SumarPuntos(int points)
    {
        puntos += points;
    }

    private void AnadirFichaTablero(Ficha x, bool iz)
    {
        if (x.retornaLado1() == 6 && x.retornaLado2() == 6)
        {
            GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(x.retornaLado1(), x.retornaLado2(), iz);
        }
        else
        {
            if (iz)
            {
                if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == x.retornaLado1())
                {
                    GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(x.retornaLado1(), x.retornaLado2(), iz);
                }
                else if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == x.retornaLado2())
                {
                    GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(x.retornaLado2(), x.retornaLado1(), iz);
                }
            }
            else
            {
                if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == x.retornaLado1())
                {
                    GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(x.retornaLado1(), x.retornaLado2(), iz);
                }
                else if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == x.retornaLado2())
                {
                    GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(x.retornaLado2(), x.retornaLado1(), iz);
                }
            }
        }
        nodo.getJugadorLigaD().BorrarFicha(x.retornaLado1(), x.retornaLado2());
    }
}
