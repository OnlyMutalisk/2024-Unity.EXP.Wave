using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Wave 스크립트는 소리치기, 발구르기를 통해 발사된 Wave의 충돌 판정을 확인하고 BoundLight 프리팹을 생성하는 스크립트이다.
 *
 * Wave가 생성될때 Stomp의 경우 isTrigger 상태로 되어있고 Trail Renderer의 emitting이 꺼져있다.
 *
 * Wave의 발사 위치, 개수, 각도 등은 모두 Player의 Shout 함수와 Stomp 함수에 담겨있다.
 * 
 */

public class Wave : MonoBehaviour
{
    public GameObject BoundLightPrefab;
    GameObject clockwiseboundlight;
    GameObject counterclockwiseboundlight;

    //lifetime 변수는 Player 스크립트에서 수정되어 실행됨
    public float lifetime = 2f;
    float lefttime = 0f;
    TrailRenderer trail;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lefttime = lifetime;
        Debug.Log(lifetime);
        Debug.Log(lefttime);
        //StartCoroutine(WaitingDestroy(this.gameObject, lifetime));
        Invoke("DestroySelf", lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(lefttime > Time.deltaTime)
        {
            lefttime -= Time.deltaTime;
        }

        trail.startColor = new Color(255, 255, 255,(lefttime / lifetime));
        trail.endColor = new Color(255, 255, 255, (lefttime / lifetime));

    }



    //벽 충돌시 벽의 테두리 빛 보임
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("콜리젼 엔터");
        if (collision.gameObject.tag == "Ground")
        {
            //시계방향으로 도는 Boundlight 생성
            clockwiseboundlight = Instantiate(BoundLightPrefab, transform.position, transform.rotation);

            //boundlight 프리팹에 있는 스크립트 변수에 충돌한 벽의 콜라이더를 부여
            BoundLight boundlightscript = clockwiseboundlight.GetComponent<BoundLight>();
            boundlightscript.platformCollider = collision.gameObject.GetComponent<BoxCollider2D>();

            //boundlight의 수명길이 조정 맥시멈수명 = 2초
            if (lefttime > 2f)
            {
                boundlightscript.lifetime = 2f;
            }
            else
            {
                boundlightscript.lifetime = lefttime;
            }
            

            //반시계방향으로 도는 Boundlight 생성
            counterclockwiseboundlight = Instantiate(BoundLightPrefab, transform.position, transform.rotation);

            //boundlight 프리팹에 있는 스크립트 변수에 충돌한 벽의 콜라이더 부여 및 반시계방향임을 알림
            BoundLight counterboundlightscript = counterclockwiseboundlight.GetComponent<BoundLight>();
            counterboundlightscript.platformCollider = collision.gameObject.GetComponent<BoxCollider2D>();
            counterboundlightscript.isclockwise = false;
            
            if (lefttime > 2f)
            {
                counterboundlightscript.lifetime = 2f;
            }
            else
            {
                counterboundlightscript.lifetime = lefttime;
            }

        }
    }

    //stomp시 벽을 통과할때부터 보이기
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //벽 통과시 isTrigger를 끔으로써 충돌 판정 활성화
            CircleCollider2D wavecollider = GetComponent<CircleCollider2D>();
            wavecollider.isTrigger = false;

            //벽 통과 이후부터 trail Renderer 활성화
            gameObject.GetComponent<TrailRenderer>().emitting = true;
        }
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    IEnumerator WaitingDestroy(GameObject wave, float wavetime)
    {
        yield return new WaitForSeconds(wavetime);
        Rigidbody2D rigid = wave.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        Destroy(wave);
    }


}
