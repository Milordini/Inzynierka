using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Djikstra 
{
    Square[,] grid;
    int width;
    int height;

    public Djikstra(Square[,] _grid)
    {
        grid = _grid;
        width = _grid.GetLength(0);
        height = _grid.GetLength(1);
    }

    public List<Square> findPath(Square start, Square end)
    {
        List<Square> q = new List<Square>();
        
        foreach(Square square in grid)
        {
            square.distance = Mathf.Infinity;
            square.parent = null;
            q.Add(square);
        }
        start.distance = 0;
        while(q.Count>0)
        {
            Square u = q[0];
            for(int i = 1; i < q.Count; i++)
                if (u.distance > q[i].distance)
                    u = q[i];
            q.Remove(u);
            foreach(Square square in getNeighbors(u,q))
            {
                if (!square.canWalk)
                    continue;
                float newdistance = u.distance +getDistance(u,square);
                if(newdistance <= square.distance)
                {
                    square.distance = newdistance;
                    square.parent = u;
                }
            }
        }
        return retracePath(start,end); 
    }

    public List<Square> findPath(Square start, Square end, List<Square> visitedCords)
    {
        List<Square> q = new List<Square>();

        foreach (Square square in grid)
        {
            square.distance = Mathf.Infinity;
            square.parent = null;
            q.Add(square);
        }
        start.distance = 0;
        while (q.Count > 0)
        {
            Square u = q[0];
            for (int i = 1; i < q.Count; i++)
                if (u.distance > q[i].distance)
                    u = q[i];
            q.Remove(u);
            
            foreach (Square square in getNeighbors(u, q))
            {
                if (!square.canWalk)
                    continue;
                float newdistance = u.distance + getDistance(u, square);
                visitedCords.Add(u);
                if (newdistance <= square.distance)
                {
                    square.distance = newdistance;
                    square.parent = u;
                }
            }
        }
        return retracePath(start, end);
    }

    private List<Square> getNeighbors(Square nd,List<Square> lt)
    {
        List<Square> neighbors = new List<Square>();
        for (int x = -1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int newX = nd.X + x;
                int newY = nd.Y + y;

                if (newX >= 0 && newX < width && newY >= 0 && newY < height && lt.Contains(grid[newX,newY]))
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

    private List<Square> retracePath(Square start, Square target)
    {
        List<Square> path = new List<Square>();
        Square tem = target;
        while(tem != start)
        {
            path.Add(tem);
            tem = tem.parent;
        }
        return path;
    }

}
