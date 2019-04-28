using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    private Rigidbody2D playerCollisioner;
    private SpriteRenderer palletSprite;

    private bool canMoveRight;
    private bool canMoveLeft;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        playerCollisioner = GetComponent<Rigidbody2D>();
        palletSprite = GetComponent<SpriteRenderer>();
        canMoveRight = true;
        canMoveLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        canMove = GManager.instance.GameStatus();
        //float horizontal = Input.GetAxis("Horizontal");
        var position = gameObject.transform.position;

        if (Input.acceleration.x > 0 && canMoveRight && canMove)
        {
            //Moving right
            //position.x += horizontal * .37f;
            //gameObject.transform.position = position;
            gameObject.transform.Translate(Input.acceleration.x,0,0);

            palletSprite.flipX = true;
        }
        else if (Input.acceleration.x < 0 && canMoveLeft && canMove)
        {
            //Moving left
            //position.x += horizontal * .37f;
            //gameObject.transform.position = position;
            gameObject.transform.Translate(Input.acceleration.x,0,0);
            palletSprite.flipX = false;
        }

    }

    void OnTriggerEnter2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("LimitRight"))
        {
            canMoveRight = false;
        }
        else if (collisionObject.gameObject.CompareTag("LimitLeft"))
        {
            canMoveLeft = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collisionObject)
    {
        if (collisionObject.gameObject.CompareTag("LimitRight"))
        {
            canMoveRight = true;
        }
        else if (collisionObject.gameObject.CompareTag("LimitLeft"))
        {
            canMoveLeft = true;
        }
    }
}
