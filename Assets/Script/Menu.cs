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
    [SerializeField] private GameObject scrollWiew;
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
        scrollWiew.SetActive(!scrollWiew.activeInHierarchy);
    }


}
