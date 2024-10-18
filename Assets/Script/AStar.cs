using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class AStar
{

    private Square[,] grid;
    private int width;
    private int height;
    public AStar(Square[,] square)
    {
        grid = square;
        width=square.GetLength(0);
        height = square.GetLength(1);
    }


    private List<Square> getNeighbors(Square nd)
    { 
        List<Square> neighbors = new List<Square>();

        for(int x=-1; x<=1; x++) 
            for(int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int newX = nd.X + x;
                int newY = nd.Y + y;

                if(newX >= 0 && newX <  width && newY >= 0 && newY < height)
                    neighbors.Add(grid[newX, newY]);
            }
        
        return neighbors;
    }
    public List<Square> findPath(Square start, Square end)
    {
        List<Square> openSet = new List<Square>();
        HashSet<Square> closedSet = new HashSet<Square>();
        openSet.Add(start);
        start.H = 0;
        start.G = 0;

        while (openSet.Count > 0)
        {
        Square curent = openSet[0];
            
            for (int i = 1; i < openSet.Count; i++)
                if (openSet[i].F < curent.F || openSet[i].F == curent.F && openSet[i].H < curent.H)
                    curent = openSet[i];

            openSet.Remove(curent);
            
            //curent.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
            //Thread.Sleep(100);
            closedSet.Add(curent);

            if (curent == end)
                return RetracePath(start, curent); 

            foreach(Square neighbor in getNeighbors(curent))
            {

                if (!neighbor.canWalk ||closedSet.Contains(neighbor))
                    continue;
                
                int movecost = curent.G + getDistance(curent,neighbor);

                if(movecost < neighbor.G || !openSet.Contains(neighbor))
                {
                    neighbor.G = movecost;
                    neighbor.H = getDistance(neighbor,end);
                    neighbor.parent = curent;
                    if(!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return new List<Square>();
    }

    public List<Square> findPath(Square start, Square end, List<Square> visitedCords)
    {
        List<Square> openSet = new List<Square>();
        HashSet<Square> closedSet = new HashSet<Square>();
        openSet.Add(start);
        start.H = 0;
        start.G = 0;

        while (openSet.Count > 0)
        {
            Square curent = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
                if (openSet[i].F < curent.F || openSet[i].F == curent.F && openSet[i].H < curent.H)
                    curent = openSet[i];

            openSet.Remove(curent);

            visitedCords.Add(curent);
            closedSet.Add(curent);

            if (curent == end)
                return RetracePath(start, curent);

            foreach (Square neighbor in getNeighbors(curent))
            {

                if (!neighbor.canWalk || closedSet.Contains(neighbor))
                    continue;

                int movecost = curent.G + getDistance(curent, neighbor);

                if (movecost < neighbor.G || !openSet.Contains(neighbor))
                {
                    neighbor.G = movecost;
                    neighbor.H = getDistance(neighbor, end);
                    neighbor.parent = curent;
                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return new List<Square>();
    }

    public int getDistance(Square a, Square b)
    {
        int dstX = Mathf.Abs(a.X - b.X);
        int dstY = Mathf.Abs(a.Y - b.Y);
        if (dstX > dstY)
            return 14*dstY + 10 * (dstX - dstY);
        return 14*dstX + 10 * (dstY - dstX);

    }
    private List<Square> RetracePath(Square startNode, Square endNode)
    {
        List<Square> path = new List<Square>();
        Square currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        return path;
    }

}

