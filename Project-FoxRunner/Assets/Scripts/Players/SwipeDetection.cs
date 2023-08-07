using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{

   // [SerializeField] private float minimumDistance  = 0.2f;
   // [SerializeField] private float maximumTime  = 1f;

    private InputManager inputManager;
    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    private void Awake()
    {
        inputManager = InputManager.Instance;
    }

    private void OnEnable()
    {
    //    inputManager.OnSwipeLeft += OnSwipeLeft;
    //    inputManager.OnSwipeRight += OnSwipeRight;
    //    inputManager.OnSwipeUp += OnSwipeUp;
    //    inputManager.OnSwipeDown += OnSwipeDown;
    }

    private void OnDisable()
    {
    //    inputManager.OnSwipeLeft -= OnSwipeLeft;
    //    inputManager.OnSwipeRight -= OnSwipeRight;
    //    inputManager.OnSwipeUp -= OnSwipeUp;
    //    inputManager.OnSwipeDown -= OnSwipeDown;
    }

    private void OnSwipeLeft() => Debug.Log("Left swipe detected");
    private void OnSwipeRight() => Debug.Log("Right swipe detected");
    private void OnSwipeUp() => Debug.Log("Up swipe detected");
    private void OnSwipeDown() => Debug.Log("Down swipe detected");

}
