using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GhostLightGone : MonoBehaviour
{
    public float GoneGhostLightDelay; // 몇초뒤 빛이 사라질지 조절
    private Light2D _light;


    // 보간하여 점점 빛이 0으로 수렴하는 코드 
    private float startValue = 1.0f; // 시작할때 intensity값 
    private float endValue = 0.0f; // 끝났을때 불빛은 0 이다.
    private float duration = 2.0f; // 2초동안 서서히 감소한다. 2초뒤에 파괴한다.
    private float currentTime = 0.0f; // 시간설명


    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // 현재 시간 갱신
        currentTime += Time.deltaTime;
        // 값 감소시키기. Mathf.Lerp(시작값, 끝값, 종료시간 / 언제부터~) 
        float _lightIntensity = Mathf.Lerp(1f, 0.0f, currentTime / duration);
        _light.intensity = _lightIntensity;

        if(currentTime>=duration) // duration 시간이 되면 파괴.
        {
            Debug.Log("코드 접근 확인");
            Destroy(transform.parent.gameObject);
        }
    }
}
