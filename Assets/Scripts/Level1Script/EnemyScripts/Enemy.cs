using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void ChangeDirection(bool moveLeft,GameObject a)
    {
        moveLeft = !moveLeft;
        Vector3 s = a.transform.localScale;
        if (moveLeft)
        {
            s.x = Mathf.Abs(s.x);
        }
        else
        {
            s.x = -Mathf.Abs(s.x);
        }
        transform.localScale = s;
    }
}
