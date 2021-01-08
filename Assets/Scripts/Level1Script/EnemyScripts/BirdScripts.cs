using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScripts : MonoBehaviour
{
    private bool canMove;
    private Vector3 moveDirection;
    private Vector3 maxPosition;
    private Vector3 minPosition;
    public float speed = 2.5f;
    private bool acttacked = false;
    public LayerMask playerLayer;
    public GameObject birdEgg;
    private Animator anim;
    private Rigidbody2D myBody;


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;

        //初始化
        moveDirection = Vector3.left;

        maxPosition = transform.position;
        maxPosition.x += 6f;

        minPosition = transform.position;
        minPosition.x -= 6f;

        anim = GetComponent<Animator>();
        myBody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveBird();
        DropTheEgg();
    }

    private void DropTheEgg()
    {
        if (!acttacked)
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, playerLayer))
            {
                Instantiate(birdEgg, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
                acttacked = true;

                anim.Play("BirdFly");
            }
        }
    }

    private void MoveBird()
    {
        if (canMove)
        {
            transform.Translate(moveDirection * Time.deltaTime * speed);
            if (transform.position.x >= maxPosition.x)
            {
                moveDirection = Vector3.left;
                ChangeDirection(1f);
            }
            else if (transform.position.x <= minPosition.x)
            {
                moveDirection = Vector3.right;
                ChangeDirection(-1f);
            }
        }
    }

    private void ChangeDirection(float v)
    {
        Vector3 s = transform.localScale;
        s.x = v;
        transform.localScale = s;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.BULLET_TAG)
        {
            anim.Play("BirdDead");
            myBody.bodyType = RigidbodyType2D.Dynamic;


            GetComponent<BoxCollider2D>().isTrigger = true;
            canMove = false;

            StartCoroutine(BirdDead());

        }
    }

    private IEnumerator BirdDead()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
