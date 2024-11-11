using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class fileLoader : MonoBehaviour
{
    [SerializeField] StreamReader sr = null;
    [SerializeField] String line;
    [SerializeField] BoardMaker bM;
    [SerializeField] bool reading;
    [SerializeField] private GameObject WSquare;
    [SerializeField] private GameObject BSquare;
    Vector2 st;
    [SerializeField] int i, j;
    [SerializeField] int h, w;
    SelectMenager SLinstance;
    void Start()
    {

    }

    void Update()
    {
        if (!reading)
            return;


        if (line != null)
        {
            if (j >= w - 1)
                j = 0;

            line = sr.ReadLine();
            char[] chars = line.ToCharArray();
            foreach (char c in chars)
            {
                if (c == '.')
                {
                    Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = true;
                    st += Vector2.right;
                }
                else if (c == '@')
                {
                    Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = false;
                    st += Vector2.right;
                }
                j++;
            }
            i++;
            st = new Vector2(0.5f, st.y - 1);
        }
        

    }

    public void loadMap(Save sv)
    {
        sr = new StreamReader(sv.path);
        h = bM.setHeight(sv.Height);
        w = bM.setWidth(sv.Width);
        sr.DiscardBufferedData();
        SLinstance = SelectMenager.GetInstance();
        SLinstance.setData(w, h, this.transform);
        bM.setConfiner(w, h);
        st = new Vector2(0.5f, -0.5f);
        i = j = 0;
        reading = true;
        line = sr.ReadLine();
        line = sr.ReadLine();
        line = sr.ReadLine();
        line = sr.ReadLine();
    }

    public void loadMapInstant(Save sv)
    {
        h = bM.setHeight(sv.Height);
        w = bM.setWidth(sv.Width);

        SLinstance = SelectMenager.GetInstance();
        SLinstance.setData(w, h, this.transform);
        bM.setConfiner(w, h); 
        st = new Vector2(0.5f, -0.5f);
        reading = false;
        sr = new StreamReader(sv.path);

        line = sr.ReadLine();
        line = sr.ReadLine();
        for(int i = 0; i < w; i++)
        {
            line = sr.ReadLine();
            char[] chars = line.ToCharArray();
            j = 0;
            foreach (char c in chars)
            {
                if (c == '.')
                {
                    Square sq = Instantiate(WSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = true;
                    st += Vector2.right;
                }
                else if (c == '@')
                {
                    Square sq = Instantiate(BSquare, st, transform.rotation, transform).GetComponent<Square>();
                    sq.X = j;
                    sq.Y = i;
                    sq.canWalk = false;
                    st += Vector2.right;
                }
                j++;
            }
            st = new Vector2(0.5f, st.y - 1);
        }
        
    }
    public void stop()
    {
        reading = false;
    }

    public void saveMap()
    {

    }

}
