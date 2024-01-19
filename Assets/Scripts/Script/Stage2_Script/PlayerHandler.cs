using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
    Rigidbody2D rig;
    // 시간관련 타이머 설정
    public UnityEngine.UI.Text text_Timer; // 쿨타임계산
    private float time_current;
    private float time_Max = 2.8f;
    private bool isEnded_foot = true;
    private bool isFooting = false;
    private bool isWalking = false;

    // 움직임 관련 정의
    public float moveSpeed;
    public float maxSpeed;
    public float jumpPower;
    public float Scale = 3f; // 캐릭터 크기조정
    public bool isJump = false; 
    float First_moveSpeed = 3; // 
    float First_maxSpeed = 5;   
    float Run_moveSpeed = 6;
    float Run_maxSpeed = 10;
    float Gauge = 0;
  

    //분신 관련 정의
    public GhostLightHandler ghost;

    private void Start()
    {
        ghost = this.gameObject.GetComponent<GhostLightHandler>();
        rig = GetComponent<Rigidbody2D>();
        moveSpeed = First_moveSpeed;
        maxSpeed = First_maxSpeed;
        jumpPower = 14;
}
    void Update()
    {
        // 왼쪽 화살표를 누르고 있는 경우
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // 달리는 것 인지
            isWalking = true;

            // 잔상 불빛을 만듭니다.
            ghost.makeGhost = true; 

            // 물체에 왼쪽 방향으로 물리적 힘을 가해줍니다. 즉, 왼쪽으로 이동 시킵니다.
            rig.AddForce(Vector2.left * moveSpeed, ForceMode2D.Impulse);

            // velocity 는 물체의 속도입니다. 일정 속도에 도달하면 더 이상 빨라지지 않게합니다.
            rig.velocity = new Vector2(Mathf.Max(rig.velocity.x, -maxSpeed), rig.velocity.y);

            // scale 값을 이용해 캐릭터가 이동 방향을 바라보게 합니다.
            transform.localScale = new Vector3(-Scale, Scale, Scale);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) // 오른쪽 화살표를 누르고 있는 경우
        {
            // 달리는 것 인지
            isWalking = true;

            // 잔상 불빛을 만듭니다
            ghost.makeGhost = true;

            rig.AddForce(Vector2.right * moveSpeed, ForceMode2D.Impulse);
            rig.velocity = new Vector2(Mathf.Min(rig.velocity.x, maxSpeed), rig.velocity.y);
            transform.localScale = new Vector3(Scale, Scale, Scale);
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow)) // 이동 키를 뗀 경우
        {
            // 멈춘것 인지
            isWalking = false;

            // 이동키를 떼면 잔상을 만들지않습니다.
            ghost.makeGhost = false;
            
            // x 속도를 줄여 이동 방향으로 아주 살짝만 움직이고 거의 바로 멈추게 합니다.
            rig.velocity = new Vector3(rig.velocity.normalized.x, rig.velocity.y);
        }



        // 스페이스바를 누르면 점프합니다.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJump)
                rig.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
        
        // 쉬프트를 누르면 달립니다. 최대 속도가 올라가고 가속도가 증가합니다. 바닥에서만 가능합니다.
        if ((Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.RightShift))&&isJump)
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
        // 공중에서 쉬프트를 누른상태로 바닥에 도착하면 바로 달리게끔하는 기능을 만들고싶으나 아직 바닥에 닿아도 바로 달리지않음. 한번 더 눌러야함. 추가필요할듯.

   


        // 발구르기 점프중 불가능
        if ((Input.GetKeyDown(KeyCode.Z)&&isJump)&&isEnded_foot) // isFoot 쿨타임 계산
        {
            time_current = time_Max;
            Debug.Log("발구르기 발동");
            isEnded_foot = false;
            isFooting = true;
        } // 일정시간까진 불가능하게하는 메커니즘 필요
        if((0<time_current)&&isFooting) // 발구르기 행동 진입
        {
            time_current -= Time.deltaTime; //쿨타임 계산
            Debug.Log(time_current);
        }
        else if(isFooting)  // 시간 모두 떨어지면
        {
            Debug.Log("쿨타임종료");  // 다시 사용가능
            isFooting = false;
            isEnded_foot = true;
        }

        if(!isJump) // 만약 점프가 불가능한 상황 = 공중에 있는 상황이면
        {
            ghost.makeGhost = true;// 잔상을 만듭니다.
        }
        else if(isJump&&!isWalking ) // 점프가 가능한 상황이고 걷지않는 상황이다
        {
            ghost.makeGhost = false; // 분신을 만들지 않습니다.
        }




        // 소리치기 충전 점프중 가능, 게이지 있음.
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Gauge += 1;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl)) // 키업 하면 소리치기. 게이지 표출
        {

            Gauge = 0;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌시 점프가능
        if(collision.gameObject.tag == "Ground")
        {
            isJump = true;
        }
    }

    // 땅과 떨어질시 점프 불가능.

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }


}
