using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void 메뉴()
    {
        GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;

        gameObjectMenu.SetActive(true);
    }

    public void 종료()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            Application.Quit();
        }
        else
        {
            GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;

            gameObjectMenu.SetActive(false);

            SceneManager.LoadScene("Main");
        }
    }
}
