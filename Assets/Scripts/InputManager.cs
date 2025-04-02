using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class InputManager : MonoBehaviour
{
    public static event Action<Vector2> OnSwipe;

    private Vector2 swipeStart;

    void Update()
    {
        if (Time.timeScale == 0) return;

        // Mouse
        if (Input.GetMouseButtonDown(0))
            swipeStart = Input.mousePosition;

        if (Input.GetMouseButtonUp(0))
            DetectSwipe((Vector2)Input.mousePosition - swipeStart);


        // Touch
        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
                swipeStart = touch.position;

            if (touch.phase == TouchPhase.Ended)
                DetectSwipe(touch.position - swipeStart);
        }
    }

    void DetectSwipe(Vector2 delta)
    {
        if (delta.magnitude < 50f) return;
        OnSwipe?.Invoke(delta);
    }
}
