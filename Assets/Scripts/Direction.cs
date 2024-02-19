using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}

public static class DirectionExtensions
{
    public static Vector2 ToVector2(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector2.up;
            case Direction.Down:
                return Vector2.down;
            case Direction.Left:
                return Vector2.left;
            case Direction.Right:
                return Vector2.right;
            
        }

        return Vector2.zero;
    
    }
    public static Vector3Int ToVector3Int(this Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                return Vector3Int.up;
            case Direction.Down:
                return Vector3Int.down;
            case Direction.Left:
                return Vector3Int.left;
            case Direction.Right:
                return Vector3Int.right;
            
        }

        return Vector3Int.zero;
    }
}