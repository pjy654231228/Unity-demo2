using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    private Animator anim;
    public Transform attackInstantiate;
    public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StartAttack");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();   
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartAttack() {
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f,5f));
        anim.Play("BossAttack");

        StartCoroutine("StartAttack");
    }

    internal void DeactiveBossScript()
    {
        StopCoroutine("StartAttack");
        anim.Play("BossDead");
        enabled = false;
    }

    void BackToIdle()
    {
        anim.Play("BossIdle");
    }

    //attack
    void Attack() {

       GameObject sto= Instantiate(stone,attackInstantiate.position,Quaternion.identity);
        sto.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-300f,-700f),0f));
    }
}
