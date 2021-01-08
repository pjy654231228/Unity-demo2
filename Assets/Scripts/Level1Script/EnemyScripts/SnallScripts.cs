using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnallScripts : MonoBehaviour
{
    private bool moveLeft;
    private Rigidbody2D myBody;
    public float moveSpeed = 5f;
    public Transform downCollision;
    public Transform topCollision;
    public Transform backCollision;
    public Transform frontCollision;
    public LayerMask playerLayer;
    private bool stunned = false;
    private bool canMove;
    private Animator anim;
    private BoxCollider2D sr;


    // Start is called before the first frame update
    void Start()
    {
        moveLeft = true;
        canMove = true;

        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (moveLeft)
            {
                myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
            }
            else
            {
                myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
            }
        }

        CheckCollision();

    }

    private void CheckCollision()
    {   //射线碰撞检测
        if (!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f))
        {
            ChangeDirection();
        }
        //圆形碰撞检测
        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if ("Player" == topHit.gameObject.tag)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 10f);
                    //检测到碰撞停止蜗牛移动
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    //播放死亡动画
                    anim.Play("SnallDie");
                    stunned = true;


                }
            }
        }


        //    前后碰撞检测
        RaycastHit2D frontHit = Physics2D.Raycast(frontCollision.position, moveLeft ? Vector2.left : Vector2.right, 0.1f, playerLayer);
        RaycastHit2D backHit = Physics2D.Raycast(backCollision.position, moveLeft ? Vector2.left : Vector2.right, 0.1f, playerLayer);

        if (frontHit)
        {
            if (frontHit.collider.tag == MyTag.PLAYER_TAG)
            {
                if (stunned)
                {
                    myBody.velocity = new Vector2(transform.forward.normalized.z * 15f, myBody.velocity.y);
                    sr.isTrigger = true;
                }
                else
                {
                    //减少玩家生命值
                    frontHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
            }
        }
        if (backHit)
        {
            if (backHit.collider.tag == MyTag.PLAYER_TAG)
            {
                if (stunned)
                {
                    myBody.velocity = new Vector2(transform.forward.normalized.z * -15f, myBody.velocity.y);
                    sr.isTrigger = true;
                }
                else
                {
                    //减少玩家生命值
                    backHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.BULLET_TAG)
        {
            canMove = false;
            myBody.velocity = new Vector2(0, 0);
            anim.Play("SnallDie");
            stunned = true;

            //StopCoroutine("FrogJump");
            //StopCoroutine(AnimationFinished());
            StartCoroutine(Dead());
        }
    }

    private IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    private void ChangeDirection()
    {
        moveLeft = !moveLeft;
       Vector3 s = transform.localScale;
        if (moveLeft)
        {
            s.x = Mathf.Abs(s.x);
            //sr.flipX = true;
        }
        else
        {
            s.x = -Mathf.Abs(s.x);
            //sr.flipX = false;
        }
     transform.localScale = s;
    }
}
