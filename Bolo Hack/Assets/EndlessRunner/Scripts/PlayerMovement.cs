using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40;
    float horizontalMove = 0f;
    public bool jump = false;

    void Awake()
    {
        controller = GetComponent<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            jump = true;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            /*Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0f;
            transform.position = touchPosition;*/
            jump = true;
        }      
    }
    void FixedUpdate()
    {
        controller.Move(1, false, jump);
        jump = false;
    }
}
