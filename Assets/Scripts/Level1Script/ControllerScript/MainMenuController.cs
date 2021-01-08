using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayGame() {
        //关卡的加载
        SceneManager.LoadScene("Level1");
    }
}
