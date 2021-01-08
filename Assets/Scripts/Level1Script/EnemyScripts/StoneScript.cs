using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Deactive",4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deactive() {
        Destroy(gameObject);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.PLAYER_TAG)
        {
            collision.GetComponent<PlayerDamage>().DealDamage();
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }
 
}
