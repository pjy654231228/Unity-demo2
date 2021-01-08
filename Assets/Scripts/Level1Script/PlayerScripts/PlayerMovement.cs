using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D mybody;
    public float speed = 5f;

    Animator aimn;
    public Transform groundCheckPosition;
    public LayerMask groundLayer;
    private bool IsGround;
    public float jumpPower = 6f;
    private bool jumped;

    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        mybody = GetComponent<Rigidbody2D>();
        aimn = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckIfGrounded();
        PlayerJump();
    }

    private void PlayerJump()
    {
        if (IsGround)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                mybody.velocity = new Vector2(mybody.velocity.x, jumpPower);
                jumped = true;
                //播放动画
                aimn.SetBool("Jump", true);
            }

        }
    }

    private void CheckIfGrounded()
    {
        IsGround = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, groundLayer);
        if (IsGround)
        {
            if (jumped)
            {
                jumped = false;
                aimn.SetBool("Jump", false);
            }
        }
    }

    private void FixedUpdate()
    {
        PlayerWalk();
    }

    private void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");
        if (h > 0)
        {
            mybody.velocity = new Vector2(speed, mybody.velocity.y);
            ChangeDirection(1);
        }
        if (h < 0)
        {
            mybody.velocity = new Vector2(-speed, mybody.velocity.y);
            ChangeDirection(-1);
        }
        aimn.SetInteger("Speed", Mathf.Abs((int)mybody.velocity.x));
    }

    private void ChangeDirection(int v)
    {
        Vector3 s = transform.localScale;
        s.x = v;
        transform.localScale = s;

    }
}



