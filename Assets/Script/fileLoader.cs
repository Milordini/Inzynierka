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
    Vector2 st;int i; int j;
    int h; int w;
    SelectMenager SLinstance;
    void Start()
    {

    }

    void Update()
    {
        if (reading)
        {
            if (sr == null)
            {
                sr = new StreamReader("Assets/maze512-1-9.txt");
                line = sr.ReadLine();
                bM.setHeight(int.Parse(line));
                line = sr.ReadLine();
                bM.setHeight(int.Parse(line));

                st = new Vector2(0.5f, -0.5f);
                i = j = 0;
            }

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
            if (i >= h - 1)
            {
                SLinstance = SelectMenager.GetInstance();
                SLinstance.setData(w, h, transform);
                //ssetConfiner(w, h);
            }
        }
    }
}
