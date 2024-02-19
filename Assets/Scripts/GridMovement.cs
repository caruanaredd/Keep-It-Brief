using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridMovement : MonoBehaviour
{
    private static Grid grid;
    Vector2 input = new();
    Vector3Int cellPosition;
    Vector3 target;
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
            cellPosition += direction;
            target = grid.CellToWorld(cellPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);
    }

    public void Move(Vector3Int dir)
    {
        direction = dir;
    }
}
