using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDeath : Singleton<TrapDeath>
{
    public bool touchedSpike { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
           touchedSpike = true;
        }
    }
}
