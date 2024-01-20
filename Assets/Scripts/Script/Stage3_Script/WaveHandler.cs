using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public GameObject wavePrefab;
    public Transform shoutpos;
    public Transform stomppos;

    GameObject[] shoutWave;
    GameObject[] stompWave;
    private void Awake()
    {
        shoutWave = new GameObject[10];
        stompWave = new GameObject[10];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Shout();

        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            Stomp();
        }
    }

    public void Shout()
    {
        //wave 개수는 10개 
        int waveNum = 10;

        //wave 0번부터 9번까지 각도 조정
        for(int index = 1; index < waveNum; index++)
        {
            //wave 생성 ; 위치는 shoutpos의 위치에서 생성하고, 각도는 0,0,0으로 시작
            shoutWave[index] = Instantiate(wavePrefab, shoutpos.position, shoutpos.rotation);

            //rigidbody 컴포넌트 불러와서 rigid로 저장
            Rigidbody2D rigid = shoutWave[index].GetComponent<Rigidbody2D>();

            // 보는 방향은 180도의 index  /  총 wave의 개수 
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * index /( waveNum-1)), Mathf.Sin(Mathf.PI * index / (waveNum-1)));

            //이부분이 addforce를 하면 오른쪽 위로 튀는 현상이 있어서 translate로 위치 변경 임시로 해놨습니다.
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
    }

    public void Stomp()
    {
        //wave 개수는 10개 
        int waveNum = 10;

        //wave 0번부터 9번까지 각도 조정
        for (int index = 1; index < waveNum; index++)
        {
            //wave 생성 ; 위치는 wavepos의 위치에서 생성하고, 각도는 0,0,0으로 시작
            stompWave[index] = Instantiate(wavePrefab, stomppos.position, stomppos.rotation);

            //rigidbody 컴포넌트 불러와서 rigid로 저장
            Rigidbody2D rigid = shoutWave[index].GetComponent<Rigidbody2D>();

            // 보는 방향은 180도의 index  /  총 wave의 개수 
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * index / (waveNum + 1)), Mathf.Sin(Mathf.PI * index / (waveNum + 1)));

            //이부분이 addforce를 하면 오른쪽 위로 튀는 현상이 있어서 translate로 위치 변경 임시로 해놨습니다.
            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
        }
    }


}
