using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public List<Vector2> points;

    public Path(Vector2 newAnchor) // تعریف نقاط اولین سگمنت - 4 تایی
    {
        points = new List<Vector2>
        {
            newAnchor + Vector2.left,
            newAnchor + (Vector2.left + Vector2.up) * 0.5f,
            newAnchor + (Vector2.right + Vector2.down) * 0.5f,
            newAnchor + Vector2.right
        };
    }

    public void AddSegment(Vector2 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);
    }

    public Vector2[] GetAllPointsInSegment(int segmentNumber)
    {
        return new Vector2[]
        {
            points[segmentNumber * 3],
            points[segmentNumber * 3 + 1],
            points[segmentNumber * 3 + 2],
            points[segmentNumber * 3 + 3]
        };
    }

    public Vector2 this[int i] // ایندکسر برای سهولت دسترسی به مختصات نقاط
    {
        get
        {
            return points[i];
        }
    }

    public int NumberOfPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumberOfSegments
    {
        get
        {
            return (points.Count - 4) / 3 + 1;
        }
    }

    public void MovePoint(int i,Vector2 newPos)
    {
        Vector2 deltaMove = newPos - points[i];
        points[i] = newPos;

        if (i % 3 == 0)
        {
            if (i + 1 < points.Count)
            {
                points[i + 1] += deltaMove;
            }
            if (i - 1 >= 0)
            {
                points[i - 1] += deltaMove;
            }
        }
        else
        {
            bool nextPointIsAnchor = (i + 1) % 3 == 0;
            int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
            int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;

            if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count)
            {
                float dst = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                Vector2 dir = (points[anchorIndex] - newPos).normalized;
                points[correspondingControlIndex] = points[anchorIndex] + dir * dst;
            }
        }
    }
}
