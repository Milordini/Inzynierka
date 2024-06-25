using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenager
{
    private static SelectMenager _instance;
    private GameObject selected1, selected2;
    private int[,] BinMap = null;

    private SelectMenager() { }

    public static SelectMenager GetInstance()
    {
        if(_instance==null)
        {
            _instance = new SelectMenager();
        }
        return _instance;
    }

    public void SetSelected(GameObject selected, SpriteRenderer SR)
    {
        if (selected1 != selected && selected2 != selected) {
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
            if(selected1 == selected) {
                selected1.GetComponent<SpriteRenderer>().color = Color.white;
                selected1 = null;
            }
            else if(selected2 == selected)
            {
                selected2.GetComponent<SpriteRenderer>().color = Color.white;
                selected2 = null;
            }
        }
    
    }

    public void MakeBitMap(int height, int width, Transform parent)
    {
        BinMap = new int[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                BinMap[i, j] = Dbit(i * width + j, parent);
            }
        }
    }


    private int Dbit(int x, Transform parent)
    {
        string tg = parent.GetChild(x).tag;
        if (tg == "White")
            return 1;
        else if (tg == "Black")
            return 0;
        else
            return -1;
    }

    public int[,] getBitMap()
    {
        return BinMap;
    }
}
