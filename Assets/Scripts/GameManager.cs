using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 모든 스테이지를 관리하기 위한 리스트 입니다.
    public static List<Stage> stages = new List<Stage>();

    /// <summary>
    /// <br>모든 내역을 저장합니다.</br>
    /// <br>현재 저장 및 불러오기 대상 : Stage 클리어 상태</br>
    /// </summary>
    public static void SAVE()
    {
        // Stage 속성을 저장합니다.
        // (key, value) = (chapter-number, 속성1-속성2-...) 형태로 저장합니다.
        foreach (Stage stage in stages)
        {
            StringBuilder key = new StringBuilder();
            key.Append(stage.chapter.ToString());
            key.Append("-");
            key.Append(stage.number.ToString());

            StringBuilder value = new StringBuilder();
            value.Append(stage.isLock);

            PlayerPrefs.SetString(key.ToString(), value.ToString());
        }
    }

    /// <summary>
    /// <br>모든 내역을 불러옵니다.</br>
    /// </summary>
    public static void LOAD()
    {
        // Stage 속성을 가져옵니다.
        // (key, value) = (chapter-number, 속성1-속성2-...) 형태로 불러옵니다.
        foreach (Stage stage in stages)
        {
            StringBuilder key = new StringBuilder();
            key.Append(stage.chapter.ToString());
            key.Append("-");
            key.Append(stage.number.ToString());

            // key 에 해당하는 데이터가 있을 경우에만 가져옵니다.
            if (PlayerPrefs.HasKey(key.ToString()) == true)
            {
                string str = PlayerPrefs.GetString(key.ToString());
                string pattern = "-";

                string[] result = Regex.Split(str, pattern);

                stage.isLock = result[0];
            }
        }
    }
}
