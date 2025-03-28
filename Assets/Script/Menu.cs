using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using System.Diagnostics;

public class Menu : MonoBehaviour
{
    [SerializeField] private ToggleGroup mapT;
    [SerializeField] private ToggleGroup pathAl;
    [SerializeField] private TMP_InputField wid;
    [SerializeField] private TMP_InputField hei;
    [SerializeField] private GameObject scrollWiew_save;
    [SerializeField] private GameObject content_results;
    [SerializeField] private TextMeshProUGUI start_pos;
    [SerializeField] private TextMeshProUGUI end_pos;
    static private Menu inst;
    private List<CSV> results = new List<CSV>();
    [SerializeField] private GameObject pathBoard;
    [SerializeField] private TextMeshProUGUI mapNameArea;
    private string mapName = "Temp";

    private void Start()
    {
        inst = this;
    }

    public int[] mapOpt()
    {
        int[] map = new int[3];
        map[0] = int.Parse(wid.text);
        map[1] = int.Parse(hei.text);
        Toggle tg = mapT.ActiveToggles().FirstOrDefault();
        map[2] = togSelect(tg, 3);
        return map;
    }

    private int togSelect(Toggle tg, int poz)
    {
        char x = tg.gameObject.name[poz - 1];
        switch (x)
        {
            case 'W': { poz = 0; } break;
            case 'B': { poz = 1; } break;
            case 'R': { poz = 2; } break;
            default: { poz = -1; } break;
        }
        return poz;
    }

    public void updateFields(int w, int h)
    {
        wid.text = w.ToString();
        hei.text = h.ToString();
    }

    public void FilesWindow()
    {
        scrollWiew_save.SetActive(!scrollWiew_save.activeInHierarchy);
    }

    public void setdat(CSV file)
    {
        results.Add(file);
        var but = Instantiate(Resources.Load<GameObject>("Pref/Button"));
        var pan = Instantiate(Resources.Load<GameObject>("Pref/Info_Panel"));
        but.GetComponent<Button>().onClick.AddListener(() => { pan.SetActive(!pan.activeInHierarchy); });
        but.transform.parent = content_results.transform;
        pan.transform.parent = content_results.transform;
        pan.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(file.Algorithm + " x" + file.Tryb);
        pan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("(" + file.Start.X + "," + file.Start.Y + ")");
        pan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("(" + file.End.X + "," + file.End.Y + ")");
        pan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(file.Traced.ToString());
        pan.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(file.pathLenght.ToString());
        pan.transform.GetChild(5).GetComponent<TextMeshProUGUI>().SetText(file.Time.ToString());
    }

    public void SaveFileCSV()
    {
        StreamWriter sw = new StreamWriter("Assets/CSV\\" + mapName + ".csv");
        sw.WriteLine("algorithm;tryb;start;end;traced;pathLenght;time");
        foreach (CSV csv in results)
        {
            sw.WriteLine(csv.toCSV());
        }
        sw.Close();
    }

    public void SetStart(Square sq)
    {
        if (sq != null)
        {
            start_pos.text = sq.X + "," + sq.Y;
        }
        else
        {
            start_pos.text = "Start position";
        }
    }

    public void SetEnd(Square sq)
    {
        if (sq != null)
        {
            end_pos.text = sq.X + "," + sq.Y;
        }
        else
        {
            end_pos.text = "End position";
        }
    }

    static public Menu getInst()
    {
        return inst;
    }

    public void wy�wietl_pathBoard() { pathBoard.SetActive(true); }
    public void wy�wietl_makerBoard() { pathBoard.SetActive(false); }

    public void openFolder(string path)
    {
        Process.Start(new ProcessStartInfo()
        {
            FileName = path,
            UseShellExecute = true,
            Verb = "open"
        });
    }

    public void refreshMapName()
    {
        mapNameArea.text = mapName;
    }

    public void refreshMapName(string newName)
    {
        mapName = newName;
        mapNameArea.text = mapName;
    }

    public string getMapNAme() { return mapName; }
}
