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
    [SerializeField] private Vector2 st = new Vector2(0.5f,-0.5f);
    [SerializeField] private GameObject WSquare;
    [SerializeField] private GameObject BSquare;
    [SerializeField] private int height = 10;
    [SerializeField] private int width = 10;
    [SerializeField] SelectMenager SLinstance;

    void Start()
    {
        BuildMap();
    }


    public void BuildMap()
    {
        for (int i = 1; i < height+1; i++)
        {
            for (int j = 1; j < width+1; j++)
            {
                if (ChosePLate() % 2 == 0 || ChosePLate() % 3 == 0 || ChosePLate() % 7 == 0)
                {
                    Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.setX(j);
                    sq.setY(i);
                    sq.setCanWalk(true);
                    st += Vector2.right;
                }
                else
                {
                    Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.setX(j);
                    sq.setY(i);
                    sq.setCanWalk(false);
                    st += Vector2.right;
                }
            }
            st = new Vector2(0.5f, st.y - 1);
        }
        SLinstance = SelectMenager.GetInstance();
        SLinstance.MakeBitMap(height,width,transform);
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
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                sw.Write(tab[i, j]);
                sw.Write(' ');
            }
            sw.Write('\n');
        }
        sw.Close();
    }

}
