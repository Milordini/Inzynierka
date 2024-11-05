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
    int i, j;

    void Start()
    {
        Me = transform;
        BuildMap();

    }

    private void Update()
    {
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
    public void BuildMap()
    {
        int[] tab = mn.mapOpt();
        w = tab[0];
        h = tab[1];
        tryb = tab[2];

        if (transform.childCount != 0)
        {
            st = new Vector2(0.5f, -0.5f);
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
        }
        i = j = 0;
        SLinstance = SelectMenager.GetInstance();
        SLinstance.clearPath(pathParent);
        SLinstance.setData(w, h, transform);
        //SLinstance.MakeBitMap(h, w);
        //doPliku(SLinstance.getBitMap());
        setConfiner(w, h);
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

    //private void zPLiku()
    //{
    //    StreamReader sr = new StreamReader("Assets/maze512-1-9.txt");
    //    String line = sr.ReadLine();
    //    h = int.Parse(line);
    //    line = sr.ReadLine();
    //    w = int.Parse(line);
    //    char[] board;
    //    st = new Vector2(0.5f, -0.5f);
    //    int j = 0;
    //    while (line != null)
    //    {
    //        board = line.ToCharArray();
    //        foreach (char c in board)
    //            Debug.Log(c);

    //        for (int i = 0; i < 1; i++)
    //        {
    //            if (board[i] == '.')
    //            {
    //                Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
    //                sq.X = j;
    //                sq.Y = i;
    //                sq.canWalk = true;
    //                st += Vector2.right;
    //            }
    //            else
    //            {
    //                Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
    //                sq.X = j;
    //                sq.Y = i;
    //                sq.canWalk = false;
    //                st += Vector2.right;
    //            }
    //        }
    //        st = new Vector2(0.5f, st.y - 1);
    //        j++;
    //    }

    //}

    //static public void pathScreen(List<Square> path,Transform par)
    //{
    //    GameObject pathPref = Resources.Load<GameObject>("Pref/Square (2)");
    //    if (pathPref == null)
    //        Debug.Log("kurwa");
    //    foreach (Square square in path)
    //    {
    //        Debug.Log("X = " + square.X);
    //        Debug.Log("Y = " + square.Y);
    //        GameObject sq = Instantiate(pathPref, square.transform.position, square.transform.rotation, par);
    //        sq.transform.position = new Vector3(sq.transform.position.x, sq.transform.position.y, -1f);
    //        if(square == path[0])
    //            sq.GetComponent<SpriteRenderer>().color = Color.blue;
    //    }
    //}

    //static public void pathScreen(List<Square> path, Transform par,List<Square> traced)
    //{
    //    GameObject pathPref = Resources.Load<GameObject>("Pref/Square (2)");
    //    GameObject tracPref = Resources.Load<GameObject>("Pref/Square (3)");

    //    if (pathPref == null)
    //        Debug.Log("kurwa");
    //    foreach (Square square in path)
    //    {
    //        Debug.Log("X = " + square.X);
    //        Debug.Log("Y = " + square.Y);
    //        GameObject sq = Instantiate(pathPref, square.transform.position, square.transform.rotation, par);
    //        sq.transform.position = new Vector3(sq.transform.position.x, sq.transform.position.y, -1f);
    //    }

    //    foreach (Square square in traced)
    //    {
    //        GameObject sq = Instantiate(tracPref, square.transform.position, square.transform.rotation, par);
    //        sq.transform.position = new Vector3(sq.transform.position.x, sq.transform.position.y, -0.5f);
    //    }
    //    SelectMenager SLinstance = SelectMenager.GetInstance();
    //    GameObject sq1 = Instantiate(Resources.Load<GameObject>("Pref/Start"), SLinstance.getStart().transform.position, SLinstance.getStart().transform.rotation,par);
    //    sq1.transform.position = new Vector3(sq1.transform.position.x, sq1.transform.position.y, -1.5f);
    //    sq1 = Instantiate(Resources.Load<GameObject>("Pref/End"), SLinstance.getEnd().transform.position, SLinstance.getEnd().transform.rotation, par);
    //    sq1.transform.position = new Vector3(sq1.transform.position.x, sq1.transform.position.y, -1.5f);
    //}

    public void setHeight(int h) { this.h = h; }
    public void setWidth(int w) { this.w = w; }
}
