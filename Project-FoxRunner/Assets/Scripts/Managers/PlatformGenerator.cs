using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : Singleton<PlatformGenerator>
{
    [SerializeField] private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 28f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelParts;
    [SerializeField] private Transform grid;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private float yLimit;
    [SerializeField] private Vector3 offset;

    private Vector3 lastEndPosition;

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;
        int spawncounter = 2;

        for (int i = 0; i < spawncounter; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        float playerYPosition = player.GetPosition().y;
        float dist = Vector3.Distance(player.GetPosition(), lastEndPosition);

        if (lastEndPosition.y < yLimit && dist < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            SpawnPlatform();
        }

        if (lastEndPosition.y > yLimit)
        {
            // offset.y = Random.Range(-4.9f, -4f);
            //Debug.Log(offset);
            lastEndPosition = new Vector3(lastEndPosition.x, -4.9f, lastEndPosition.z);
        }
    }

    private void SpawnPlatform()
    {
        Transform platformToSpawn = levelParts[Random.Range(0, levelParts.Count)];
        Transform lastlevelPartTransform;
        lastlevelPartTransform = SpawnPlatform(platformToSpawn, lastEndPosition);
        lastEndPosition = lastlevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnPlatform(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity, grid);
        return levelPartTransform;
    }
}
