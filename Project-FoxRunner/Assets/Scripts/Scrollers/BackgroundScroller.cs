using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class BackgroundScroller : MonoBehaviour
{

    public float speed = 2f;

    private Vector3 StartPosition;

    private void Start()
    {
        StartPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if(Camera.main.transform.position.x > -3f)
        {
            transform.position = StartPosition;
        }
    }
    

}
