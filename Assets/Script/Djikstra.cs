using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class Djikstra : MonoBehaviour
{
    Square[,] grid;
    List<Square> q = new List<Square>();
    int width;
    int height;
    Square start, end;
    [SerializeField] Transform par;
    Square tem;
    bool ended = true;
    int tryb;
    private void Update()
    {
        if (ended)
            return;

        if (q.Count > 0)
        {


            Square u = q[0];
            for (int i = 1; i < q.Count; i++)
                if (u.distance > q[i].distance)
                    u = q[i];
            q.Remove(u);
            if (u == end)
            {
                q.Clear();

            }
            if (tryb == 4)
            {
                foreach (Square square in getNeighborsx4(u, q))
                {
                    if (!square.canWalk)
                    {
                        q.Remove(square);
                        continue;
                    }
                    float newdistance = u.distance + getDistance(u, square);
                    Instantiate(Resources.Load<GameObject>("Pref/Square (3)"), u.transform.position, u.transform.rotation, par);
                    if (newdistance <= square.distance)
                    {
                        square.distance = newdistance;
                        square.parent = u;
                    }
                }
            }
            else if (tryb == 8)
            {
                foreach (Square square in getNeighbors(u, q))
                {
                    if (!square.canWalk)
                    {
                        q.Remove(square);
                        continue;
                    }
                    float newdistance = u.distance + getDistance(u, square);
                    Instantiate(Resources.Load<GameObject>("Pref/Square (3)"), u.transform.position, u.transform.rotation, par);
                    if (newdistance <= square.distance)
                    {
                        square.distance = newdistance;
                        square.parent = u;
                    }
                }
            }
        }
        if (q.Count == 0)
        {

            if (tem != start)
            {
                Instantiate(Resources.Load<GameObject>("Pref/Square (2)"), tem.transform.position, tem.transform.rotation, par);
                tem = tem.parent;
            }
            else
            {
                Instantiate(Resources.Load<GameObject>("Pref/Start"), start.transform.position, start.transform.rotation, par);
                Instantiate(Resources.Load<GameObject>("Pref/End"), end.transform.position, end.transform.rotation, par);
                ended = true;
            }

        }
        //return retracePath(start, end);
    }

    //public List<Square> findPath(Square start, Square end, List<Square> visitedCords)
    //{
    //    List<Square> q = new List<Square>();

    //    foreach (Square square in grid)
    //    {
    //        square.distance = Mathf.Infinity;
    //        square.parent = null;
    //        q.Add(square);
    //    }
    //    start.distance = 0;
    //    while (q.Count > 0)
    //    {
    //        Square u = q[0];
    //        for (int i = 1; i < q.Count; i++)
    //            if (u.distance > q[i].distance)
    //                u = q[i];
    //        q.Remove(u);

    //        foreach (Square square in getNeighbors(u, q))
    //        {
    //            if (!square.canWalk)
    //                continue;
    //            float newdistance = u.distance + getDistance(u, square);
    //            visitedCords.Add(u);
    //            if (newdistance <= square.distance)
    //            {
    //                square.distance = newdistance;
    //                square.parent = u;
    //            }
    //        }
    //    }
    //    return retracePath(start, end);
    //}

    public void setData(Square[,] _grid, Square start, Square end, int tryb)
    {
        grid = _grid;
        width = _grid.GetLength(0);
        height = _grid.GetLength(1);
        foreach (Square square in grid)
        {
            square.distance = Mathf.Infinity;
            square.parent = null;
            q.Add(square);
        }
        this.start = start;
        this.end = end;
        start.distance = 0;
        tem = end;
        ended = false;
        this.tryb = tryb;
        if (par.childCount != 0)
        {
            for (int i = 0; i < par.childCount; i++)
                Destroy(par.GetChild(i).gameObject);
        }
    }

    private List<Square> getNeighbors(Square nd, List<Square> lt)
    {
        List<Square> neighbors = new List<Square>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int newX = nd.X + x;
                int newY = nd.Y + y;

                if (newX >= 0 && newX < width && newY >= 0 && newY < height && lt.Contains(grid[newX, newY]))
                    neighbors.Add(grid[newX, newY]);
            }

        return neighbors;
    }

    private List<Square> getNeighborsx4(Square nd, List<Square> lt)
    {
        List<Square> neighbors = new List<Square>();

        int[] tabx = { -1, 0, 0, 1 };
        int[] taby = { 0, 1, -1, 0 };

        for (int i = 0; i < 4; i++)
        {
            int newX = nd.X + tabx[i];
            int newY = nd.Y + taby[i];

            if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                neighbors.Add(grid[newX, newY]);
        }
        return neighbors;
    }

    private float getDistance(Square a, Square b)
    {
        if (a.X == b.X || a.Y == b.Y)
            return 1;
        return 1.4f;

    }

    //private List<Square> retracePath(Square start, Square target)
    //{
    //    List<Square> path = new List<Square>();
    //    Square tem = target;
    //    while(tem != start)
    //    {
    //        path.Add(tem);
    //        tem = tem.parent;
    //    }
    //    return path;
    //}

}
