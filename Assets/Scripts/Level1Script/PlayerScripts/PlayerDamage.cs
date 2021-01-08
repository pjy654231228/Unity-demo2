using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerDamage : MonoBehaviour
{
    private bool canDamage;
    private int lifeScoreCount;
    public Text lifeText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1; 
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void Awake()
    {
        canDamage = true;
        lifeScoreCount = 3;
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScoreCount--;
            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;

            }
            else
            {
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());

            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");
    }
}
