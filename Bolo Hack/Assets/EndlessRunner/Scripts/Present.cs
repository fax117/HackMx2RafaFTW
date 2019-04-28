using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    HUD hud;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            hud = GameObject.Find("Main Camera").GetComponent<HUD>();
            hud.IncreaseScore(10);
            Destroy(this.gameObject);
        }    
    }
}
