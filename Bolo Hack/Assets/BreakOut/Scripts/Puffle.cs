using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puffle : MonoBehaviour
{

    public GameObject puffleDeathParticle;

    public int puffleType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Instantiate(puffleDeathParticle, transform.position, Quaternion.identity);
        GManager.instance.DestroyPuffle(puffleType);
        GManager.instance.UpdateScore(puffleType);
        Destroy(gameObject);
    }
}
