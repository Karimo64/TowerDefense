using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdministradorTorres : MonoBehaviour
{

    public AdministradorToques referenciaAdminToques;
    public AdminJuego referenciaAdminJuego;
    public SpawnerEnemigos referenciaSpawner;
    public GameObject Objetivo;

    public enum TorreSeleccionada
    {
        Torre1, Torre2, Torre3, Torre4, Torre5
    }

    public TorreSeleccionada torreSeleccionada;
    public List<GameObject> prefabTorres;
    public List<GameObject> torresInstanciadas;

    public delegate void EnemigoObjetivoActualizado();
    public event EnemigoObjetivoActualizado EnEnemigoObjetivoActualizado;

    private void OnEnable()
    {
        referenciaAdminToques.EnPlataformaTocada += CrearTorre;
        referenciaSpawner.EnOleadaIniciada += ActualizarObjetivo;
        torresInstanciadas = new List<GameObject>();
    }



    private void OnDisable()
    {
        referenciaAdminToques.EnPlataformaTocada -= CrearTorre;
        referenciaSpawner.EnOleadaIniciada -=  ActualizarObjetivo;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActualizarObjetivo()
    {
        if (referenciaSpawner.laOleadaHaIniciado)
        {
            float distanciaMasCorta = float.MaxValue;
            GameObject enemigoMasCercano = null;
            foreach (GameObject enemigo in referenciaSpawner.EnemigosGenerados)
            {
                float dist = Vector3.Distance(enemigo.transform.position, Objetivo.transform.position);
                if (dist < distanciaMasCorta)
                {
                    distanciaMasCorta = dist;
                    enemigoMasCercano = enemigo;
                }
            }
            if (enemigoMasCercano != null)
            {
                foreach (GameObject torre in torresInstanciadas)
                {
                    torre.GetComponent<TorreBase>().enemigo = enemigoMasCercano;
                    torre.GetComponent<TorreBase>().Disparar();
                }
                if (EnEnemigoObjetivoActualizado != null)
                {
                    EnEnemigoObjetivoActualizado();
                }
            }
        }
        if (referenciaSpawner.laOleadaHaIniciado)
        {
            Invoke("ActualizarObjetivo", 3);
        }
        
    }

    private void CrearTorre(GameObject plataforma)
    {
        int costo = torreSeleccionada switch
        {
            TorreSeleccionada.Torre1 => 200,
            TorreSeleccionada.Torre2 => 400,
            TorreSeleccionada.Torre3 => 400,
            TorreSeleccionada.Torre4 => 1000,
            TorreSeleccionada.Torre5 => 2000,
            _ => 0
        };
        if (plataforma.transform.childCount == 0 && referenciaAdminJuego.recursos >= costo)
        {
            referenciaAdminJuego.ModificarRecursos(-costo);
            Debug.Log("Crear Torre");
            int indiceTorre = (int)torreSeleccionada;
            Vector3 posParaInstanciar = plataforma.transform.position;
            posParaInstanciar.y += 0.5f;
            GameObject torreInstanciada = Instantiate<GameObject>(prefabTorres[indiceTorre], posParaInstanciar, Quaternion.identity);
            torreInstanciada.transform.SetParent(plataforma.transform);
            torresInstanciadas.Add(torreInstanciada);
        }

    }

    public void ConfigurarTorre(int torre)
    {
        if (Enum.IsDefined(typeof(TorreSeleccionada), torre))
        {
            torreSeleccionada = (TorreSeleccionada)torre;
        }
        else
        {
            Debug.LogError("No esta definida");
        }
    }
}
