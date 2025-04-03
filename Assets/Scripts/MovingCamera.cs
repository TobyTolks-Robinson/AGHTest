using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    public float moveSpeed = 5.0f; 

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (!target || !cam) return;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        Vector3 camPos = transform.position;
        Vector3 targetPos = target.position;

        float leftBound = camPos.x - halfWidth;
        float rightBound = camPos.x + halfWidth;
        float bottomBound = camPos.y - halfHeight;
        float topBound = camPos.y + halfHeight;

        bool isOutsideX = targetPos.x < leftBound || targetPos.x > rightBound;
        bool isOutsideY = targetPos.y < bottomBound || targetPos.y > topBound;

        if (isOutsideX || isOutsideY)
        {
            Vector3 desiredPosition = new Vector3(targetPos.x, targetPos.y, camPos.z);
            transform.position = new Vector3(targetPos.x, targetPos.y, camPos.z);
        }
    }
}