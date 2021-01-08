using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    private int scoreCount;
    public Text coinText;
    private AudioSource aduioManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        aduioManager = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == MyTag.COIN_TAG || collision.tag == MyTag.BLOCK_TAG)
        {
            collision.gameObject.SetActive(false);

            scoreCount++;
            coinText.text = "x" + scoreCount;

            aduioManager.Play();


        }
    }
}
