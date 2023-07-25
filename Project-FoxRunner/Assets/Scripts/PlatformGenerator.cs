using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
   [SerializeField] private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 28f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelParts;
    [SerializeField] private Transform grid;
    [SerializeField] private PlayerMovement player;

    private Vector3 lastEndPosition;
    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        int spawncounter = 3;

        for(int i = 0; i < spawncounter; i++)
        {
           SpawnPlatform();
        }

    }

    private void Update()
    {
        float dist = Vector3.Distance(player.GetPosition(), lastEndPosition);

        Debug.Log(dist);
        if ( dist < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnPlatform();
        }
    }

    private void SpawnPlatform()
    {
        Transform platformToSpawn = levelParts[Random.Range(0, levelParts.Count)];
        Transform lastlevelPartTransform = SpawnPlatform(platformToSpawn, lastEndPosition);
        lastEndPosition = lastlevelPartTransform.Find("EndPosition").position;
    }
    private Transform SpawnPlatform(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity, grid);
        return levelPartTransform;
    }
}
