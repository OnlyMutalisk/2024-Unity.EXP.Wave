using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* BoundLight 스크립트는 BoundLight 프리팹이 가지고 있는 스크립트이며 
 * wave 스크립트에서 받은 콜라이더를 따라 시계 또는 반시계 방향으로 자동으로 이동하게 만드는 스크립트이다.
 * 
 * 현재 moveSpeed 적용이 안되어 직접 넣어놓은 상태
 */
public class BoundLight : MonoBehaviour
{
    public Collider2D platformCollider;  // 발판의 Collider(wave 스크립트에서 받아올 예정)
    public float moveSpeed = 0.5f; // 미적용
    public bool isclockwise = true; // 시계방향인지(wave 스크립트에서 받아올 예정)

    private Vector2[] waypoints; //이동하는 목표 지점
    private int currentWaypointIndex = 0; // 처음 이동할 지점

    public float lifeTime = 2f;

    bool timetodestroy = false;

    void Start()
    {
        //시작과 동시에 destroy 대기
        StartCoroutine(WaitingDestroy(lifeTime)); 

        if (platformCollider != null)
        {
            // Collider의 경계 얻기
            if(isclockwise == true)
            {
                waypoints = GetClockWiseWaypoints(platformCollider);
            }
            else
            {
                waypoints = GetCounterClockWiseWaypoints(platformCollider);
            }
        }
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0)
            return;

        // 몹을 현재 지점에서 다음 지점으로 이동 
        if (timetodestroy == false)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, waypoints[currentWaypointIndex], 1f * Time.deltaTime); //원래 여기에 1f 대신 moveSpeed 들어가야함
        }
        

        // 다음 지점에 도달하면 다음 지점으로 이동
        if (Vector2.Distance(this.transform.position, waypoints[currentWaypointIndex]) < 0.01f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }


    IEnumerator WaitingDestroy(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        timetodestroy = true; // 움직임을 멈춤

        yield return new WaitForSeconds(1f); // trail renderer가 도착하기까지 대기

        Destroy(gameObject); 
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Collider의 경계를 얻어와서 경로로 반환하는 함수
    private Vector2[] GetClockWiseWaypoints(Collider2D collider)
    {
        Vector2[] waypoints = new Vector2[4];

        // Collider의 경계 얻어오기
        Bounds bounds = collider.bounds;
        waypoints[0] = new Vector2(bounds.min.x, bounds.max.y);
        waypoints[1] = new Vector2(bounds.max.x, bounds.max.y);
        waypoints[2] = new Vector2(bounds.max.x, bounds.min.y);
        waypoints[3] = new Vector2(bounds.min.x, bounds.min.y);

        //가장 가까운 점의 인덱스 확인
        float minDistance = 100f;
        for(int i = 0; i < 4; i++)
        {
            if(Vector2.Distance(waypoints[i], transform.position) < minDistance)
            {
                minDistance = Vector2.Distance(waypoints[i], transform.position);
                currentWaypointIndex = i;
            }
        }
        
        // 가장 가까운 점이 시계방향인지 확인
        // 그리고 벽에 딱 붙게 위치 조정 (currentWaypointIndex == 0 인 지점만이 적용되어 있음 수정 필요)
        if(currentWaypointIndex == 0) // 1. 왼쪽 위 꼭짓점일 경우
        {
            // 왼쪽 위 꼭짓점보다 오른쪽에 있을경우
            if (transform.position.x - waypoints[currentWaypointIndex].x > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(transform.position.x, bounds.max.y);

                // 다음 인덱스가 시계방향
                currentWaypointIndex += 1;
            }
            else // 왼쪽 면에 있을 시
            {
                //현재 위치의 x를 bound에 고정
                transform.position = new Vector2(bounds.min.x, transform.position.y);
            }
        }else if(currentWaypointIndex == 1) // 2. 오른쪽 위 꼭짓점일 경우
        {
            // 오른쪽 위 꼭짓점보다 y가 낮을 경우
            if (waypoints[currentWaypointIndex].y - transform.position.y > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(bounds.max.x, transform.position.y);

                currentWaypointIndex += 1;
            }
        }
        else if (currentWaypointIndex == 2) // 3. 오른쪽 아래 꼭짓점일 경우
        {
            if (waypoints[currentWaypointIndex].x - transform.position.x > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(transform.position.x, bounds.min.y);

                currentWaypointIndex += 1;
            }
        }
        else if (currentWaypointIndex == 3) // 4. 왼쪽 아래 꼭짓점일 경우
        {
            if (transform.position.y - waypoints[currentWaypointIndex].y > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(bounds.min.x, transform.position.y);

                currentWaypointIndex = 0;
            }
        }


        return waypoints;
    }

    private Vector2[] GetCounterClockWiseWaypoints(Collider2D collider)
    {
        Vector2[] waypoints = new Vector2[4];

        // Collider의 경계 얻어오기
        Bounds bounds = collider.bounds;
        waypoints[0] = new Vector2(bounds.min.x, bounds.max.y);
        waypoints[3] = new Vector2(bounds.max.x, bounds.max.y);
        waypoints[2] = new Vector2(bounds.max.x, bounds.min.y);
        waypoints[1] = new Vector2(bounds.min.x, bounds.min.y);


        float minDistance = 100f;
        for (int i = 0; i < 4; i++)
        {
            if (Vector2.Distance(waypoints[i], transform.position) < minDistance)
            {
                minDistance = Vector2.Distance(waypoints[i], transform.position);
                currentWaypointIndex = i;
            }
        }

        // 가장 가까운 점이 반시계방향인지 확인
        // 그리고 벽에 딱 붙게 위치 조정
        if (currentWaypointIndex == 1) // 1. 왼쪽 아래 꼭짓점일 경우
        {
            // 왼쪽 아래 꼭짓점보다 오른쪽에 있을경우
            if (transform.position.x - waypoints[currentWaypointIndex].x > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(transform.position.x, bounds.min.y);

                // 오른쪽 위 꼭짓점이 목표
                currentWaypointIndex += 1;
            }
            else // 왼쪽 면에 았음
            {
                //현재 위치의 x를 bound에 고정
                transform.position = new Vector2(bounds.min.x, transform.position.y);
            }
        }
        else if (currentWaypointIndex == 0) // 2. 왼쪽 위 꼭짓점일 경우
        {
            // 오른쪽 위 꼭짓점보다 y가 낮을 경우
            if (waypoints[currentWaypointIndex].y - transform.position.y > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(bounds.min.x, transform.position.y);

                currentWaypointIndex += 1;
            }
        }
        else if (currentWaypointIndex == 3) // 3. 오른쪽 위 꼭짓점일 경우
        {
            if (waypoints[currentWaypointIndex].x - transform.position.x > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(transform.position.x, bounds.max.y);

                currentWaypointIndex = 0;
            }
        }
        else if (currentWaypointIndex == 2) // 4. 오른쪽 아래 꼭짓점일 경우
        {
            if (transform.position.y - waypoints[currentWaypointIndex].y > 0.1f)
            {
                //현재 위치의 y를 bound에 고정
                transform.position = new Vector2(bounds.max.x, transform.position.y);

                currentWaypointIndex += 1;
            }
        }


        return waypoints;
    }
}