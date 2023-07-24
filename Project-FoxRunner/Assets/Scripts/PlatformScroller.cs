using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScroller : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
    }
}
