using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void 시작()
    {
        GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;
        gameObjectMenu.SetActive(false);
    }

    public void 메뉴()
    {
        GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;
        gameObjectMenu.SetActive(true);
    }

    public void 종료()
    {
        if (SceneManager.GetActiveScene().name == "Main")
        {
            GameManager.SAVE();
            Application.Quit();
        }
        else
        {
            GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;

            gameObjectMenu.SetActive(false);

            SceneManager.LoadScene("Main");
        }
    }

    /// <summary>
    /// <br>해상도를 설정하기 위한 토글 입니다.</br>
    /// <br>게임오브젝트의 이름으로 설정됩니다. (Ex. 1920x1080)</br>
    /// </summary>
    public void 토글(GameObject gameObject)
    {
        string str = gameObject.name;
        string pattern = @"(\d+)x(\d+)";

        Match match = Regex.Match(str, pattern);

        int x = int.Parse(match.Groups[1].Value);
        int y = int.Parse(match.Groups[2].Value);

        Screen.SetResolution(x, y, Screen.fullScreen);
    }
}
