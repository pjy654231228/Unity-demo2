using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2Health : MonoBehaviour
{
    private bool canDamage;
    private int heath=4;

    // Start is called before the first frame update
    void Start()
    {
        canDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.tag == MyTag.BULLET_TAG)
            {
                heath--;
                canDamage = false;

                if (heath == 0)
                {
                    GetComponent<Boss2Script>().DeactiveBossScript();
                }
            }
            //
            StartCoroutine(WaitForDamage());
        }
    }
    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
