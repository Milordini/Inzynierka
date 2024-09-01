using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button but;
    [SerializeField] GameObject obj;
    [SerializeField] TMP_Text but_text;
    [SerializeField] private GameObject Obstacle;
    [SerializeField] private ToggleGroup mapT;
    [SerializeField] private ToggleGroup pathAl;
    [SerializeField] private TMP_InputField wid;
    [SerializeField] private TMP_InputField hei;

    private void Start()
    {
        obj = this.gameObject;
        but_text = but.GetComponentInChildren<TMP_Text>();
        but.onClick.AddListener(Toglle);
    }
    public void Toglle()
    {
        obj.SetActive(!obj.activeInHierarchy);
        if (obj.activeInHierarchy)
        {
            but_text.SetText("Close");
            Obstacle.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (!obj.activeInHierarchy)
        {
            but_text.SetText("Menu");
            Obstacle.GetComponent<BoxCollider2D>().enabled = false;
        }
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
    private int toint(int[] st)
    {
        int l = st.Length;
        int sum = 0;
        int pow = 0;
        for (int i = l - 1; i >= 0; i--)
        {
            sum += st[i] * (int)MathF.Pow(10, pow);
            pow++;
        }
        return sum;
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
}
