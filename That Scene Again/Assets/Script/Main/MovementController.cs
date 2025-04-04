using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    public bool isOnGround = false;
    private float dirX;

     void Update()
    {
        Move();
        Jump();
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    void Move()
    {
        dirX = Input.GetAxis("Horizontal");
        dirX *= ((!GameManager.Instance.reverse) ? 1 : -1);
        rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
    }

}