using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using UnityEditor;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    [SerializeField] private Vector2 st = new Vector2(0.5f, -0.5f);
    [SerializeField] private GameObject WSquare;
    [SerializeField] private GameObject BSquare;
    [SerializeField] private int h = 10;
    [SerializeField] private int w = 10;
    [SerializeField] private int tryb = 0;
    [SerializeField] SelectMenager SLinstance;
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private Menu mn;

    void Start()
    {
        BuildMap();
    }


    public void BuildMap()
    {
        int[] tab = mn.mapOpt();
        w = tab[0];
        h = tab[1];
        tryb = tab[2];

        if (tryb == 0)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = true;
                    st += Vector2.right;
                }
                st = new Vector2(0.5f, st.y - 1);
            }
        }
        else if (tryb == 1)
        {
            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = false;
                    st += Vector2.right;
                }
                st = new Vector2(0.5f, st.y - 1);
            }
        }
        else if (tryb == 2)
        {

            for (int i = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++)
                {
                    if (ChosePLate() % 2 == 0 || ChosePLate() % 3 == 0 || ChosePLate() % 7 == 0)
                    {
                        Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                        sq.X = j;
                        sq.Y = i;
                        sq.canWalk = true;
                        st += Vector2.right;
                    }
                    else
                    {
                        Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                        sq.X = j;
                        sq.Y = i;
                        sq.canWalk = false;
                        st += Vector2.right;
                    }
                }
                st = new Vector2(0.5f, st.y - 1);
            }
        }
        Obstacle.transform.position = new Vector2(w / 2.0f, -h / 2.0f);
        Obstacle.GetComponent<BoxCollider2D>().size = new Vector2(w, h);
        SLinstance = SelectMenager.GetInstance();
        SLinstance.setData(w, h, transform);
        SLinstance.MakeBitMap(h, w);
        doPliku(SLinstance.getBitMap());
    }


    private int ChosePLate()
    {
        using (RNGCryptoServiceProvider rg = new RNGCryptoServiceProvider())
        {
            byte[] rno = new byte[5];
            rg.GetBytes(rno);
            int randomvalue = BitConverter.ToInt32(rno, 0);
            return randomvalue;
        }

    }

    private void doPliku(int[,] tab)
    {
        //Pass the filepath and filename to the StreamWriter Constructor
        StreamWriter sw = new StreamWriter("Assets/test.txt");
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                sw.Write(tab[j, i]);
                sw.Write(' ');
            }
            sw.Write('\n');
        }
        sw.Close();
    }
}
