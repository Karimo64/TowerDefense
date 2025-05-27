using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreSeraph : TorreBase, IAtacante
{
    public float divisionesRayo = 10;
    public LineRenderer LRayo;
    public List<Vector3> puntos;
    public int potenciaRayo = 999; // Very high damage
    public float tiempoEntreDisparos = 10f;
    public float duracionRayo = 0.1f;

    private float temporizador;

    public void Start()
    {
        LRayo = GetComponent<LineRenderer>();
        temporizador = tiempoEntreDisparos; // So it fires immediately if needed
    }

    public void Update()
    {
        if (enemigo != null)
        {
            temporizador += Time.deltaTime;
            if (temporizador >= tiempoEntreDisparos)
            {
                temporizador = 0f;
                StartCoroutine(DispararRayoLetal());
            }
        }
        else
        {
            LRayo.positionCount = 0;
        }
    }

    private IEnumerator DispararRayoLetal()
    {
        Disparar(); // Sets the laser visuals
        Danar(potenciaRayo); // Deals damage
        yield return new WaitForSeconds(duracionRayo); // Show the beam briefly
        LRayo.positionCount = 0; // Hide the beam
    }

    public override void Disparar()
    {
        puntos = ObtenerPuntos();
        puntos.Insert(0, puntasCanon[0].transform.position);
        var posEnemigo = enemigo.transform.position;
        posEnemigo.y += 1;
        puntos.Add(posEnemigo);
        LRayo.positionCount = puntos.Count;
        LRayo.SetPositions(puntos.ToArray());
    }

    private List<Vector3> ObtenerPuntos()
    {
        List<Vector3> puntosTemporales = new List<Vector3>();
        float divisor = 1f / divisionesRayo;
        float linear = 0f;
        bool esPositivo = false;

        if (divisionesRayo == 0)
        {
            Debug.LogError("Divisiones de rayo no puede ser cero.");
            return null;
        }
        if (divisionesRayo == 1)
        {
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, 0.5f);
            puntosTemporales.Add(punto);
            return puntosTemporales;
        }

        for (int i = 0; i < divisionesRayo; i++)
        {
            linear = (i == 0) ? divisor / 2 : linear + divisor;
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, linear);
            punto.x += (esPositivo ? 1 : -1) * Random.value * 2;
            esPositivo = !esPositivo;
            puntosTemporales.Add(punto);
        }

        return puntosTemporales;
    }

    public void Danar(int dano = 0)
    {
        enemigo.GetComponent<EnemigoBase>().RecibirDano(dano);
    }
}
