using System.Collections;
using System.Collections.Generic;
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
    private int[] _dx = { -1, 1, 0, 0 };
    private int[] _dy = { 0, 0, -1, 1 };
    public AStar(Square[,] square)
    {
        grid = square;
        width=square.GetLength(0);
        height = square.GetLength(1);
    }

    //private int GetHeuristic(Square a, Square b)
    //{
    //    int dx = Mathf.Abs(a.getX() - b.getX());
    //    int dy = Mathf.Abs(a.getY() - b.getY());
    //    return dx + dy;
    //}

    private List<Node> getNeighbors(Node nd)
    { 
        List<Node> neighbors = new List<Node>();

        for(int i=0; i<4; i++) 
        {
            int newX = nd.sq.getX() + _dx[i];
            int newY = nd.sq.getY() + _dy[i];

            if (newX > 0 && newX < width && newY > 0 && newY < height)
                neighbors.Add(new Node(grid[newX, newY]));

        }
        return neighbors;
    }
    public List<Square> findPath(Node start, Node end)
    {
        //List<Node> openList = new List<Node>();
        //HashSet<Node> closedList = new HashSet<Node>();

        //Node startNode = start;
        //Node endNode = end;
        //openList.Add(startNode);

        //    Node currentNode = openList[0];
        //while (openList.Count > 0)
        //{
        //    for (int i = 1; i < openList.Count; i++)
        //        if (openList[i].F < currentNode.F || (openList[i].F == currentNode.F && openList[i].H < currentNode.H))
        //            currentNode = openList[i];

        //    if (currentNode.sq == endNode.sq)
        //        return RetracePath(startNode, currentNode);

        //    openList.Remove(currentNode);
        //    closedList.Add(currentNode);

        //    foreach (var neigbor in getNeighbors(currentNode))
        //    {
        //        if (!neigbor.sq.getCanWalk() || closedList.Contains(neigbor))
        //            continue;

        //        int movementCost = currentNode.G + 1;
        //        if (movementCost < neigbor.G || !openList.Contains(neigbor))
        //        {
        //            neigbor.G = movementCost;
        //            neigbor.H = GetHeuristic(neigbor.sq, endNode.sq);
        //            neigbor.parent = currentNode;

        //            if (!openList.Contains(neigbor))
        //                openList.Add(neigbor);


        //        }
        //    }
        //}
        //return new List<Square>();
        Debug.Log("kurwa");
        List<Node> closedSet = new List<Node>();
        List<Node> openSet = new List<Node>();
        openSet.Add(start);
        Node curent = start;

        while (openSet.Count > 0)
        {
        Debug.Log("while "+openSet.Count);

            for (int i = 0; i < openSet.Count; i++)
                if (openSet[i].F < curent.F)
                { curent = openSet[i]; Debug.Log("if1"); }

            if (curent == end)
            {Debug.Log("if2"); return RetracePath(start, curent); }

            openSet.Remove(curent);
            closedSet.Add(curent);
            foreach(Node neighbor in getNeighbors(curent))
            {
                Debug.Log("forech");

                if (closedSet.Contains(neighbor))
                    continue;
                
                int movecost = curent.G + 10;

                if(!openSet.Contains(neighbor) || movecost < neighbor.G)
                {
                    neighbor.G = movecost;
                    neighbor.H = neighbor.heuristic(end.sq);
                    if(!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }
        return new List<Square>();
    }

    private List<Square> RetracePath(Node startNode, Node endNode)
    {
        List<Square> path = new List<Square>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode.sq);
            currentNode = currentNode.parent;
        }
        path.Add(startNode.sq);
        //path.Reverse();
        return path;
    }

}

public class Node
{
    public Square sq { get; set; }
    public Node parent { get; set; }
    public int G{  get; set; }
    public int H { get; set; }
    public int F { get { return G + H; } }
    public Node(Square square)
    {
        sq = square;
    }

    public int heuristic(Square sq)
    {
    //    this.H = Mathf.Sqrt(Mathf.Pow(sq.getX() - this.sq.getX(), 2) + Mathf.Pow(sq.getY() - this.sq.getY(), 2));
    //    return H;
        H = Mathf.Abs((this.sq.getX() - sq.getX()) + (this.sq.getY() - sq.getY()));
        return H;
    }
}



