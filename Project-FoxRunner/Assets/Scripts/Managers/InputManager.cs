using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
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

    private Mobile playerControls;
    private PlayerMovement player;

    private Camera mainCamera;


    private void Awake()
    {
        playerControls = new Mobile();
        player = PlayerMovement.Instance;
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
       // playerControls.Enable();
        OnStartTouch += SwipeStart;
        OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {

        playerControls.Disable();
        OnStartTouch -= SwipeStart;
        OnEndTouch -= SwipeEnd;
    }

    private void Start()
    {
        playerControls.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerControls.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    public void StartControls()
    {
        playerControls.Enable();
    }


    private Vector2 ScreenPosition() { return playerControls.Touch.PrimaryPosition.ReadValue<Vector2>(); }

    private void StartTouchPrimary(InputAction.CallbackContext ctx) { if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(mainCamera, ScreenPosition()), (float)ctx.startTime) ; }
    private void EndTouchPrimary(InputAction.CallbackContext ctx) { if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(mainCamera, ScreenPosition()), (float)ctx.time); }

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