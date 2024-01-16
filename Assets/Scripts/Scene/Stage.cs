using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage : MonoBehaviour
{
    public Image image;
    public int chapter;
    public int number;
    public string isLock;
    public int maxChapterNumber = 3;
    public int maxStageNumber = 3;

    public void Awake()
    {   
        GameManager.stages.Add(this);
        GameManager.LOAD();

        // isLock 에 따라 이미지를 변경합니다.
        switch (isLock)
        {
            case "Clear":
                StringBuilder stage = new StringBuilder();
                stage.Append(chapter.ToString());
                stage.Append(number.ToString());
                image.sprite = Resources.Load<Sprite>($"Images/Stages/{stage}");
                break;

            case "UnClear":
                image.sprite = Resources.Load<Sprite>("Images/Stages/UnClear");
                break;

            case "Lock":
                image.sprite = Resources.Load<Sprite>("Images/Stages/Lock");
                break;
        }
    }

    public void Click()
    {
        // Lock 이 아니면 스테이지 진입을 허용합니다.
        if (isLock != "Lock")
        {
            StringBuilder sceneName = new StringBuilder();
            sceneName.Append(chapter.ToString());
            sceneName.Append("-");
            sceneName.Append(number.ToString());

            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}
