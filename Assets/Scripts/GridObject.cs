using System.Linq;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    // The minimum distance we should be in to accept more inputs.
    private const float MinDistanceFromTarget = 0.1f;
    private const float MovementSpeed = 4.25f;
    private bool reachedCell = true;

    StepsCount steps;
    
    // The Grid Parent available to all objects
    protected static Grid GridParent { get; private set; }

    // We need a field to store the cell in
    private Vector3Int _cell;
    
    /// <summary>
    /// The object's cell position.
    /// </summary>
    public Vector3Int Cell
    {
        get => _cell;
        set
        {
            _cell = value;
            _targetPosition = GridParent.WorldToCell(_cell);
        }
    }

    // The attached collider
    private Collider2D _collider;

    // The position this object should be in
    private Vector3 _targetPosition;
    
    // Checks if the object is still in motion
    protected bool IsMoving => Vector3.Distance(transform.position, _targetPosition) > MinDistanceFromTarget;

    /// <summary>
    /// Extendable function which helps with setup.
    /// </summary>
    protected virtual void Awake()
    {
        if (GridParent == null)
        {
            GridParent = FindObjectOfType<Grid>();
        }
        
        _collider = GetComponent<Collider2D>();

        steps = GetComponent<StepsCount>();
    }
    /// <summary>
    /// Extendable function which helps with initialization.
    /// </summary>
    protected virtual void Start()
    {
        SnapToGrid();
    }
    
    protected virtual void FixedUpdate()
    {
        // The character is no longer moving (they're close to the threshold).
        if (!IsMoving)
        {
            transform.position = _targetPosition;
            if (reachedCell == false)
            {
                OnStopMoving();
                reachedCell = true;
            }
            return;
        }

        reachedCell = false;
        transform.position = Vector3.MoveTowards(transform.position, _targetPosition, MovementSpeed * Time.deltaTime);
    }

    protected virtual void OnStopMoving()
    {

    }
    
    /// <summary>
    /// Returns a collider in the neighboring cell.
    /// </summary>
    /// <param name="direction">The direction to check in.</param>
    protected Collider2D[] GetNeighborColliders(Vector3Int direction)
    {
        // This will offset the size of our collision check by a few units
        const float reduction = 0.1f;

        // Start from the same cell
        var neighbor = Cell;
        
        // We'll calculate according to the size of the collider.
        var size = new Vector2(
            direction.y != 0 ? _collider.bounds.size.x - reduction : 0.1f,
            direction.x != 0 ? _collider.bounds.size.y - reduction : 0.1f);
        
        // The colliders we find will be stored here
        Collider2D[] others;
        
        // This loop allows us to check if we're also touching objects that
        // are larger than a cell.
        // Also - do-while loops will run at least once, so we don't add
        // the direction beforehand!
        do
        {
            // The neighboring cell
            neighbor += direction;

            // We need to find the center point of calculation, taking into account
            // the collider size and offset
            var point = GridParent.CellToWorld(neighbor);
            if (direction.x != 0) point.y = _collider.bounds.center.y;
            if (direction.y != 0) point.x = _collider.bounds.center.x;
            
            //steps.Invoke("TakeAStep",0);
            
            others = Physics2D.OverlapBoxAll(point, size, 0).Where(col => !col.isTrigger).ToArray();
        } while (others.Any(col => col == _collider));

        return others;
    }

    /// <summary>
    /// Moves in a direction and stops if there are walls.
    /// </summary>
    /// <param name="direction">The direction of movement.</param>
    public void Move(Vector3Int direction)
    {
        // Stop if we're already moving
        if (IsMoving)
            return;
        
        // Stop if we've bumped into a neighbor
        var neighbors = GetNeighborColliders(direction);
        if (neighbors.Length > 0)
            return;

        SetCell(direction);
    }

    /// <summary>
    /// Attempts to push the object in a direction.
    /// </summary>
    /// <param name="direction">The direction of movement.</param>
    public bool Push(Vector3Int direction)
    {
        // Stop if we're already moving
        if (IsMoving)
            return false;
        
        // Get the neighboring collider
        var neighbors = GetNeighborColliders(direction);

        // There is no neighbor, so we can move
        if (neighbors.Length == 0)
        {
            SetCell(direction);
            return true;
        }
        
        // We'll start by assuming that we can move
        var canPush = true;
        
        // Stop here if we can't move one object
        foreach (var neighbor in neighbors)
        {
            // We can stop the loop here if we can't push anything
            if (neighbor.TryGetComponent<GridObject>(out var gridObject) == false)
            {
                canPush = false;
                break;
            }
            
            // If the next object can't push its neighbor, we'll stop here as well
            if (!gridObject.Push(direction))
            {
                canPush = false;
                break;
            }
        }
        
        // If we can move, we should
        if (canPush)
        {
            SetCell(direction);
        }

        // Return the outcome
        return canPush;
    }

    /// <summary>
    /// Sets the object's target cell.
    /// </summary>
    /// <param name="direction">The direction.</param>
    protected void SetCell(Vector3Int direction)
    {
        Cell += direction;
    }
    
    /// <summary>
    /// Snaps the object to the grid.
    /// </summary>
    private void SnapToGrid()
    {
        _cell = GridParent.WorldToCell(transform.position);
        _targetPosition = transform.position = GridParent.CellToWorld(_cell);
    }

    public void Teleport(Vector3Int destination)
    {
        Cell = destination;
        transform.position = _targetPosition;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 50), IsMoving.ToString());
    }
    
}