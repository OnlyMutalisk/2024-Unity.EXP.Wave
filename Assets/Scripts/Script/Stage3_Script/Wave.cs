using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ChangeDirection()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("콜리젼 엔터");
        if (collision.gameObject.tag == "Ground")
        {
            ChangeDirection();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("콜리젼 엑싵");
        if (collision.gameObject.tag == "Ground")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("트리거 엑시트");
        if (collision.gameObject.tag == "Ground")
        {
            CircleCollider2D boxcollider = GetComponent<CircleCollider2D>();
            boxcollider.isTrigger = false;
            Color color = GetComponent<SpriteRenderer>().color;
            color.a = 255;
            gameObject.GetComponent<TrailRenderer>().emitting = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("트리거 엔터");
    }
}
