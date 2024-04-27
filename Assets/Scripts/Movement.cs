using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : GridObject
{
    public float speed;
    public Tilemap directionTilemap;
    private Rigidbody2D player;
    private SpriteRenderer playerR;
    private Animator myAnimation;

    public Direction direction { get; private set; } = Direction.Down;

    public bool hasControl { get; private set; } = true;

    Scene currentScene;

    Vector2 movement = Vector2.zero;

    private bool _isInteracting;

    private GridObject _holdingObject;



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        currentScene = SceneManager.GetActiveScene();
        player = GetComponent<Rigidbody2D>(); 
        playerR = GetComponentInChildren<SpriteRenderer>();
        myAnimation = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(currentScene.name);
        }

        MoveObject();
    }

    void OnMove(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    void MoveObject()
    {
        if (IsMoving || hasControl == false)
            return;
        
        // If we're holding an object, we should lock the direction of motion.
        if (_isInteracting && _holdingObject != null)
        {
            // No X if we're facing up or down.
            if (movement.x != 0 && direction is Direction.Up or Direction.Down)
            {
                movement.x = 0;
            }
            
            // No Y if we're facing left or right
            if (movement.y != 0 && direction is Direction.Left or Direction.Right)
            {
                movement.y = 0;
            }
        }

        // Store the direction in a temporary variable
        var tmpDirection = direction;
        if (movement.x > 0)
        {
            tmpDirection = Direction.Right;
        }
        else if (movement.x < 0)
        {
            tmpDirection = Direction.Left;
        }
        else if (movement.y > 0)
        {
            tmpDirection = Direction.Up;
        }
        else if (movement.y < 0)
        {
            tmpDirection = Direction.Down;
        }

        if (movement != Vector2.zero)
        {
            // If we're not interacting with anything, move 
            if (!_isInteracting)
            {
                direction = tmpDirection;
                OnStopMoving();
                Move(direction.ToVector3Int());
            }
            else
            {
                // Push if we're facing the same direction
                if (direction == tmpDirection)
                {
                    Push(direction.ToVector3Int());
                }
                // Or pull if we're going the opposite side
                else
                {
                    // To pull an object, we just push the object we're holding in reverse
                    if (_holdingObject != null)
                    {
                        _holdingObject.Push(tmpDirection.ToVector3Int());
                    }
                    // We're not holding anything
                    else
                    {
                        direction = tmpDirection;
                        Push(direction.ToVector3Int());
                    }
                }
            }

            var facingDirection = direction.ToVector3Int();
            myAnimation.SetFloat("Xaxis", facingDirection.x);
            myAnimation.SetFloat("Yaxis", facingDirection.y);
            playerR.flipX = direction == Direction.Left;
        }

        /*
        if (hasControl)
        {
                //player.velocity = movement * speed;

            if(PauseMenu.isPaused == false)
            {
                if (movement == Vector2.zero)
                {
                    direction = Direction.None;
                    myAnimation.SetFloat("Xaxis", 0);
                    myAnimation.SetFloat("Yaxis", 0);
                }

                if (movement.x > 0)
                {
                    direction = Direction.Right;

                    if (transform.childCount <=2)
                        playerR.flipX = false;
                }
                else if (movement.x < 0)
                {
                    direction = Direction.Left;
                    if (transform.childCount <= 2)
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

            gridMove.Move(direction.ToVector3Int());
        }
        */
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

            CustomTileBase tile = other.GetComponent<CustomTileBase>();
            tile.Trigger(this);
        }
        if (other.CompareTag("KillTile"))
        {
            //if (Vector2.Distance(transform.position, other.transform.position) > 0.5f)
                //return;

            CustomTileBase tile = other.GetComponent<CustomTileBase>();
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

    private void OnInteract(InputValue value)
    {
        _isInteracting = value.Get<float>() != 0f;

        if (_isInteracting)
        {
            // Get all the colliders available
            var colliders = GetNeighborColliders(direction.ToVector3Int());
            
            // Exit if no colliders
            if (colliders.Length == 0)
                return;

            // Exit if not a GridObject
            if (colliders[0].TryGetComponent<GridObject>(out var gridObject) == false)
                return;

            // Hold me
            _holdingObject = gridObject;
        }
        else
        {
            _holdingObject = null;
        }
    }

    protected override void OnStopMoving()
    {
        var tile = directionTilemap.GetTile(Cell);

        if (tile is not PlayerPusher pusher)
        {
            // Will trigger the character to stop moving
            myAnimation.SetFloat("Xaxis", movement.x);
            myAnimation.SetFloat("Yaxis", movement.y);
            return;
        }

        if (pusher.Direction == Direction.None)
            return;

        var offsetCell = pusher.Direction.ToVector3Int();
        var nextTile = directionTilemap.GetTile(Cell + offsetCell);
        
        var limit = 20;
        while (limit > 0 && (nextTile == null || nextTile is not PlayerPusher))
        {
            offsetCell += pusher.Direction.ToVector3Int();
            nextTile = directionTilemap.GetTile(Cell + offsetCell);
            limit--;
        }

        Move(offsetCell);
        movement = Vector2.zero;
        
        myAnimation.SetFloat("Xaxis", movement.x);
        myAnimation.SetFloat("Yaxis", movement.y);
    }
}
