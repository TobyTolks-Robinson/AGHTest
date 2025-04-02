using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveDistance = 20.0f;
    public float moveDuration = 2.0f;

        private Coroutine moveCoroutine;

    void OnEnable() => InputManager.OnSwipe += HandleSwipe;
    
    void HandleSwipe(Vector2 swipe)
    {
        Vector2 dir;
        string dirName;

        if(Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
        {
            dir = swipe.x > 0 ? Vector2.right : Vector2.left;
            dirName = swipe.x > 0 ? "right" : "left";
        }
        else
        {
            dir = swipe.y > 0 ? Vector2.up : Vector2.down;
            dirName = swipe.y > 0 ? "up" : "down";
        }

        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MovePlayer(dir));
        AudioManager.Instance.PlayNarrtion(dirName);
    }

    IEnumerator MovePlayer(Vector2 dir)
    {
        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(dir * moveDistance);
        float time = 0.0f;

        while (time < moveDuration)
        {
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(start, end, time / moveDuration);
            yield return null;
        }

        transform.position = end;
    }

}
