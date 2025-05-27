using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreBase : MonoBehaviour
{
    public GameObject enemigo;
    public GameObject prefabBala;
    public List<GameObject> puntasCanon;

    private void Update()
    {
        if (enemigo != null)
        {
            Apuntar();
        }
    }

    private void Apuntar()
    {
        transform.LookAt(enemigo.transform.position);
    }

    public virtual void Disparar()
    { 
        foreach (GameObject punta in puntasCanon)
        {var tempBala = Instantiate(prefabBala, punta.transform.position, Quaternion.identity);
            tempBala.GetComponent<Bala>().destino = enemigo.transform.position;
        }
    }
}
