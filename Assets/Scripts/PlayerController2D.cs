using UnityEngine;

/// <summary>
/// WASD top-down movement using Rigidbody2D. Attach to Player.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;

    private Rigidbody2D _rb;
    private Vector2 _input;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");
        _input = Vector2.ClampMagnitude(_input, 1f);
    }

    private void FixedUpdate()
    {
        _rb.velocity = _input * moveSpeed;
    }
}