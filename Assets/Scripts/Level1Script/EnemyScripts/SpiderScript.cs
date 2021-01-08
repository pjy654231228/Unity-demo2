using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderScript : MonoBehaviour
{
    private Vector3 moveDirection;
    private Animator anim;
    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Vector3.down;
        StartCoroutine(ChanegeMovement());
    }

    // Update is called once per frame
    void Update()
    {
        SpiderMove();
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();
    }

    private void SpiderMove()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChanegeMovement()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 5f));
        if (moveDirection == Vector3.down)
        {
            moveDirection = Vector3.up;
        }
        else
        {
            moveDirection = Vector3.down;
        }
        StartCoroutine(ChanegeMovement());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.BULLET_TAG)
        {
            anim.Play("SpiderDead");
            myBody.bodyType = RigidbodyType2D.Dynamic;


            StopCoroutine(ChanegeMovement());
            StartCoroutine(SpiderDead());
        }
        if (collision.tag == MyTag.PLAYER_TAG)
        {
            
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }

    private IEnumerator SpiderDead()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
