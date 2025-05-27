using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigoBase : MonoBehaviour, IAtacante, IAtacable
{
    public GameObject objetivo;
    public int vida = 100, _dano = 5, recursosGanados = 200;

    public AdminJuego referenciaAdminJuego;
    public SpawnerEnemigos referenciaSpawner;
    public Animator Anim;

    public void OnEnable()
    {
        objetivo = GameObject.Find("Objetivo");
        referenciaAdminJuego = GameObject.Find("AdminJuego").GetComponent<AdminJuego>();
        referenciaSpawner = GameObject.Find("SpawnerEnemigos").GetComponent<SpawnerEnemigos>();
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;
    }

    public void OnDisable()
    {
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido -= Detener;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objetivo.transform.position);
        Anim = GetComponent<Animator>(); 
        Anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(vida <= 0)
        {
            Anim.SetTrigger("OnDeath");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Destroy(gameObject, 3);
        }
    }

    public virtual void OnDestroy()
    {
        referenciaAdminJuego.ModificarRecursos(recursosGanados);
        referenciaSpawner.EnemigosGenerados.Remove(this.gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void Detener()
    {
        Anim.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }

    public void Danar(int dano)
    {
        if(dano == 0) dano = _dano;
        objetivo?.GetComponent<Objetivo>().RecibirDano(40);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }
}
