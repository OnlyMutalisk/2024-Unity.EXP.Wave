using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GaugeHandler : MonoBehaviour
{
    public Image gaugeBar;
    public float fillSpeed = 0.5f;
    private bool isCharging = false;

    private void Update()
    {
        if (isCharging)
        {
            FillGauge();
            UpdateGaugePosition();

        }
    }

    void FillGauge()
    {
        float currentFill = gaugeBar.fillAmount;

        if (currentFill < 1f)
        {
            currentFill += fillSpeed * Time.deltaTime;
            gaugeBar.fillAmount = currentFill;
        }
    }
    void UpdateGaugePosition()
    {
        // 캐릭터의 위치를 기준으로 게이지바 위치 조정
        Vector3 characterPosition = Camera.main.WorldToScreenPoint(transform.position);
        gaugeBar.transform.position = new Vector3(characterPosition.x, characterPosition.y + 50f, characterPosition.z);
    }
}
