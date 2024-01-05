using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public static Waypoint previousWaypoint;

    public Vector2 targetPosition;

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        collider2D.transform.position = targetPosition;
        previousWaypoint = this;
    }
}