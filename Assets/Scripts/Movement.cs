using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D player;

    public Direction direction { get; private set; } = Direction.Down;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>(); 
    }

    void OnMove(InputValue value)
    {
        Vector2 movement = value.Get<Vector2>();
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // Vector2 movement = new Vector2(horizontal, vertical);
        // movement.Normalize();

        player.velocity = movement * speed;

        if (movement == Vector2.zero)
        {
            return;
        }

        if (movement.x > 0)
        {
            direction = Direction.Right;
        }
        else if (movement.x < 0)
        {
            direction = Direction.Left;
        }

        if (movement.y > 0)
        {
            direction = Direction.Up;
        }
        else if (movement.y < 0)
        {
            direction = Direction.Down;
        }
    }
}
