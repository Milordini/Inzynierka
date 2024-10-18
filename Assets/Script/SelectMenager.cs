using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SelectMenager
{
    private int width, height;
    Transform Parent;
    private static SelectMenager _instance;
    private GameObject selected1, selected2;
    private int[,] BinMap = null;
    Square[,] grid;
    private SelectMenager() { }

    public static SelectMenager GetInstance()
    {
        if (_instance == null)
            _instance = new SelectMenager();

        return _instance;
    }

    public void setData(int w, int h, Transform p)
    {
        width = w; height = h; Parent = p;
    }

    public void SetSelected(GameObject selected, SpriteRenderer SR)
    {
        if (selected1 != selected && selected2 != selected)
        {
            if (selected1 == null)
            {
                selected1 = selected;
                SR.color = Color.green;
            }
            else if (selected2 == null)
            {
                selected2 = selected;
                SR.color = Color.blue;
            }
            else
            {
                selected1.GetComponent<SpriteRenderer>().color = Color.white;
                selected1 = selected2;
                selected1.GetComponent<SpriteRenderer>().color = Color.green;

                selected2 = selected;
                SR.color = Color.blue;
            }
        }
        else
        {
            if (selected1 == selected)
            {
                selected1.GetComponent<SpriteRenderer>().color = Color.white;
                selected1 = null;
            }
            else if (selected2 == selected)
            {
                selected2.GetComponent<SpriteRenderer>().color = Color.white;
                selected2 = null;
            }
        }

    }

    public void makePathAStar()
    {
        List<Square> traced = new List<Square>();
        AStar ast = new AStar(GetGrid(height, width));
        List<Square> path = ast.findPath(selected1.GetComponent<Square>(), selected2.GetComponent<Square>(),traced);
        GameObject pathBoard = new GameObject();
        pathBoard.transform.position = Parent.position;
        BoardMaker.pathScreen(path, pathBoard.transform,traced);
    }

    public void makePathDjikstra()
    {
        List<Square> traced = new List<Square>();
        Djikstra dj = new Djikstra(GetGrid(height, width));
        List<Square> path = dj.findPath(selected1.GetComponent<Square>(), selected2.GetComponent<Square>(),traced);
        GameObject pathBoard = new GameObject();
        pathBoard.transform.position = Parent.position;
        BoardMaker.pathScreen(path, pathBoard.transform,traced);
    }


    public Square[,] GetGrid(int height, int width)
    {
        grid = new Square[width, height];//emolium szampon dow ³osów
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                grid[j, i] = Parent.GetChild(i * width + j).GetComponent<Square>();
            }
        }
        return grid;
    }

    public void MakeBitMap(int height, int width)
    {
        BinMap = new int[width, height];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                BinMap[j, i] = Dbit(i * width + j, Parent);
            }
        }
    }


    private int Dbit(int x, Transform parent)
    {
        Square sq = parent.GetChild(x).GetComponent<Square>();
        if (sq.canWalk)
            return 1;
        else if (!sq.canWalk)
            return 0;
        else
            return -1;
    }

    public int[,] getBitMap()
    {
        return BinMap;
    }

    public GameObject getStart() { return selected1; }
    public GameObject getEnd() { return selected2; }
}
