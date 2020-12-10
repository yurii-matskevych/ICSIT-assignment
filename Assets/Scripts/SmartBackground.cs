using UnityEngine;

public class SmartBackground : MonoBehaviour
{
    [SerializeField] private float backgroundRadius = 0f;
    [SerializeField] private RectTransform bgRectTransform = null;

    [Header("Delta")]
    [SerializeField] private PlayerMovement playerMovement = null;
    [SerializeField] private Vector2 deltaCoefficient = Vector2.one;

    private void Update()
    {
        MoveBackground(playerMovement.GetDeltaSpeed());
    }
    private void MoveBackground(Vector2 delta)
    {
        Vector3 newPosition = bgRectTransform.localPosition;
        if (newPosition.magnitude > backgroundRadius)
            newPosition *= -1;

        newPosition.x -= delta.x * deltaCoefficient.x;
        newPosition.y -= delta.y * deltaCoefficient.y;

        bgRectTransform.localPosition = newPosition;
    }
}
