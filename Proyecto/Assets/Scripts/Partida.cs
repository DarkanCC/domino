using System;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Globalization;

public class Partida : MonoBehaviour
{
    private NodoDoble primerJugador;
    public GameObject prefabFicha;
    public GameObject prefabFichaV;
    [HideInInspector]
    public GameObject extremoI;
    [HideInInspector]
    public GameObject extremoD;
    [HideInInspector]
    public GameObject primJ;

    public GameObject Jugador;
    public GameObject Bot1;
    public GameObject Bot2;
    public GameObject Bot3;
    public GameObject GanadorRonda;
    public GameObject GanadorPartida;
    private ListaFichas lista;
    private double filaIniD;
    private double filaIniI;
    private double numAux;
    private double numAuxD;
    private LDL listaJugadores;
    private LDL listaTablero;
    public int numExtremoD;
    public int numExtremoI;
    [HideInInspector]
    public int numPasa;

    public AudioClip audioFicha;
    AudioSource audioSource;

    private bool ultimaV;
    private bool ultimaVD;
    private bool filaIz;
    private bool filaDe;
    private bool filaI;
    private bool filaD;
    
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        GanadorRonda.SetActive(false);
        GanadorPartida.SetActive(false);
        EmpezarPartida();
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            MenuPrincipal();
        }
    }

        public void CerrarJuego()
    {
        Application.Quit();
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }

    public void EmpezarPartida()
    {
        Jugador.GetComponent<Jugador>().setPuntos(0);
        Bot1.GetComponent<Bot>().setPuntos(0);
        Bot2.GetComponent<Bot>().setPuntos(0);
        Bot3.GetComponent<Bot>().setPuntos(0);
        EmpezarRonda();
    }

    public void EmpezarRonda()
    {
        GameObject[] todasFichas = GameObject.FindGameObjectsWithTag("FichaTablero");
        foreach (GameObject fichaActual in todasFichas)
        {
            Destroy(fichaActual);
        }
        GanadorRonda.SetActive(false);
        listaJugadores = new LDL();
        listaTablero = new LDL();
        numPasa = 0;
        filaIniI = 0.5;
        filaIniD = 0.5;
        FichasCompletas();
        AgregarJugador(GameObject.Find("Nombres").GetComponent<Nombres>().N1);
        Jugador.GetComponent<Jugador>().nodo = listaJugadores.getFichaExtremoI();
        Jugador.GetComponent<Jugador>().ConfigurarFichas();
        if (primerJugador == Jugador.GetComponent<Jugador>().nodo) primJ = Jugador;
        AgregarJugador(GameObject.Find("Nombres").GetComponent<Nombres>().N2);
        Bot1.GetComponent<Bot>().nodo = listaJugadores.getFichaExtremoI();
        if (primerJugador == Bot1.GetComponent<Bot>().nodo) primJ = Bot1;
        AgregarJugador(GameObject.Find("Nombres").GetComponent<Nombres>().N3);
        Bot2.GetComponent<Bot>().nodo = listaJugadores.getFichaExtremoI();
        if (primerJugador == Bot2.GetComponent<Bot>().nodo) primJ = Bot2;
        AgregarJugador(GameObject.Find("Nombres").GetComponent<Nombres>().N4);
        Bot3.GetComponent<Bot>().nodo = listaJugadores.getFichaExtremoI();
        if (primerJugador == Bot3.GetComponent<Bot>().nodo) primJ = Bot3;

        if (primJ == Jugador)
        {
            Jugador.GetComponent<Jugador>().ComenzarPrimero();
        }
        else
        {
            primJ.GetComponent<Bot>().ComenzarPrimero();
        }
    }

    public void TerminarRonda(GameObject juga)
    {
        if(numPasa == 4)
        {
            int puntos = 0;
            int puntosJ1 = 0, puntosJ2 = 0, puntosJ3 = 0, puntosJ4 = 0;
            NodoDoble nodoD = listaJugadores.getFichaExtremoD();
            for (int c = 1; c < 5; c++)
            {
                Ficha fichaX = nodoD.getJugadorLigaD().retornarPrimeraFicha();
                for (int z = 0; z < nodoD.getJugadorLigaD().retornarTama(); z++)
                {
                    if (c == 1)
                    {
                        puntosJ1 += fichaX.retornaLado1();
                        puntosJ1 += fichaX.retornaLado2();
                    }
                    if (c == 2)
                    {
                        puntosJ2 += fichaX.retornaLado1();
                        puntosJ2 += fichaX.retornaLado2();
                    }
                    if (c == 3)
                    {
                        puntosJ3 += fichaX.retornaLado1();
                        puntosJ3 += fichaX.retornaLado2();
                    }
                    if (c == 4)
                    {
                        puntosJ4 += fichaX.retornaLado1();
                        puntosJ4 += fichaX.retornaLado2();
                    }
                    fichaX = fichaX.retornaLiga();
                }
                nodoD = nodoD.getLigaI();
            }
                GameObject ganador = null;
                if (puntosJ1 < puntosJ2 && puntosJ1 < puntosJ4 && puntosJ1 < puntosJ3)
                {
                    puntos = puntosJ2 + puntosJ3 + puntosJ4;
                    Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                    ganador = Jugador;
                }
                if (puntosJ2 < puntosJ1 && puntosJ2 < puntosJ4 && puntosJ2 < puntosJ3)
                {
                    puntos = puntosJ1 + puntosJ3 + puntosJ4;
                    Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                    ganador = Bot1;
                }
                if (puntosJ3 < puntosJ2 && puntosJ3 < puntosJ4 && puntosJ3 < puntosJ1)
                {
                    puntos = puntosJ2 + puntosJ1 + puntosJ4;
                    Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                    ganador = Bot2;
                }
                if (puntosJ4 < puntosJ2 && puntosJ4 < puntosJ1 && puntosJ4 < puntosJ3)
                {
                    puntos = puntosJ2 + puntosJ3 + puntosJ1;
                    Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                    ganador = Bot3;
                }
                if (puntosJ1 == puntosJ2 && puntosJ1 < puntosJ3 && puntosJ1 < puntosJ4)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                }
                if (puntosJ1 == puntosJ3 && puntosJ1 < puntosJ2 && puntosJ1 < puntosJ4)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                }
                if (puntosJ1 == puntosJ4 && puntosJ1 < puntosJ2 && puntosJ1 < puntosJ3)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ4;
                        Jugador.GetComponent<Jugador>().SumarPuntos(puntos);
                        ganador = Jugador;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                }
                if (puntosJ2 == puntosJ3 && puntosJ2 < puntosJ1 && puntosJ1 < puntosJ4)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                }
                if (puntosJ2 == puntosJ4 && puntosJ2 < puntosJ1 && puntosJ1 < puntosJ3)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ1 + puntosJ3 + puntosJ4;
                        Bot1.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot1;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                }
                if (puntosJ3 == puntosJ4 && puntosJ3 < puntosJ1 && puntosJ3 < puntosJ2)
                {
                    if (juga == Jugador)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                    if (juga == Bot1)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                    if (juga == Bot2)
                    {
                        puntos = puntosJ2 + puntosJ1 + puntosJ4;
                        Bot2.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot2;
                    }
                    if (juga == Bot3)
                    {
                        puntos = puntosJ2 + puntosJ3 + puntosJ1;
                        Bot3.GetComponent<Bot>().SumarPuntos(puntos);
                        ganador = Bot3;
                    }
                }

            if (ganador == Jugador)
            {
                if (ganador.GetComponent<Jugador>().getPuntos() > 100)
                {
                    TerminarPartida(ganador.GetComponent<Jugador>().nodo.getNombre());
                }
                else
                {
                    GanadorRonda.SetActive(true);
                    GanadorRonda.transform.GetChild(0).GetComponent<Text>().text = ganador.GetComponent<Jugador>().nodo.getNombre() + " ha ganado la ronda.";
                }
            }
            else
            {
                if (ganador.GetComponent<Bot>().getPuntos() > 100)
                {
                    TerminarPartida(ganador.GetComponent<Bot>().nodo.getNombre());
                }
                else
                {
                    GanadorRonda.SetActive(true);
                    GanadorRonda.transform.GetChild(0).GetComponent<Text>().text = ganador.GetComponent<Bot>().nodo.getNombre() + " ha ganado la ronda.";
                }
            }
        }
        else
        {
            int puntos = 0;
            NodoDoble nodoD = listaJugadores.getFichaExtremoD();
            for (int y = 1; y < 5; y++)
            {
                if(nodoD.getJugadorLigaD().retornarTama() != 0)
                {
                    Ficha fichaX = nodoD.getJugadorLigaD().retornarPrimeraFicha();
                    for(int z = 0; z < nodoD.getJugadorLigaD().retornarTama(); z++)
                    {
                        puntos += fichaX.retornaLado1();
                        puntos += fichaX.retornaLado2();
                        fichaX = fichaX.retornaLiga();
                    }
                }
                nodoD = nodoD.getLigaI();
            }
            if (juga == Jugador)
            {
                juga.GetComponent<Jugador>().SumarPuntos(puntos);
                if (juga.GetComponent<Jugador>().getPuntos() > 100)
                {
                    TerminarPartida(juga.GetComponent<Jugador>().nodo.getNombre());
                }
                else
                {
                    GanadorRonda.SetActive(true);
                    GanadorRonda.transform.GetChild(0).GetComponent<Text>().text = juga.GetComponent<Jugador>().nodo.getNombre() + " ha ganado la ronda.";
                }
            }
            else
            {
                juga.GetComponent<Bot>().SumarPuntos(puntos);
                if (juga.GetComponent<Bot>().getPuntos() > 100)
                {
                    TerminarPartida(juga.GetComponent<Bot>().nodo.getNombre());
                }
                else
                {
                    GanadorRonda.SetActive(true);
                    GanadorRonda.transform.GetChild(0).GetComponent<Text>().text = juga.GetComponent<Bot>().nodo.getNombre() + " ha ganado la ronda.";
                }
            }
        }
    }

    public void TerminarPartida(string jugad)
    {
        GanadorPartida.SetActive(true);
        GanadorPartida.transform.GetChild(0).GetComponent<Text>().text = jugad + " ha ganado la partida.";
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(2);
    }

    public void AgregarFichaTablero(int l1, int l2, bool izquierda)
    {
        GameObject fichaAc;
        if (listaTablero.esVacia())
        {
            listaTablero.agregarNodoDoble(l1, l2, null);
            fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(0, Convert.ToSingle(1.5), 0), Quaternion.identity);
            fichaAc.transform.GetComponent<FichaT>().nodo = listaTablero.getFichaExtremoI();
            ultimaV = true;
            ultimaVD = true;
            filaIz = true;
            filaDe = true;
            extremoD = fichaAc;
            extremoI = fichaAc;
            numExtremoD = l2;
            numExtremoI = l2;
            audioSource.clip = audioFicha;
            audioSource.Play();
        }
        else
        {
            if (izquierda)
            {
                ///////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////
                if (l1 == l2)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (filaIz)
                    {
                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                        if (filaIniI + 1 > 15)
                        {
                            fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            numAux = filaIniI + 1;
                            numAux = numAux - 15;
                            numAux = 1 - numAux;
                            filaIniI = 0;
                            filaIz = false;
                            ultimaV = false;
                            filaI = true;
                        }
                        else
                        {
                            fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                            ultimaV = true;
                            filaIniI = filaIniI + 1;
                        }
                    }
                    else
                    {
                        if(filaIniI == 0)
                        {
                            if (filaI)
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoI());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x + Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            else
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            filaIniI = filaIniI + numAux + 2;
                            ultimaV = false;
                        }
                        else
                        {
                            if(filaIniI + 1 > 30)
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                if (filaI) 
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x + Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                                }
                                else
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                                }
                                numAux = 1 - filaIniI + 1 - 30;
                                filaIniI = 0;
                                filaIz = false;
                                ultimaV = false;
                                filaI = !filaI;
                            }
                            else
                            {
                                if (filaI)
                                {
                                    listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoI());
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x + Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                                }
                                else
                                {
                                    listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                                }
                                filaIniI = filaIniI + 1;
                                ultimaV = true;
                            }
                        }
                    }
                    
                }
                else
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (filaIz)
                    {
                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                        if (filaIniI + 2 > 15)
                        {
                            if (ultimaV)
                            {
                                fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x, extremoI.transform.position.y + 2, 0), Quaternion.identity);
                            }
                            else
                            {
                                fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            numAux = filaIniI + 2;
                            numAux = numAux - 15;
                            numAux = 2 - numAux;
                            filaIniI = 0;
                            filaIz = false;
                            ultimaV = false;
                            filaI = true;
                        }
                        else
                        {
                            if (ultimaV)
                            {
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                            }
                            else
                            {
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - 2, extremoI.transform.position.y, 0), Quaternion.identity);
                            }
                            ultimaV = false;
                            filaIniI = filaIniI + 2;
                        }
                    }
                    else
                    {
                        if (filaIniI == 0)
                        {
                            if (filaI)
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoI());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x + Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            else
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            filaIniI = filaIniI + numAux + 2;
                            ultimaV = false;
                        }
                        else
                        {
                            if (filaIniI + 2 > 30)
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                if (ultimaV)
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x, extremoI.transform.position.y + 2, 0), Quaternion.identity);
                                }
                                else
                                {
                                    if (filaI)
                                    {
                                        fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x + Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoI.transform.position.x - Convert.ToSingle(0.5), extremoI.transform.position.y + Convert.ToSingle(1.5), 0), Quaternion.identity);
                                    }
                                }
                                numAux = 2 - filaIniI + 2 - 30;
                                filaIniI = 0;
                                filaIz = false;
                                ultimaV = false;
                                filaI = !filaI;
                            }
                            else
                            {
                                if (ultimaV)
                                {
                                    if (filaI)
                                    {
                                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoI());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x + Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - Convert.ToSingle(1.5), extremoI.transform.position.y, 0), Quaternion.identity);
                                    }
                                }
                                else
                                {
                                    if (filaI)
                                    {
                                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoI());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x + 2, extremoI.transform.position.y, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoI());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoI.transform.position.x - 2, extremoI.transform.position.y, 0), Quaternion.identity);
                                    }
                                }
                                filaIniI = filaIniI + 2;
                                ultimaV = false;
                            }
                        }
                    }
                }
                audioSource.clip = audioFicha;
                audioSource.Play();
                fichaAc.transform.GetComponent<FichaT>().nodo = listaTablero.getFichaExtremoI();
                numExtremoI = l2;
                if(extremoI.GetComponent<FichaT>().nodo.getLado1() != 6 && extremoI.GetComponent<FichaT>().nodo.getLado2() != 6)
                {
                    extremoI.GetComponent<BoxCollider2D>().enabled = false;
                }
                extremoI = fichaAc;
            }
            else
            {
                ///////////////////////////////////////////////////////////////////////
                ///////////////////////////////////////////////////////////////////////
                if (l1 == l2)
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (filaDe)
                    {
                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                        if (filaIniD + 1 > 15)
                        {
                            fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            numAuxD = filaIniD + 1;
                            numAuxD = numAuxD - 15;
                            numAuxD = 1 - numAuxD;
                            filaIniD = 0;
                            filaDe = false;
                            ultimaVD = false;
                            filaD = true;
                        }
                        else
                        {
                            fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                            ultimaVD = true;
                            filaIniD += 1;
                        }
                    }
                    else
                    {
                        if (filaIniD == 0)
                        {
                            if (filaD)
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoD());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x - Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            else
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            filaIniD += numAuxD + 2;
                            ultimaVD = false;
                        }
                        else
                        {
                            if (filaIniD + 1 > 30)
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                if (filaD)
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x - Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                                }
                                else
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                                }
                                numAuxD = 1 - filaIniD + 1 - 30;
                                filaIniD = 0;
                                filaDe = false;
                                ultimaVD = false;
                                filaD = !filaD;
                            }
                            else
                            {
                                if (filaD)
                                {
                                    listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoD());
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x - Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                                }
                                else
                                {
                                    listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                                }
                                filaIniD += 1;
                                ultimaVD = true;
                            }
                        }
                    }

                }
                else
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    if (filaDe)
                    {
                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                        if (filaIniD + 2 > 15)
                        {
                            if (ultimaVD)
                            {
                                fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x, extremoD.transform.position.y - 2, 0), Quaternion.identity);
                            }
                            else
                            {
                                fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            numAuxD = filaIniD + 2;
                            numAuxD = numAuxD - 15;
                            numAuxD = 2 - numAuxD;
                            filaIniD = 0;
                            filaDe = false;
                            ultimaVD = false;
                            filaD = true;
                        }
                        else
                        {
                            if (ultimaVD)
                            {
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                            }
                            else
                            {
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + 2, extremoD.transform.position.y, 0), Quaternion.identity);
                            }
                            ultimaVD = false;
                            filaIniD += 2;
                        }
                    }
                    else
                    {
                        if (filaIniD == 0)
                        {
                            if (filaD)
                            {
                                listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoD());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x - Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            else
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                            }
                            filaIniD += numAuxD + 2;
                            ultimaVD = false;
                        }
                        else
                        {
                            if (filaIniD + 2 > 30)
                            {
                                listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                if (ultimaVD)
                                {
                                    fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x, extremoD.transform.position.y - 2, 0), Quaternion.identity);
                                }
                                else
                                {
                                    if (filaD)
                                    {
                                        fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x - Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        fichaAc = Instantiate<GameObject>(prefabFichaV, new Vector3(extremoD.transform.position.x + Convert.ToSingle(0.5), extremoD.transform.position.y - Convert.ToSingle(1.5), 0), Quaternion.identity);
                                    }
                                }
                                numAuxD = 2 - filaIniD + 2 - 30;
                                filaIniD = 0;
                                filaDe = false;
                                ultimaVD = false;
                                filaD = !filaD;
                            }
                            else
                            {
                                if (ultimaVD)
                                {
                                    if (filaD)
                                    {
                                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoD());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x - Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + Convert.ToSingle(1.5), extremoD.transform.position.y, 0), Quaternion.identity);
                                    }
                                }
                                else
                                {
                                    if (filaD)
                                    {
                                        listaTablero.agregarNodoDoble(l2, l1, listaTablero.getFichaExtremoD());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x - 2, extremoD.transform.position.y, 0), Quaternion.identity);
                                    }
                                    else
                                    {
                                        listaTablero.agregarNodoDoble(l1, l2, listaTablero.getFichaExtremoD());
                                        fichaAc = Instantiate<GameObject>(prefabFicha, new Vector3(extremoD.transform.position.x + 2, extremoD.transform.position.y, 0), Quaternion.identity);
                                    }
                                }
                                filaIniD += 2;
                                ultimaVD = false;
                            }
                        }
                    }
                }
                audioSource.clip = audioFicha;
                audioSource.Play();
                fichaAc.GetComponent<FichaT>().nodo = listaTablero.getFichaExtremoD();
                if (extremoD.GetComponent<FichaT>().nodo.getLado1() != 6 && extremoD.GetComponent<FichaT>().nodo.getLado2() != 6)
                {
                    extremoD.GetComponent<BoxCollider2D>().enabled = false;
                }
                extremoD = fichaAc;
                numExtremoD = l2;
            }
        }
    }

    public void AgregarJugador(string name)
    {
        listaJugadores.agregarNodoJugador(name);
        listaJugadores.getFichaExtremoI().setJugadorLigaD(new ListaFichas());
        RepartirFichas(listaJugadores.getFichaExtremoI());
    }

    private void FichasCompletas()
    {
        lista = new ListaFichas();
        lista.AsignarFicha(0, 0);
        lista.AsignarFicha(0, 1);
        lista.AsignarFicha(0, 2);
        lista.AsignarFicha(0, 3);
        lista.AsignarFicha(0, 4);
        lista.AsignarFicha(0, 5);
        lista.AsignarFicha(0, 6);
        lista.AsignarFicha(1, 1);
        lista.AsignarFicha(1, 2);
        lista.AsignarFicha(1, 3);
        lista.AsignarFicha(1, 4);
        lista.AsignarFicha(1, 5);
        lista.AsignarFicha(1, 6);
        lista.AsignarFicha(2, 2);
        lista.AsignarFicha(2, 3);
        lista.AsignarFicha(2, 4);
        lista.AsignarFicha(2, 5);
        lista.AsignarFicha(2, 6);
        lista.AsignarFicha(3, 3);
        lista.AsignarFicha(3, 4);
        lista.AsignarFicha(3, 5);
        lista.AsignarFicha(3, 6);
        lista.AsignarFicha(4, 4);
        lista.AsignarFicha(4, 5);
        lista.AsignarFicha(4, 6);
        lista.AsignarFicha(5, 5);
        lista.AsignarFicha(5, 6);
        lista.AsignarFicha(6, 6);
    }

    private void RepartirFichas(NodoDoble player)
    {
        UnityEngine.Debug.Log(player.getNombre());
        for (int i = 1; i < 8; i++) {
            System.Random rnd = new System.Random();
            int posicionFicha = rnd.Next(1, lista.retornarTama() + 1);
            Ficha ficha = lista.retornarPrimeraFicha();
            for (int j = 1; j < posicionFicha; j++)
            {
                ficha = ficha.retornaLiga();
            }
            if (ficha.retornaLado1() == 6 && ficha.retornaLado2() == 6) primerJugador = player;
            player.getJugadorLigaD().AsignarFicha(ficha.retornaLado1(), ficha.retornaLado2());
            lista.BorrarFicha(ficha.retornaLado1(), ficha.retornaLado2());
            UnityEngine.Debug.Log("Lado 1: " + ficha.retornaLado1() + ", Lado 2: " + ficha.retornaLado2());
        }
    }
}