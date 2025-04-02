using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCamera : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2.0f;
    public Vector2 bounds = new Vector2(4.0f, 2.0f);

    private void LateUpdate()
    {
        if (!target) return;

        Vector3 diff = target.position - transform.position;
        Vector3 newPos = transform.position;

        if (Mathf.Abs(diff.x) > bounds.x)
            newPos.x = Mathf.Lerp(transform.position.x, target.position.x, Time.deltaTime * followSpeed);

        if (Mathf.Abs(diff.y) > bounds.y)
            newPos.y = Mathf.Lerp(transform.position.y, target.position.y, Time.deltaTime * followSpeed);

        newPos.z = transform.position.z;
        transform.position = newPos;
    }
}
