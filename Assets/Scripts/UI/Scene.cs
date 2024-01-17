using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        GameObject gameObjectMenu = GameObject.Find("CanvasDontDestroy").transform.GetChild(0).gameObject;

        gameObjectMenu.SetActive(false);

        SceneManager.LoadScene(sceneName);
    }

    public void Clear()
    {
        #region 스테이지 클리어 상태 변환

        // 현재 스테이지 정보를 씬 네임으로 찾아 게임 매니저로부터 가져옵니다.
        string str = SceneManager.GetActiveScene().name;
        string pattern = @"(\d+)-(\d+)";

        Match match = Regex.Match(str, pattern);

        int chapter = int.Parse(match.Groups[1].Value);
        int number = int.Parse(match.Groups[2].Value);

        Stage currentStage = GameManager.stages.FirstOrDefault(i => i.chapter == chapter && i.number == number);

        // 현재 스테이지가 UnClear 상태면, Clear 로 전환 후 다음 스테이지를 오픈합니다.
        if (currentStage.isLock == "UnClear")
        {
            currentStage.isLock = "Clear";

            if (currentStage.number < currentStage.maxStageNumber)
            {
                Stage nextStage = GameManager.stages.FirstOrDefault(i => i.chapter == chapter && i.number == number + 1);
                nextStage.isLock = "UnClear";
            }
            else if (currentStage.number == currentStage.maxStageNumber)
            {
                if (currentStage.chapter == currentStage.maxChapterNumber)
                {
                    // 게임 클리어
                }
                else if (currentStage.chapter < currentStage.maxChapterNumber)
                {
                    Stage nextStage = GameManager.stages.FirstOrDefault(i => i.chapter == chapter + 1 && i.number == 1);
                    nextStage.isLock = "UnClear";
                }
            }
        }
        // 현재 스테이지가 Clear 상태면, 이미 클리어 한 스테이지이므로 아무것도 하지 않습니다.
        else if (currentStage.isLock == "Clear")
        {

        }

        #endregion

        GameManager.SAVE();

        SceneManager.LoadScene("Main");
    }
}
