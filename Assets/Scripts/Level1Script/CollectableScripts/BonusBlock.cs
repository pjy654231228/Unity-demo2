using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    private bool startAnim;
    private bool canAnimae;
    public Transform bottom_Collision;
    public LayerMask playerLayer;
    private Animator anim;
    private Vector3 moveDirection;
    private Vector3 animPosition;
    private Vector3 originPosition;
    //private AudioSource aduioManager;



    // Start is called before the first frame update
    void Start()
    {
        canAnimae = true;
        originPosition = transform.position;
        animPosition = transform.position;
        animPosition.y += 0.2f;
        moveDirection = Vector3.up;
        //
        //A = new ScoreScript();

    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        //aduioManager = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        CheckCollision();
        AnimationUpDown();
    }

    private void CheckCollision()
    {
        if (canAnimae)
        {
            RaycastHit2D hit = Physics2D.Raycast(bottom_Collision.position, Vector2.down, 0.1f, playerLayer);
            if (hit)
            {
                if (hit.collider.gameObject.tag == MyTag.PLAYER_TAG)
                {
                    anim.Play("BlockIdle");
                    //aduioManager.Play();
                    startAnim = true;
                    canAnimae = false;
                }
            }
        }
    }

    private void AnimationUpDown()
    {
        if (startAnim)
        {
            transform.Translate(moveDirection * Time.deltaTime);
            if (transform.position.y >= animPosition.y)
            {
                moveDirection = Vector3.down;
            }
            else if (transform.position.y <= originPosition.y)
            {
                startAnim = false;
            }

        }
    }
}
