using System.Collections.Generic;
using UnityEngine;

public class SelectMenager
{
    private int width, height;
    Transform Parent;
    private static SelectMenager _instance;
    private GameObject selected1, selected2;
    private int[,] BinMap = null;
    Square[,] grid;
    public int tryb { get; set; }
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
        if (tryb != 0)
            return;

        if (selected1 != selected && selected2 != selected)
        {
            if (selected1 == null)
            {
                selected1 = selected;
                SR.color = Color.green;
                Menu.getInst().SetStart(selected.GetComponent<Square>());
            }
            else if (selected2 == null)
            {
                selected2 = selected;
                SR.color = Color.blue;
                Menu.getInst().SetEnd(selected.GetComponent<Square>());
            }
            else
            {
                selected1.GetComponent<SpriteRenderer>().color = Color.white;
                selected1 = selected2;
                selected1.GetComponent<SpriteRenderer>().color = Color.green;
                Menu.getInst().SetStart(selected1.GetComponent<Square>());

                selected2 = selected;
                SR.color = Color.blue;
                Menu.getInst().SetEnd(selected.GetComponent<Square>());
            }
        }
        else
        {
            if (selected1 == selected)
            {
                selected1.GetComponent<SpriteRenderer>().color = Color.white;
                selected1 = null;
                Menu.getInst().SetStart(null);
            }
            else if (selected2 == selected)
            {
                selected2.GetComponent<SpriteRenderer>().color = Color.white;
                selected2 = null;
                Menu.getInst().SetEnd(null);
            }
        }

    }

    public void resSelect()
    {
        if (selected1 != null)
        {
            selected1.GetComponent<SpriteRenderer>().color= Color.white;
            selected1 = null;
        }
        if (selected2 != null)
        {
            selected2.GetComponent<SpriteRenderer>().color= Color.white;
            selected2 = null;
        }
    }

    //public void makePathAStar()
    //{
    //    if(pathBoard!=null)
    //        clearPath();

    //    AStar ast = new AStar(GetGrid(height, width));
    //    List<Square> path = ast.findPath(selected1.GetComponent<Square>(), selected2.GetComponent<Square>(),traced);
    //    pathBoard = new GameObject();
    //    pathBoard.transform.position = Vector3.zero;
    //    BoardMaker.pathScreen(path, pathBoard.transform,traced);
    //}

    //public void makePathDjikstra()
    //{
    //    if (pathBoard != null)
    //        clearPath();
    //    List<Square> traced = new List<Square>();
    //    Djikstra dj = new Djikstra(GetGrid());
    //    List<Square> path = dj.findPath(selected1.GetComponent<Square>(), selected2.GetComponent<Square>(),traced);
    //    pathBoard = new GameObject();
    //    pathBoard.transform.position = Parent.position;
    //    BoardMaker.pathScreen(path, pathBoard.transform,traced);
    //}

    public void deleteChildren(GameObject parent)
    {
        if (parent.transform.childCount != 0)
        {
            for (int i = 0; i < parent.transform.childCount; i++)
                GameObject.Destroy(parent.transform.GetChild(i).gameObject);
        }
    }

    public Square[,] GetGrid()
    {
        grid = new Square[width, height];
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
