using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

public class BackgroundScroller : MonoBehaviour
{

    public float speed = 2f;

    [SerializeField] private Renderer backgroundRenderer;

   
    private void Update()
    {
        backgroundRenderer.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
    

}
