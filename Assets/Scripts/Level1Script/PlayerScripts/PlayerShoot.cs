using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject fireBullet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            GameObject obj = Instantiate(fireBullet, transform.position, Quaternion.identity);
            obj.GetComponent<FireBullet>().Speed *= transform.localScale.x;
        }

    }
}
