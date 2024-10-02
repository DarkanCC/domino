using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LDL
{
	private NodoDoble fichaExtremoI;
	private NodoDoble fichaExtremoD;

	// Constructor
	public LDL()
	{
		fichaExtremoD = null;
		fichaExtremoI = null;
	}

	public bool esVacia()
	{
		if (fichaExtremoI == null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public NodoDoble getFichaExtremoI()
    {
		return fichaExtremoI;
	}

	public NodoDoble getFichaExtremoD()
	{
		return fichaExtremoD;
	}

	// M�todo para agregar domin�s al tablero
	public void agregarNodoDoble(int l1, int l2, NodoDoble extremo)
	{
		NodoDoble nodo = new NodoDoble(l1, l2);
		if (this.esVacia())
		{
			fichaExtremoI = nodo;
			fichaExtremoD = nodo;
			nodo.setFichaLigaD(null);
			nodo.setLigaI(null);
		}
		else
		{
			if (extremo == fichaExtremoI)
			{
				extremo.setLigaI(nodo);
				fichaExtremoI = nodo;
				nodo.setLigaI(null);
			}
			else
			{
				extremo.setFichaLigaD(nodo);
				fichaExtremoD = nodo;
				nodo.setFichaLigaD(null);
			}

		}
	}

	public void agregarNodoJugador(string name)
	{
		NodoDoble nodo = new NodoDoble(name);
		if (this.esVacia())
		{
			fichaExtremoI = nodo;
			fichaExtremoD = nodo;
			nodo.setJugadorLigaD(null);
			nodo.setLigaI(null);
		}
		else
		{
			fichaExtremoI.setLigaI(nodo);
			fichaExtremoI = nodo;
			fichaExtremoI.setLigaI(fichaExtremoD);
		}
	}
}
