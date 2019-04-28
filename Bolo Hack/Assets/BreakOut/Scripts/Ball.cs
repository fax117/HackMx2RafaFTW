using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float ballSpeed;
    public GameObject deathParticles;

    private Rigidbody2D rb;
    private bool gameActive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //if(/*GManager.instance.lives != 3*/false)
        //{
        //    gameActive = true;
        //    Invoke("RespawnForce", 1f);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && gameActive == false)
        {
            Debug.Log("salu2");
            transform.parent = null; // Ball is currently a child of the player's pallet.
            gameActive = true;
            rb.isKinematic = false;
            rb.AddForce(new Vector2(ballSpeed, 1.5f*ballSpeed));
            Debug.Log(ballSpeed);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log(ballSpeed);
        }

        if(GManager.instance.GameStatus() == false)
        {
            Invoke("DestroyBall", 0.25f);
        }
    }

    public void DestroyBall()
    {
        Destroy(gameObject);
    }

    //public void RespawnForce()
    //{
    //    rb.isKinematic = false;
    //    rb.AddForce(new Vector2(ballSpeed, 1.5f * ballSpeed));
    //}

    private void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("Death Zone"))
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Debug.Log("Creating Death Particles!");
            Destroy(gameObject);
        }
    }
}
