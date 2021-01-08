using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss2Script : MonoBehaviour
{
    private bool moveLeft;
    private Rigidbody2D myBody;
    //private Transform BossPostion;
    public float moveSpeed = 5f;
    public Transform frontCollision;
    public Transform downCollision;
    public LayerMask playerLayer;
    private bool stunned = false;
    private bool canMove;
    private Animator anim;
    private Vector3 maxPosition;
    private Vector3 minPosition;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartAttack");
      
    }
    private void Awake()
    {
        moveLeft = true;
        canMove = true;
        //BossPostion = GetComponent<Transform>();
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

        
    }

    IEnumerator StartAttack()
    {
        yield return new WaitForSeconds(0f);
        
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

        StartCoroutine("StartAttack");
    }

    private void CheckCollision()
    {   //射线碰撞检测
        if (!Physics2D.Raycast(downCollision.position, Vector2.down, 0.1f))
        {

            ChangeDirection();
           
        }
        //圆形碰撞检测
        Collider2D topHit = Physics2D.OverlapCircle(frontCollision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if ("Player" == topHit.gameObject.tag)
            {
    
                    //检测到碰撞停止BOSS移动
                    canMove = false;
                //myBody.velocity = new Vector2(0, 0);
                topHit.gameObject.GetComponent<PlayerDamage>().DealDamage();
                //播放攻击动画
                anim.Play("BossAttack");
                //topHit = null;
                //StartCoroutine(WaitForDamage());
                //ChangeDirection();
                //
                //StartCoroutine(Dead());

            }
        }
     
    }



    internal void DeactiveBossScript()
    {
        StopCoroutine("StartAttack");
        anim.Play("BossDead");
        enabled = false;

        SceneManager.LoadScene("MainMeun");

    }

    private void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 s = transform.localScale;
        if (moveLeft)
        {
            s.x = Mathf.Abs(s.x);
            //print("333");
        }
        else
        {
            s.x = -Mathf.Abs(s.x);
            //print("444");
        }
        transform.localScale = s;
    }
    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(1f);
        print("111");
        ChangeDirection();
        anim.Play("BossMove");
        canMove = true;
        StartCoroutine("StartAttack");
        //canDamage = true;
    }
  
}
