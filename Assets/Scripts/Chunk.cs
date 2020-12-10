using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public bool current = false;

    [SerializeField] private Transform deathTriggerTransform = null;
    [SerializeField] private Transform otherChunkTransform = null;
    [SerializeField] private Transform chunkNewPositionTransform = null;
    [SerializeField] private Transform deathTriggerPositionTransform = null;

    [Header("Random respawn")]
    [SerializeField] private Vector2 xRange = Vector2.zero;
    [SerializeField] private Vector2 yRange = Vector2.zero;

    // IDs:
    // 0 - solid platform
    // 1 - broken platform
    // 2 - enemy
    [Header("Field items pool reference")]
    [SerializeField] private List<FieldItem> firstLevelObjects = new List<FieldItem>(3);
    [SerializeField] private List<FieldItem> secondLevelObjects = new List<FieldItem>(3);
    [SerializeField] private List<FieldItem> thirdLevelObjects = new List<FieldItem>(3);

    public void NextChunk()
    {
        current = false;
        otherChunkTransform.position = chunkNewPositionTransform.position;
        Chunk chunk = otherChunkTransform.gameObject.GetComponent<Chunk>();

        chunk.current = true;
        chunk.GenerateObjects();
        deathTriggerTransform.position = deathTriggerPositionTransform.position;
    }

    public void GenerateObjects()
    {
        int first  = GenerateObjectIndex();
        int second = GenerateObjectIndex();
        int third  = GenerateObjectIndex();

        firstLevelObjects[0].gameObject.SetActive(false);
        firstLevelObjects[1].gameObject.SetActive(false);
        firstLevelObjects[2].gameObject.SetActive(false);

        firstLevelObjects[first].gameObject.SetActive(true);
        firstLevelObjects[first].RandomRespawn(xRange, yRange);

        secondLevelObjects[0].gameObject.SetActive(false);
        secondLevelObjects[1].gameObject.SetActive(false);
        secondLevelObjects[2].gameObject.SetActive(false);
        secondLevelObjects[second].gameObject.SetActive(true);
        secondLevelObjects[second].RandomRespawn(xRange, yRange);

        thirdLevelObjects[0].gameObject.SetActive(false);
        thirdLevelObjects[1].gameObject.SetActive(false);
        thirdLevelObjects[2].gameObject.SetActive(false);
        thirdLevelObjects[third].gameObject.SetActive(true);
        thirdLevelObjects[third].RandomRespawn(xRange, yRange);
    }

    private int GenerateObjectIndex()
    {
        int i = Random.Range(0, 100);

        if (i < 5) return 2;
        if (i < 25) return 1;
        return 0;
    }
}
