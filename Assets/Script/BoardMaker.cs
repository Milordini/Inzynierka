using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class BoardMaker : MonoBehaviour
{
    [SerializeField] private Vector2 st = new Vector2(0.5f, -0.5f);
    [SerializeField] private GameObject WSquare;
    [SerializeField] private GameObject BSquare;
    private Transform Me;
    [SerializeField] private int h = 10;
    [SerializeField] private int w = 10;
    [SerializeField] private int tryb = 0;
    [SerializeField] SelectMenager SLinstance;
    [SerializeField] private Menu mn;
    [SerializeField] private camSlider cS;
    [SerializeField] private BoxCollider confiner;
    [SerializeField] private GameObject pathParent;
    [SerializeField] private bool isMaking = false;
    int i, j;

    void Start()
    {
        Me = transform;
        build();

    }

    private void Update()
    {
        if (!isMaking)
            return;

        if (tryb == 0)
        {
            Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
            sq.X = j++;
            sq.Y = i;
            sq.canWalk = true;
            st += Vector2.right;

            if (j == w)
            {
                st = new Vector2(0.5f, st.y - 1);
                j = 0;
                i++;
            }

        }
        else if (tryb == 1)
        {
            Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
            sq.X = j++;
            sq.Y = i;
            sq.canWalk = true;
            st += Vector2.right;

            if (j == w)
            {
                st = new Vector2(0.5f, st.y - 1);
                j = 0;
                i++;
            }
        }
        else if (tryb == 2)
        {


            if (ChosePLate() % 2 == 0 || ChosePLate() % 3 == 0 || ChosePLate() % 7 == 0)
            {
                Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                sq.X = j++;
                sq.Y = i;
                sq.canWalk = true;
                st += Vector2.right;
            }
            else
            {
                Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                sq.X = j++;
                sq.Y = i;
                sq.canWalk = false;
                st += Vector2.right;
            }

            if (j == w)
            {
                st = new Vector2(0.5f, st.y - 1);
                j = 0;
                i++;
            }

        }
    }



    public void build()
    {
        int[] tab = mn.mapOpt();
        w = tab[0];
        h = tab[1];
        tryb = tab[2];
        st = new Vector2(0.5f, -0.5f);
        if ((w * h) > (200 * 200))
            BuildMap();
        else
            BuildMapInstant(); 
    }

    private void BuildMap()
    {
        
        isMaking = true;

        st = new Vector2(0.5f, -0.5f);
        i = j = 0;

        SLinstance = SelectMenager.GetInstance();
        SLinstance.deleteChildren(pathParent);
        SLinstance.deleteChildren(this.gameObject);
        SLinstance.setData(w, h, transform);
        setConfiner(w, h);
    }


    private void BuildMapInstant()
    {
        isMaking = false;

        i = j = 0;

        SLinstance = SelectMenager.GetInstance();
        SLinstance.deleteChildren(pathParent);
        SLinstance.deleteChildren(this.gameObject);
        SLinstance.setData(w, h, transform);
        setConfiner(w, h);


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

    }
    public void setConfiner(int w, int h)
    {
        confiner.size = new Vector3(w, h, 0);
        confiner.transform.position = new Vector3(w / 2f, -h / 2f, -10f);
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

    public void clearPath()
    {
        SLinstance = SelectMenager.GetInstance();
        SLinstance.deleteChildren(pathParent);
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

    public int setHeight(int h) { return this.h = h; }
    public int setWidth(int w) { return this.w = w; }
    
    public void sysOut()
    {
        Application.Quit();
    }
}
