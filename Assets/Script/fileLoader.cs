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
        if(line == null)
            sr.Close();

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

        Menu.getInst().refreshMapName(sv.Name);
        SLinstance = SelectMenager.GetInstance();
        SLinstance.setData(w, h, this.transform);
        bM.setConfiner(w, h);
        st = new Vector2(0.5f, -0.5f);
        reading = false;
        sr = new StreamReader(sv.path);
        sr.DiscardBufferedData();
        line = sr.ReadLine();
        line = sr.ReadLine();
        line = sr.ReadLine();
        line = sr.ReadLine();

        
        for (int i = 0; i < h; i++)
        {
            line = sr.ReadLine();
            if (line == null)
                continue;
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
        sr.Close();
    }
    public void stop()
    {
        reading = false;
    }

    public void saveMap(string path, int height, int width)
    {
        SLinstance = SelectMenager.GetInstance();
        Square[,] grid = SLinstance.GetGrid();
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine("type octile");
        sw.WriteLine("height " + height.ToString());
        sw.WriteLine("width " + width.ToString());
        sw.WriteLine("map");
        for (int i = 0; i < grid.GetLength(1); i++)
        {
            string line = "";
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                if (grid[j, i].canWalk)
                    line += ".";
                else if (!grid[j, i].canWalk)
                    line += "@";
            }
            sw.WriteLine(line);
        }
        sw.Close();
        paint(grid,path.Substring(path.IndexOf('\\') + 1, (path.IndexOf('.')) - (path.IndexOf('\\') + 1)));
    }

    public void paint(Square[,] grid,string name)
    {
        Texture2D texture = new Texture2D(grid.GetLength(0), grid.GetLength(1));

        for (int i = 0; i < grid.GetLength(1); i++)
        {
            for (int j = 0; j < grid.GetLength(0); j++)
            {
                if (grid[j, i].canWalk)
                    texture.SetPixel(j,i,Color.white);
                else if (!grid[j, i].canWalk)
                    texture.SetPixel(j, i, Color.black);
            }

        }
        texture.Apply();
        byte[] bytes = texture.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/Resources/maps_icon\\" + name + ".png", bytes);
    }
}
