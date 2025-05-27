using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objetivo : MonoBehaviour, IAtacable
{
    public int vida = 100;

    public delegate void ObjetivoDestruido();
    public event ObjetivoDestruido EnObjetivoDestruido;

    
    void Update()
    {
        if (vida <= 0)
        {
            if (EnObjetivoDestruido != null)
            {
                EnObjetivoDestruido();
            }
            Destroy(gameObject);
        }
    }

    public void RecibirDano(int dano = 20)
    {
        vida -= dano;
        Debug.Log("Vida restante: " + vida);
    }
}
