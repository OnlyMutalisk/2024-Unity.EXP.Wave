using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyWave", 3);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void DestroyWave()
    {
        StartCoroutine(WaitingTrail());
    }
    IEnumerator WaitingTrail()
    {
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;

        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void ChangeDirection()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 땅과 충돌시 점프가능
        if (collision.gameObject.tag == "Ground")
        {
            ChangeDirection();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 땅과 떨어질시 점프 불가능.
        if (collision.gameObject.tag == "Ground")
        {
            
        }
    }
}
