using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject objetivo;
    public int vida = 20;

    public Animator Anim;
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
        
    }

    private void OnCollisionEneter(Collision collision)
    { 
        if (collision.gameObject.CompareTag("Objetivo"))
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    public void Danar() 
    { 
        objetivo?.GetComponent<Objetivo>().RecibirDanio(5);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }
}
