using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class ListaFichas
{
    private Ficha primeraFicha;
    private Ficha ultimaFicha;
    private int numFichas;

    public ListaFichas()
    {
        numFichas = 0;
    }

    public void AsignarFicha(int lado1, int lado2)
    {
        Ficha nodo = new Ficha(lado1, lado2);
        if (this.esVacio())
        {
            primeraFicha = nodo;
            ultimaFicha = nodo;
            nodo.asignaLiga(null);
            numFichas++;
        }
        else
        {
            ultimaFicha.asignaLiga(nodo);
            ultimaFicha = nodo;
            nodo.asignaLiga(null);
            numFichas++;
        }
    }

    public int retornarTama()
    {
        return numFichas;
    }

    public Ficha retornarPrimeraFicha()
    {
        return primeraFicha;
    }

    public Ficha retornarUltimaFicha()
    {
        return ultimaFicha;
    }

    public bool esVacio()
    {
        if (numFichas == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void BorrarFicha(int l1, int l2)
    {
        Ficha nodo = primeraFicha;
        if (primeraFicha == null)
        {
            return;
        }
        else
        {
            while (nodo != null)
            {
                if (nodo.retornaLado1() == l1 && nodo.retornaLado2() == l2 && nodo == primeraFicha)
                {
                    primeraFicha = nodo.retornaLiga();
                    nodo.asignaLiga(null);
                    numFichas--;
                    return;
                }
                else
                {
                    if (nodo.retornaLado1() == l1 && nodo.retornaLado2() == l2)
                    {
                        Ficha anterior = Anterior(l1, l2);
                        anterior.asignaLiga(nodo.retornaLiga());
                        nodo.asignaLiga(null);
                        numFichas--;
                        return;
                    }
                }
                nodo = nodo.retornaLiga();
            }
            return;
        }
    }

    public Ficha Anterior(int l1, int l2)
    {
        Ficha anterior = primeraFicha;
        if (anterior.retornaLado1() == l1 && anterior.retornaLado2() == l2)
        {
            return null;
        }
        else
        {
            while (anterior != null)
            {
                if (anterior.retornaLiga().retornaLado1() == l1 && anterior.retornaLiga().retornaLado2() == l2)
                {
                    return anterior;
                }
                anterior = anterior.retornaLiga();
            }
            return null;
        }
    }
}