using NUnit;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.PackageManager;
using UnityEngine;

public class AStar : MonoBehaviour 
{
    private Square[,] grid;
    private int width;
    private int height;
    private bool working = false;
    private bool ended = false;
    Square start;
    Square end;
    List<Square> neigbours;
    List<Square> openSet = new List<Square>();
    HashSet<Square> closedSet = new HashSet<Square>();
    [SerializeField] Transform par;
    Square curent;
    private void Update()
    {
        

        if (openSet.Count > 0)
        {
            if (!ended)
            {
                curent = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                    if (openSet[i].F < curent.F || openSet[i].F == curent.F && openSet[i].H < curent.H)
                        curent = openSet[i];

                openSet.Remove(curent);

                Instantiate(Resources.Load<GameObject>("Pref/Square (3)"), curent.transform.position, transform.rotation, par);

                closedSet.Add(curent);
            }

            if (curent == end || ended)
            {//RetracePath(start, curent)  (Square startNode, Square endNode)
                ended = true;
                if(curent != start)
                {
                    Instantiate(Resources.Load<GameObject>("Pref/Square (2)"), curent.transform.position, transform.rotation, par);

                    curent = curent.parent;
                }else
                {
                    Instantiate(Resources.Load<GameObject>("Pref/start"), start.transform.position, transform.rotation, par);
                    Instantiate(Resources.Load<GameObject>("Pref/end"), end.transform.position, transform.rotation, par);

                    openSet.Clear();
                }
                
                return;
            }


            foreach (Square neighbor in getNeighborsx4(curent))
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
    }

    public void setData(Square[,] square,Square start,Square end)
    {
        grid = square;
        width = square.GetLength(0);
        height = square.GetLength(1);
        this.start = start;
        this.end = end;
        openSet.Clear();
        openSet.Add(start);
        start.H = 0;
        start.G = 0;
        ended = false;
    }

    private List<Square> getNeighbors(Square nd)
    {
        List<Square> neighbors = new List<Square>();

        for (int x =-1; x <= 1; x++)
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                
                int newX = nd.X + x;
                int newY = nd.Y + y;

                if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                    neighbors.Add(grid[newX, newY]);
            }

        return neighbors;
    }

    private List<Square> getNeighborsx4(Square nd)
    {
        List<Square> neighbors = new List<Square>();

        int[] tabx = { -1, 0, 0, 1 };
        int[] taby = { 0, 1, -1, 0 };

        for(int i = 0;i<4;i++)
        {
            int newX = nd.X + tabx[i];
            int newY = nd.Y + taby[i];

            if (newX >= 0 && newX < width && newY >= 0 && newY < height)
                neighbors.Add(grid[newX, newY]);
        }
        return neighbors;
    }
    public int getDistance(Square a, Square b)
    {
        int dstX = Mathf.Abs(a.X - b.X);
        int dstY = Mathf.Abs(a.Y - b.Y);
        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
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

