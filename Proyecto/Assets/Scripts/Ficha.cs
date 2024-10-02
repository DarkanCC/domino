using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha
{
    private int lado1;
    private int lado2;
    private Ficha liga;

    public Ficha(int l1, int l2) 
    {
        lado1 = l1;
        lado2 = l2;
        liga = null;
    }
    public void asignaLado1(int l1) 
    {
        lado1 = l1;
    }
    public void asignaLado2(int l2)
    {
        lado2 = l2;
    }
    public void asignaLiga(Ficha x)
    {
        liga = x;
    }
    public int retornaLado1()
    {
        return lado1;
    }
    public int retornaLado2()
    {
        return lado2;
    }
    public Ficha retornaLiga()
    {
        return liga;
    }
}
