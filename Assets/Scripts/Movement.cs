using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D player;
    private SpriteRenderer playerR;
    private Animator myAnimation;

    public Direction direction { get; private set; } = Direction.Down;

    public bool hasControl { get; private set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>(); 
        playerR = GetComponent<SpriteRenderer>();
        myAnimation = GetComponent<Animator>();
    }

    void OnMove(InputValue value)
    {

        Vector2 movement = value.Get<Vector2>();
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");

        // Vector2 movement = new Vector2(horizontal, vertical);
        // movement.Normalize();

        if (hasControl)
        {
            player.velocity = movement * speed;

        if (movement == Vector2.zero)
        {
            myAnimation.SetFloat("Xaxis", 0);
            myAnimation.SetFloat("Yaxis", 0);
            return;
        }

        if (movement.x > 0)
        {
            direction = Direction.Right;
            playerR.flipX = false;
        }
        else if (movement.x < 0)
        {
            direction = Direction.Left;
            playerR.flipX = true;
        }

        if (movement.y > 0)
        {
            direction = Direction.Up;
        }
        else if (movement.y < 0)
        {
            direction = Direction.Down;
        }
        myAnimation.SetFloat("Xaxis", movement.x);
        myAnimation.SetFloat("Yaxis", movement.y);

        }

        //Animator.SetInt("Direction", (int)direction);

    }

    public void Push(Direction dir)
    {
        player.velocity = dir.ToVector2() * speed;
        hasControl = false;
    }

    public void Stop()
    {
        hasControl = true;
        player.velocity = Vector2.zero;
    }

    public void Kill()
    {
        StartCoroutine(Dead());
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("ActionTile"))
        {
            if (Vector2.Distance(transform.position, other.transform.position) > 1f)
                return;

            TileBase tile = other.GetComponent<TileBase>();
            tile.Trigger(this);
        }
        if (other.CompareTag("KillTile"))
        {
            //if (Vector2.Distance(transform.position, other.transform.position) > 0.5f)
                //return;

            TileBase tile = other.GetComponent<TileBase>();
            tile.Trigger(this);
        }
    }

    IEnumerator Dead()
    {   
        player.velocity = Vector2.zero;
        hasControl = false;
        myAnimation.SetBool("isDead", true);
        yield return new WaitForSeconds(1.35f);
        Destroy(gameObject);
        myAnimation.SetBool("isDead", false);
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        hasControl = true;
    }




}
