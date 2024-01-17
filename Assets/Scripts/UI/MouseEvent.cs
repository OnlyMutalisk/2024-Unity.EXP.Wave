using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseEvent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image image;

    /// <summary>
    /// <br>Menu 하위 버튼 위로 마우스가 올라가면 이미지를 변경합니다.</br>
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (image.sprite.name)
        {
            case "재개":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/ㅡ재개ㅡ");
                break;

            case "설정":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/ㅡ설정ㅡ");
                break;

            case "스테이지 나가기":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/ㅡ스테이지 나가기ㅡ");
                break;
        }
    }

    /// <summary>
    /// <br>Menu 하위 버튼 위로부터 마우스가 벗어나면 이미지를 되돌립니다.</br>
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        switch (image.sprite.name)
        {
            case "ㅡ재개ㅡ":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/재개");
                break;

            case "ㅡ설정ㅡ":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/설정");
                break;

            case "ㅡ스테이지 나가기ㅡ":
                image.sprite = Resources.Load<Sprite>("Images/Buttons/스테이지 나가기");
                break;
        }
    }
}
