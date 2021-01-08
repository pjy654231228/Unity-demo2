using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.PLAYER_TAG)
        {
            SceneManager.LoadScene("Level1");

        }
    }
}
