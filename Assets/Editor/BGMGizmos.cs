using UnityEditor;
using UnityEngine;

public static class BGMGizmos
{
    [DrawGizmo(GizmoType.Active | GizmoType.NotInSelectionHierarchy)]
    private static void DrawGroupNodes(CreepyBGMHandler handler, GizmoType gizmoType)
    {
        const float radius = 20f;
        
        Vector3 handlerPosition = handler.transform.position;
        Vector3 lowestY = new Vector3(handlerPosition.x, handler.lowestY, 0);
        Vector3 highestY = new Vector3(handlerPosition.x, handler.highestY, 0);

        GUIStyle lowerStyle = new GUIStyle
        {
            normal =
            {
                textColor = (gizmoType & GizmoType.Selected) != 0 ? Color.green : Color.gray
            }
        };

        GUIStyle higherStyle = new GUIStyle
        {
            normal =
            {
                textColor = (gizmoType & GizmoType.Selected) != 0 ? Color.red : Color.gray
            }
        };

        Gizmos.color = (gizmoType & GizmoType.Selected) != 0 ? Color.green : Color.gray;
        Gizmos.DrawLine(lowestY + Vector3.left * radius, lowestY + Vector3.right * radius);
        Handles.Label(lowestY + Vector3.left * radius + Vector3.down * 0.25f, "Full Normal BGM", lowerStyle);
        
        Gizmos.color = (gizmoType & GizmoType.Selected) != 0 ? Color.red : Color.gray;
        Gizmos.DrawLine(highestY + Vector3.left * radius, highestY + Vector3.right * radius);
        Handles.Label(highestY + Vector3.left * radius + Vector3.down * 0.25f, "Full Creepy BGM", higherStyle);
    }
}
