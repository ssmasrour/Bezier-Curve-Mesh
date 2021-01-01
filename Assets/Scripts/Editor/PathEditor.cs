using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathInspector))]
public class PathEditor : Editor
{
    PathInspector creator;
    Path path;

    private void OnEnable() // Initialize the required path for editor
    {
        creator = target as PathInspector;
        if (creator.path == null)
        {
            creator.CreatePath();
        }
        path = creator.path;
    }


    private void OnSceneGUI()
    {
        AddSegment();
        Draw();
    }

    private void Draw()
    {
        // رسم خطوط هندل
        for (int i = 0; i < path.NumberOfSegments; i++)
        {
            Vector2[] segmentPoints = path.GetAllPointsInSegment(i);

            Handles.color = Color.red;
            
            //  رسم خطوط تنجنت ها
            Handles.DrawLine(segmentPoints[0], segmentPoints[1]);
            Handles.DrawLine(segmentPoints[2], segmentPoints[3]);

            // رسم منحنیها
            Handles.DrawBezier(segmentPoints[0], segmentPoints[3], segmentPoints[1], segmentPoints[2], Color.yellow, null, 10f);
        }


        // رسم نقاط هندل ها
        Handles.color = Color.green;
        for (int i = 0; i < path.NumberOfPoints; i++)
        {
            Vector2 curPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, 0.1f, Vector2.zero, Handles.CircleHandleCap);

            if (path[i] != curPos)
            {
                path.MovePoint(i, curPos);
            }

        }
    }

    private void AddSegment()
    {
        Event sceneEvent = Event.current;
        Vector2 clickPos = HandleUtility.GUIPointToWorldRay(sceneEvent.mousePosition).origin;

        if (sceneEvent.button == 0 /* left click */ &&
            sceneEvent.type == EventType.MouseDown &&
            sceneEvent.shift)
        {
            Undo.RecordObject(creator, "New Segment Added");
            path.AddSegment(clickPos);
        }
    }

}


