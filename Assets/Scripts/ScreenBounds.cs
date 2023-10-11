using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private BoxCollider2D _boxCollider;

    public UnityEvent<Collider2D> ExitTriggerFired;

    [SerializeField] private float _teleportOffset = 0.2f;
    [SerializeField] private float _cornerOffset = 1;

    private void Awake()
    {
        //_camera.transform.localPosition = Vector3.one;
        _boxCollider = GetComponent<BoxCollider2D>();
        _boxCollider.isTrigger = true;
    }
    private void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundsSize();
    }
    public void UpdateBoundsSize()
    {
        float ySize = _camera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(ySize * _camera.aspect, ySize);
        _boxCollider.size = boxColliderSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        ExitTriggerFired?.Invoke(collision);
    }
    public bool IsOutOfBoundsX(Vector3 worldPos)
    {
        return
            Mathf.Abs(worldPos.x) > Mathf.Abs(_boxCollider.bounds.min.x)/* ||
            Mathf.Abs(worldPos.y) > Mathf.Abs(_boxCollider.bounds.min.y)*/;
    }
    public bool IsOutOfBoundsY(Vector3 worldPos)
    {
        return
            Mathf.Abs(worldPos.y) > Mathf.Abs(_boxCollider.bounds.min.y);
    }
    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult =
            Mathf.Abs(worldPosition.x) > (Mathf.Abs(_boxCollider.bounds.min.x) - _cornerOffset);
        bool yBoundResult =
            Mathf.Abs(worldPosition.y) > (Mathf.Abs(_boxCollider.bounds.min.y) - _cornerOffset);

        Vector2 signWorldPosition =
            new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));
        /*
        if (xBoundResult && yBoundResult)
        {
            return Vector2.Scale(worldPosition, Vector2.one * -1)
                + Vector2.Scale(new Vector2(_teleportOffset, _teleportOffset),
                signWorldPosition);
        }*/
        if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y)
                + new Vector2(_teleportOffset * signWorldPosition.x, _teleportOffset);
        }
        else if (yBoundResult)
        {
            return worldPosition;
            return new Vector2(worldPosition.x, worldPosition.y * -1)
                + new Vector2(_teleportOffset, _teleportOffset * signWorldPosition.y);
        }
        else
        {
            return worldPosition;
        }
    }
}
