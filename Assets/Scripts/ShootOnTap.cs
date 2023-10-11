using System.Collections;
using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class ShootOnTap : MonoBehaviour
{
    [SerializeField] private float _shootForce;
    [SerializeField] private float _angularVelocity;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ScreenBounds _screenBounds;
    [SerializeField] SpriteRenderer _gunshorSpriteRenderer;
    bool _isTouched = false;

    void Start()
    {
        _rigidbody.AddTorque(_angularVelocity, ForceMode2D.Force);
    }

    void FixedUpdate()
    {
        if (_screenBounds.IsOutOfBoundsY(transform.position))
        {
            _rigidbody.gravityScale = 1;
        }
        else
        {
            _rigidbody.gravityScale = 0.3f;
        }
        if (_screenBounds.IsOutOfBoundsX(transform.position))
        {
            Vector2 newPos = _screenBounds.CalculateWrappedPosition(transform.position);
            _rigidbody.MovePosition(newPos);
        }
        if (Input.touchCount > 0 && !_isTouched)
        {
            _isTouched = true;
            _rigidbody.AddForce(transform.right * -_shootForce, ForceMode2D.Impulse);
            StartCoroutine(DrawGunshotEffect());
        }
        if (Input.touchCount == 0)
        {
            _isTouched = false;
        }
    }
    IEnumerator DrawGunshotEffect()
    {
        _gunshorSpriteRenderer.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        _gunshorSpriteRenderer.gameObject.SetActive(false);
    }
}
