using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Player에 부착되어야 하는 코드. 프리팹을 넣어주면 그것을 잔상처럼 불러옴.
public class GhostLightHandler : MonoBehaviour
{
    public float ghostDelay; // 분신 개수 조절가능.
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = false; // 분신 불빛 만드는 것 제어
    void Start()
    {
        // 불빛이 생기는 딜레이
        ghostDelaySeconds = ghostDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (makeGhost)
        {
            if (ghostDelaySeconds > 0)
            {
                
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //따라오는 불빛생성 
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.position= new Vector3(transform.position.x,transform.position.y - 0.5f,transform.position.z);
                ghostDelaySeconds = ghostDelay;
            }
        }
    }
}
