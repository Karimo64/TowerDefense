using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Video;

public class Enemy : EnemigoBase
{
    private void Awake()
    {
        vida = 50;
        _dano = 10;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        referenciaAdminJuego.enemigosBaseDerrotados++;
    }
}
