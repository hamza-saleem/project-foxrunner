using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

public class NewInputManager : MonoBehaviour
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;


    #endregion



    [SerializeField] private float minimumDistance = 0.2f;
    [SerializeField] private float maximumTime = 1f;
    [SerializeField, Range(0f, 1f)] private float directionThreshold = 0.9f;

    private Vector2 startPosition, endPosition;
    private float startTime, endTime;

    private Camera mainCamera;
    [SerializeField] private PlayerMovement player;
    private void Awake()
    {
        mainCamera = Camera.main;
    }
    protected void OnEnable()
    {

      //  TouchSimulation.Enable();
        EnhancedTouchSupport.Enable();
        OnStartTouch += SwipeStart;
        OnEndTouch += SwipeEnd;
    }

    protected void OnDisable()
    {
      //  TouchSimulation.Disable();
        EnhancedTouchSupport.Disable();
        OnStartTouch -= SwipeStart;
        OnEndTouch -= SwipeEnd;
    }


    private void Update()
    {
        foreach (Touch touch in Touch.activeTouches)
        {
            if (touch.phase == TouchPhase.Began)
            {
               // Debug.Log("Started!" + touch.startScreenPosition);
                StartTouchPrimary(touch);
            }
            
            if (touch.phase == TouchPhase.Ended)
            { 
               // Debug.Log("Ended" + touch.screenPosition);
                EndTouchPrimary(touch);
            }

        }
    }

    private void StartTouchPrimary(Touch touch) { if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera,touch.startScreenPosition), (float)touch.startTime); }
    private void EndTouchPrimary(Touch touch) { if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, touch.screenPosition), (float)touch.time); }


    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance && (endTime - startTime) < maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.y).normalized;
            SwipeDirection(direction2D);
        }
    }
    private void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold) 
            player.Jump();

        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
        }
    }

}
