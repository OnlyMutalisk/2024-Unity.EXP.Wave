using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rig;
    // 시간관련 타이머 설정
    public UnityEngine.UI.Text text_Timer;
    // 쿨타임계산
    private float time_current;
    private float cooltime_Stomp = 2.8f;
    private float cooltime_Shout = 2.8f;
    private bool isEnded_Stomp = true;
    private bool isStomping = false;
    private bool isShouting = false;
    private bool isWalking = false;

    // 움직임 관련 정의
    public float moveSpeed;
    public float maxSpeed;
    public float jumpPower;
    public float Scale = 3f; // 캐릭터 크기조정
    public bool jumpable = false;
    float First_moveSpeed = 3; // 
    float First_maxSpeed = 5;
    float Run_moveSpeed = 6;
    float Run_maxSpeed = 10;
    float Gauge = 0;

    //소리치기 발구르기 관련 정의
    public GameObject wavePrefab;
    public Transform shoutpos;
    public Transform stomppos;
    GameObject[] shoutWave;
    GameObject[] stompWave;

    //분신 관련 정의
    public GhostLightHandler ghost;

    // Walk 함수에 필요한 bool 값
    bool isTwoArrow = false;
    bool isDelayingInput = false;
    void Walk()
    {
        // 딜레이 중인 동안 입력되는 인풋은 무시
        if (isDelayingInput)
        {
            return;
        }
        // 두 키 중 하나라도 누르고 있지 않은 경우 isTwoArrow를 false로 만듬 
        if (!Input.GetKey(KeyCode.LeftArrow) || !Input.GetKey(KeyCode.RightArrow))
        {
            isTwoArrow = false;
        }
        // 두 키를 동시에 누르고 있는 상태(isTwoArrow가 true)면 밑의 함수를 실행하지 않음(인풋 무시)
        if (isTwoArrow)
        {
            return;
        }
        // 플레이어가 두 키를 동시에 누르고 있다가 거의 동시에(Delaying의 시간내에) 땠을 때 조금이라도 늦게 땐 쪽의 인풋을 무시하기 위함
        if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyUp(KeyCode.RightArrow)) ||
          (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyUp(KeyCode.LeftArrow)))
        {
            isDelayingInput = true;
            StartCoroutine(Delaying());
            return;
        }
        // 왼쪽 화살표를 누르고 있는 경우
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 달리는 것 인지
            isWalking = true;
            // scale 값을 이용해 캐릭터가 이동 방향을 바라보게 합니다.
            transform.localScale = new Vector3(-Scale, Scale, Scale);
            // 잔상 불빛을 만듭니다.
            //ghost.makeGhost = true;
            // 물체에 왼쪽 방향으로 물리적 힘을 가해줍니다. 즉, 왼쪽으로 이동 시킵니다.
            rig.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);
            // velocity 는 물체의 속도입니다. 일정 속도에 도달하면 더 이상 빨라지지 않게합니다.
            rig.velocity = new Vector2(Mathf.Max(rig.velocity.x, -maxSpeed), rig.velocity.y);
        }
        // 왼쪽 화살표를 누르는 중에 오른쪽 화살표가 눌린 경우
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKeyDown(KeyCode.RightArrow))
        {
            // 정지한 것으로 처리함
            isWalking = false;
            //ghost.makeGhost = false;
            rig.velocity = new Vector3(rig.velocity.normalized.x, rig.velocity.y);
            // 두 키가 동시에 눌리고 있는 상태라 명시
            isTwoArrow = true;
            // 인풋을 무시하는 상태로 만듬
            isDelayingInput = true;
            // 일정 시간 기다렸다가 인풋을 다시 받는 상태로 돌림
            StartCoroutine(Delaying());
            // 밑의 함수를 실행하지 않음
            return;
        }
        // 오른쪽 화살표를 누르고 있는 경우
        if (Input.GetKey(KeyCode.RightArrow))
        {
            isWalking = true;
            transform.localScale = new Vector3(Scale, Scale, Scale);
            //ghost.makeGhost = true;
            rig.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            rig.velocity = new Vector2(Mathf.Min(rig.velocity.x, maxSpeed), rig.velocity.y);
        }
        // 오른쪽 화살표를 누르는 중에 왼쪽 화살표가 눌린 경우
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isWalking = false;
            //ghost.makeGhost = false;
            rig.velocity = new Vector3(rig.velocity.normalized.x, rig.velocity.y);
            isTwoArrow = true;
            isDelayingInput = true;
            StartCoroutine(Delaying());
            return;
        }
        // 이동 키를 뗀 경우
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            // 멈춘것 인지
            isWalking = false;

            // 이동키를 떼면 잔상을 만들지않습니다.
            //ghost.makeGhost = false;

            // x 속도를 줄여 이동 방향으로 아주 살짝만 움직이고 거의 바로 멈추게 합니다.
            rig.velocity = new Vector3(rig.velocity.normalized.x, rig.velocity.y);
            isDelayingInput = true;
            StartCoroutine(Delaying());
        }
    }
    IEnumerator Delaying()
    {
        yield return new WaitForSeconds(0.10f);
        // 위 시간만큼 기다린 다음 인풋을 다시 받는 상태로 만듬
        isDelayingInput = false;
        Debug.Log("딜레이 해제");
    }
    void Dash()
    {
        // 쉬프트를 누르면 달립니다. 최대 속도가 올라가고 가속도가 증가합니다. 바닥에서만 가능합니다.
        if ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && jumpable)
        {
            maxSpeed = Run_maxSpeed;
            moveSpeed = Run_moveSpeed;
        }
        // 쉬프트를 떼면 최대속도가 줄어들고 가속도 또한 감소합니다.
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            maxSpeed = First_maxSpeed;
            moveSpeed = First_moveSpeed;
        }
    }
    void Jump()
    {
        // 스페이스바를 누르면 점프합니다.
        if (Input.GetKeyDown(KeyCode.Space) && jumpable)
        {
            rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    void Stomp()
    {

        // 발구르기 점프중 불가능
        if (Input.GetKeyDown(KeyCode.Z) && jumpable && !isStomping)
        {

            /*
            * 추가 된 부분 ↓
            */

            //wave 개수는 10개 
            int waveNum = 10;

            //wave 0번부터 9번까지 각도 조정
            for (int index = 1; index < waveNum; index++)
            {
                //wave 생성 ; 위치는 shoutpos의 위치에서 생성하고, 각도는 0,0,0으로 시작
                stompWave[index] = Instantiate(wavePrefab, stomppos.position, stomppos.rotation);

                //rigidbody 컴포넌트 불러와서 rigid로 저장
                Rigidbody2D rigid = stompWave[index].GetComponent<Rigidbody2D>();

                // 보는 방향은 180도의 index  /  총 wave의 개수 
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * index / (waveNum)), Mathf.Sin(Mathf.PI * index / (waveNum)));

                // 각도 반전 후 이동
                rigid.AddForce(-dirVec.normalized * 3, ForceMode2D.Impulse);
            }


            /*
             * 추가 된 부분 ↑
             */
            Debug.Log("발구르기 발동");
            // 발구르기 쿨타임 중이라고 명시
            isStomping = true;
            // 발구르기 쿨타임 진행
            StartCoroutine(CoolingStomp());
        }
    }
    IEnumerator CoolingStomp()
    {
        // 발구르기 쿨타임
        yield return new WaitForSeconds(cooltime_Stomp);
        // 위 시간이 지난 후 다시 발구르기 사용 가능한 상태로 변경
        isStomping = false;
        Debug.Log("발구르기 쿨타임 종료");
    }
    void Shout()
    {
        // 소리치기 충전 점프중 가능, 게이지 있음.
        if (Input.GetKeyDown(KeyCode.LeftControl) && !isShouting)
        {
            Gauge += 1;
        }
        // 키업 하면 소리치기. 게이지 표출
        if (Input.GetKeyUp(KeyCode.LeftControl) && !isShouting)
        {
            Gauge = 0;

            /*
             * 추가 된 부분 ↓
             */

            //wave 개수는 10개 
            int waveNum = 10;

            //wave 0번부터 9번까지 각도 조정
            for (int index = 1; index < waveNum; index++)
            {
                //wave 생성 ; 위치는 shoutpos의 위치에서 생성하고, 각도는 0,0,0으로 시작
                shoutWave[index] = Instantiate(wavePrefab, shoutpos.position, shoutpos.rotation);

                //rigidbody 컴포넌트 불러와서 rigid로 저장
                Rigidbody2D rigid = shoutWave[index].GetComponent<Rigidbody2D>();

                // 보는 방향은 180도의 index  /  총 wave의 개수 
                Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * index / (waveNum )), Mathf.Sin(Mathf.PI * index / (waveNum)));

                // wave 이동
                rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
            }

            /*
             * 추가 된 부분 ↑
             */

            Debug.Log("소리치기 발동");
            // 소리치기 쿨타임 중이라고 명시
            isShouting = true;
            // 소리치기 쿨타임 진행
            StartCoroutine(CoolingShout());
        }
    }
    IEnumerator CoolingShout()
    {
        // 소리치기 쿨타임
        yield return new WaitForSeconds(cooltime_Shout);
        // 위 시간이 지난 후 다시 소리치기 사용 가능한 상태로 변경
        isShouting = false;
        Debug.Log("소리치기 쿨타임 종료");
    }
    void MakeGhost()
    {
        // 만약 점프가 불가능한 상황 = 공중에 있는 상황이면
        if (!jumpable)
        {
            // 잔상을 만듭니다.
            ghost.makeGhost = true;
        }
        // 점프가 가능한 상황이고 걷지않는 상황이다
        if (jumpable && !isWalking)
        {
            // 분신을 만들지 않습니다.
            ghost.makeGhost = false;
        }
    }

    private void Awake()
    {
        shoutWave = new GameObject[10];
        stompWave = new GameObject[10];
    }

    private void Start()
    {
        ghost = gameObject.GetComponent<GhostLightHandler>();
        rig = GetComponent<Rigidbody2D>();
        moveSpeed = First_moveSpeed;
        maxSpeed = First_maxSpeed;
        jumpPower = 14;
    }

    void Update()
    {
        //MakeGhost();
        Walk();
        Dash();
        Jump();
        Stomp();
        Shout();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌시
        if (collision.gameObject.tag == "Ground")
        {
            Transform groundpos = collision.gameObject.transform;


            //발구르기 파동 생성 위치를 땅의 아래쪽으로 배치 ((땅의 위치 - 땅의 크기 / 2))
            // 픽셀단위에 따라서 크기와 포지션이 다른거 같아서 일단 임시로 수치 넣어서 사용중
            float pixelfix = 3.125f;
            stomppos.position = new Vector2(transform.position.x, groundpos.position.y - groundpos.localScale.y / 2f / pixelfix - 0.1f);
            Debug.Log(groundpos.position.y - groundpos.localScale.y / 2f / pixelfix - 0.1f);


            // 땅과 충돌시 점프 가능
            jumpable = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 땅과 떨어질시 점프 불가능.
        if (collision.gameObject.tag == "Ground")
        {
            jumpable = false;
        }
    }

}
