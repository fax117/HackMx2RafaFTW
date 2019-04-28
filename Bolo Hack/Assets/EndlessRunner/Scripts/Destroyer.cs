using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<GamerManager>().EndGame();

        }

        if (collision.gameObject.transform.parent)
        {
            Destroy(collision.gameObject.transform.parent.gameObject);
        }
        else
            Destroy(collision.gameObject);
    }
}
