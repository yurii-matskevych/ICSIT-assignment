using UnityEngine;

public class FieldItem : MonoBehaviour
{
    [SerializeField] private Transform itemTransform = null;
    public void RandomRespawn(Vector2 xRange, Vector2 yRange)
    {
        Vector3 newPosition = itemTransform.localPosition;
        newPosition.x = Random.Range(xRange.x, xRange.y);
        newPosition.y = Random.Range(yRange.x, yRange.y);
        itemTransform.localPosition = newPosition;
    }
}
