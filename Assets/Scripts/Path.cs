using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public static Path instance;
    public List<PathPointprofile> pathes;

    public LineRenderer linerenderer;

    private void Awake()
    {
        instance = this;
        DrawPath();
    }

    public Transform FindNextPoint(Transform point)
    {
        PathPointprofile findedpoint=pathes.Find(x => x.pointobject == point);
        if (findedpoint == null) return null;
        return findedpoint.nextpoint;
    }


    public void DrawPath()
    {
        linerenderer.positionCount = pathes.Count;
        for (int i = 0; i < pathes.Count; i++)
        {
            linerenderer.SetPosition(i, pathes[i].pointobject.transform.position);
        }
        linerenderer.SetPosition(pathes.Count - 1, pathes[pathes.Count - 1].pointobject.transform.position);
       
    }


    [System.Serializable]
    public class PathPointprofile
    {
        public Transform pointobject;
        public Transform nextpoint;
    }

}
