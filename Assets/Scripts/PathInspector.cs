using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathInspector : MonoBehaviour
{
    public Path path;

    public void CreatePath()
    {
        path = new Path(transform.position);
    }
}
