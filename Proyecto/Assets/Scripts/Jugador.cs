using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class Jugador : MonoBehaviour
{
    public GameObject listaFichas;
    public NodoDoble nodo;
    private int puntos;
    Animator anim;
    public GameObject sigJugador;

    // Start is called before the first frame update
    void Start()
    {
        anim = listaFichas.GetComponent<Animator>();
    }

    public int getPuntos()
    {
        return puntos;
    }

    void Update()
    {
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

    public void ComenzarPrimero()
    {
        StartCoroutine(PrimerTurno());
    }

    public IEnumerator PrimerTurno()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 255, 0, 255);
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < 7; i++)
        {
            if(listaFichas.transform.GetChild(i).GetComponent<FichaJ>().nodo.retornaLado1() == 6 && listaFichas.transform.GetChild(i).GetComponent<FichaJ>().nodo.retornaLado2() == 6)
            {
                AnadirFichaTablero(listaFichas.transform.GetChild(i).gameObject);
                i = 7;
            }
        }
        TerminarTurno();
    }

    public void JugarTurno()
    {
        bool pasa = true;
        Ficha fic;
        fic = nodo.getJugadorLigaD().retornarPrimeraFicha();
        for (int j = 0; j < nodo.getJugadorLigaD().retornarTama(); j++)
        {
            if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == fic.retornaLado1() || GameObject.Find("Partida").GetComponent<Partida>().numExtremoI == fic.retornaLado2())
            {
                j = nodo.getJugadorLigaD().retornarTama();
                pasa = false;
            }
            else if (GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == fic.retornaLado1() || GameObject.Find("Partida").GetComponent<Partida>().numExtremoD == fic.retornaLado2())
            {
                j = nodo.getJugadorLigaD().retornarTama();
                pasa = false;
            }
            fic = fic.retornaLiga();
        }
        if (pasa)
        {
            GameObject.Find("Partida").GetComponent<Partida>().numPasa++;
            TerminarTurno();
        }
        else
        {
            GameObject.Find("Partida").GetComponent<Partida>().numPasa = 0;
            anim.SetBool("Mostrar", true);
            gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(0, 255, 0, 255);
        }
    }

    public void TerminarTurno()
    {
        anim.SetBool("Mostrar", false);
        gameObject.transform.GetChild(0).GetComponent<Text>().color = new Color(255, 255, 255, 255);
        if (nodo.getJugadorLigaD().esVacio() == true || GameObject.Find("Partida").GetComponent<Partida>().numPasa == 4)
        {
            GameObject.Find("Partida").GetComponent<Partida>().TerminarRonda(gameObject);
        }
        else 
        {
            sigJugador.GetComponent<Bot>().ComenzarTurno();
        }
    }

    public void ConfigurarFichas()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = nodo.getNombre();
        Ficha ficha;
        ficha = nodo.getJugadorLigaD().retornarPrimeraFicha();
        for (int i = 0; i < 7; i++)
        {
            listaFichas.transform.GetChild(i).GetComponent<FichaJ>().AutoReconstruir();
            listaFichas.transform.GetChild(i).GetComponent<FichaJ>().setNodo(ficha);
            ficha = ficha.retornaLiga();
        }
    }

    public void SumarPuntos(int points)
    {
        puntos += points;
    }

    void AnadirFichaTablero(GameObject x)
    {
        Ficha nod;
        nod = x.GetComponent<FichaJ>().nodo;
        GameObject.Find("Partida").GetComponent<Partida>().AgregarFichaTablero(nod.retornaLado1(), nod.retornaLado2(), false);
        nodo.getJugadorLigaD().BorrarFicha(nod.retornaLado1(), nod.retornaLado2());
        x.GetComponent<FichaJ>().AutoDestruir();
    }
}
