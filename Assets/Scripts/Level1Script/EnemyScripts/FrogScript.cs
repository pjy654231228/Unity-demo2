using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogScript : MonoBehaviour
{
    private Animator anim;
    private bool jumpLeft;
    private int jumpTime;
    private bool animation_start;
    private bool animation_finish;
    public LayerMask playerLayer;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(FrogJump());
    }
    private void Awake()
    {
        jumpLeft = true;
        jumpTime = 0;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //检测和玩家的碰撞
        if (Physics2D.OverlapCircle(transform.position, 0.5f, playerLayer))
        {
            //
            print("Frog Damage");
            Player.GetComponent<PlayerDamage>().DealDamage(); 
        }
    }

    private void LateUpdate()
    {
        if (animation_start && animation_finish)
        {
            transform.parent.position = transform.position;
            transform.localPosition = Vector3.zero;
        }

    }
    private IEnumerator FrogJump()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(1.5f, 2f));

        animation_start = true;
        animation_finish = false;

        jumpTime++;
        if (jumpLeft)
        {
            anim.Play("FrogJumpLeft");
        }
        else
        {
            anim.Play("FrogJumpRight");
        }

        StartCoroutine(FrogJump());
    }
    public void AnimationFinished()
    {
       
        animation_finish = true;
        if (jumpLeft)
        {
            anim.Play("FrogIdleLeft");
        }
        else
        {
            anim.Play("FrogIdleRight");
        }

        if (jumpTime == 3)
        {
            jumpTime = 0;
            Vector3 s = transform.localScale;
            s.x = s.x * -1;
            transform.localScale = s;

            jumpLeft = !jumpLeft;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag==MyTag.BULLET_TAG) {
            anim.Play("FrogDead");

            //StopCoroutine("FrogJump");
            //StopCoroutine(AnimationFinished());
            StartCoroutine(FrogDead());
        }
    }
    private IEnumerator FrogDead()
    {
        yield return new WaitForSeconds(0.2f);
       
       // gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
