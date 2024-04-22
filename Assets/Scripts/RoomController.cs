using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<GridObject> objectsToReset;
    private Dictionary<GridObject, Vector3Int> _startingPositions = new();

    void Start()
    {
        foreach (var obj in objectsToReset)
        {
            _startingPositions.Add(obj, obj.Cell);
        }
    }

    public void ResetScene()
    {
        foreach (var (gridObject, cell) in _startingPositions)
        {
            gridObject.Teleport(cell);
        }
    }
}