using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodoDoble
{
	private string nombre;
    private int lado1;
    private int lado2;
	private NodoDoble ligaI;
	private NodoDoble fichaLigaD;
	private ListaFichas jugadorLigaD;

	public NodoDoble(int l1, int l2)
    {
        lado1 = l1;
		lado2 = l2;
		this.ligaI = null;
        this.fichaLigaD = null;
    }

	public NodoDoble(string name)
	{
		nombre = name;
		this.ligaI = null;
		this.jugadorLigaD = null;
	}

	public string getNombre()
	{
		return nombre;
	}

	public int getLado1()
	{
		return lado1;
	}

	public int getLado2()
	{
		return lado2;
	}

	public NodoDoble getLigaI()
	{
		return ligaI;
	}

	public NodoDoble getFichaLigaD()
	{
		return fichaLigaD;
	}

	public ListaFichas getJugadorLigaD()
	{
		return jugadorLigaD;
	}

	public void setNombre(string nombre)
	{
		this.nombre = nombre;
	}

	public void setLado1(int lado1)
	{
		this.lado1 = lado1;
	}

	public void setLado2(int lado2)
	{
		this.lado2 = lado2;
	}

	public void setLigaI(NodoDoble ligaI)
	{
		this.ligaI = ligaI;
	}

	public void setFichaLigaD(NodoDoble fichaLigaD)
	{
		this.fichaLigaD = fichaLigaD;
	}

	public void setJugadorLigaD(ListaFichas jugadorLigaD)
	{
		this.jugadorLigaD = jugadorLigaD;
	}
}
