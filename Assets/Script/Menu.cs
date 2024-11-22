using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private ToggleGroup mapT;
    [SerializeField] private ToggleGroup pathAl;
    [SerializeField] private TMP_InputField wid;
    [SerializeField] private TMP_InputField hei;
    [SerializeField] private GameObject scrollWiew_save;
    [SerializeField] private GameObject content_results;
    [SerializeField] private TextMeshProUGUI Wisited;
    [SerializeField] private TextMeshProUGUI Pathleng;
    [SerializeField] private TextMeshProUGUI time;
    private void Start()
    {

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
        char x = tg.gameObject.name[poz-1];
        switch (x)
        {
            case 'W': { poz = 0; } break;
            case 'B': { poz = 1; } break;
            case 'R': { poz = 2; } break;
            default: { poz = -1; } break;
        }
        return poz;
    }

    public void updateFields(int w,int h)
    {
        wid.text = w.ToString();
        hei.text = h.ToString();
    }

    public void FilesWindow()
    {
        scrollWiew_save.SetActive(!scrollWiew_save.activeInHierarchy);
    }

    public void setdat(String alg,int tryb,Square start, Square end, int w, int p,long t)
    {
        var but = Instantiate(Resources.Load<GameObject>("Pref/Button"));
        var pan = Instantiate(Resources.Load<GameObject>("Pref/Info_Panel"));
        but.GetComponent<Button>().onClick.AddListener(() => { pan.SetActive(!pan.activeInHierarchy); });
        but.transform.parent = content_results.transform;
        pan.transform.parent = content_results.transform;
        pan.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(alg + " x" + tryb);
        pan.transform.GetChild(1).GetComponent<TextMeshProUGUI>().SetText("(" + start.X + "," + start.Y + ")");
        pan.transform.GetChild(2).GetComponent<TextMeshProUGUI>().SetText("(" + end.X + "," + end.Y + ")");
        pan.transform.GetChild(3).GetComponent<TextMeshProUGUI>().SetText(w.ToString());
        pan.transform.GetChild(4).GetComponent<TextMeshProUGUI>().SetText(p.ToString());
        double seconds = t / 1000f;
        pan.transform.GetChild(5).GetComponent<TextMeshProUGUI>().SetText(seconds.ToString());
    }
}
