using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class GridMovement : MonoBehaviour
{
    private static Grid grid;
    Vector2 input = new();
    public Vector3Int cellPosition;
    public  Vector3 target;
    Vector3Int direction;

    void Awake()
    {
        if (grid == null)
        {
            grid = FindObjectOfType<Grid>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cellPosition = grid.WorldToCell(transform.position);
        target = transform.position = grid.CellToWorld(cellPosition);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            
            var neighbor = cellPosition + direction;
            var nPoint = grid.CellToWorld(neighbor);
            var collider = Physics2D.OverlapBox(nPoint, grid.cellSize * .5f, 0);

            if (!collider)
            {
                cellPosition = neighbor;
                target = grid.CellToWorld(cellPosition);
            }

        }

        transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);
    }

    public void Move(Vector3Int dir)
    {
        direction = dir;
    }
    
#if TESTING
    private void OnGUI()
    {
        var neighbor = cellPosition + direction;
        var nPoint = grid.CellToWorld(neighbor);
        var collider = Physics2D.OverlapBox(nPoint, grid.cellSize * .5f, 0);

        GUI.color = Color.black;
        GUI.skin.label.fontSize = 30;
        GUI.Label(new Rect(10, 10, 500, 100), $"Neighbor in the way: {collider?.name}");
    }
#endif

    private void OnDrawGizmos()
    {
        var neighbor = cellPosition + direction;
        var nPoint = grid.CellToWorld(neighbor);

        Gizmos.color = new Color(0, 0, 1, .2f);
        Gizmos.DrawCube(nPoint, Vector3.one);
    }
}
