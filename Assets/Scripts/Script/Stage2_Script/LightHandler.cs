using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 정확히 플레이어를 따라다니는 빛
public class LightHandler : MonoBehaviour
{
    private Player thePlayer;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        // 플레이어의 라이트 위치 조정
        this.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y -0.1f, thePlayer.transform.position.z);


    }
}
