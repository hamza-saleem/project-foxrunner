using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlatformGenerator : Singleton<PlatformGenerator>
{
   [SerializeField] private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 28f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelParts;
    [SerializeField] private Transform grid;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private float yLimit = 6f;

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
        float playerYPosition = player.GetPosition().y;

        // Check if the player has not reached the Y position limit.
        if (playerYPosition < yLimit)
        {
            float dist = Vector3.Distance(player.GetPosition(), lastEndPosition);

            if (dist < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
            {
                SpawnPlatform();
            }
        }
    }

    private void SpawnPlatform()
    {
        Transform platformToSpawn = levelParts[Random.Range(0, levelParts.Count)];
        Transform lastlevelPartTransform;

        if (lastEndPosition.y < 6)
        {
            lastlevelPartTransform = SpawnPlatform(platformToSpawn, lastEndPosition);
            lastEndPosition = lastlevelPartTransform.Find("EndPosition").position;

        }
    }
    private Transform SpawnPlatform(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity, grid);
        return levelPartTransform;
    }
}
