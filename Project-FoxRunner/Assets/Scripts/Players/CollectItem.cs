using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        { 
            gameObject.SetActive(false);
            GameManager.Instance.CollectItem();
        }
    }
}
