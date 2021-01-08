using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float speed = 10f;
    private Animator anim;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        anim = GetComponent<Animator>();
        StartCoroutine(DisableBullet(5f));
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == MyTag.BEETLE_TAG || other.gameObject.tag ==MyTag.SNAIL_TAG||
            other.gameObject.tag == MyTag.BIRD_TAG|| other.gameObject.tag == MyTag.SPIDER_TAG||
            other.gameObject.tag == MyTag.FROG_TAG|| other.gameObject.tag == MyTag.BOSS_TAG)
        {
            anim.Play("Explode");
            canMove = false;

            //打中目标后延迟消失
            StartCoroutine(DisableBullet(0.3f));
        }

    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }
}
