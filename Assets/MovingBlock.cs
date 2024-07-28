using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    [SerializeField] private BoxCollider2D movementArea;
    [SerializeField] private float boxSpeed = 2.0f;
    private Vector3 minBounds;
    private Vector3 maxBounds;
    [SerializeField] bool movingRight = true;
    void Start()
    {
        if (movementArea != null)
        {
            // 박스 콜라이더의 범위 계산
            minBounds = movementArea.bounds.min;
            maxBounds = movementArea.bounds.max;
            transform.position = minBounds;
        }
        else
        {
            Debug.LogError("Movement area is not set.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlatform();
    }
    private void MovePlatform()
    {
        if (movementArea == null)
            return;

        if (movingRight) // 오른쪽이동
        {
            transform.position += Vector3.right * boxSpeed * Time.deltaTime;
            if (transform.position.x >= maxBounds.x)
                movingRight = false;
        }
        else //왼족이동
        {
            transform.position += Vector3.left * boxSpeed * Time.deltaTime;
            if (transform.position.x <= minBounds.x)
                movingRight = true;
        }
    }
}
