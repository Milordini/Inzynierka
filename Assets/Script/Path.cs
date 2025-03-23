using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Path : MonoBehaviour
{
    [SerializeField] Button but;
    [SerializeField] private ToggleGroup algorytm;
    [SerializeField] private ToggleGroup tryb;
    [SerializeField] AStar ast;
    [SerializeField] Djikstra dji;
    private int tr;
    private SelectMenager SMinstance;
    public void MakePath()
    {
        SMinstance = SelectMenager.GetInstance();
        Toggle tg = algorytm.ActiveToggles().FirstOrDefault();
        trybselect(tryb.ActiveToggles().FirstOrDefault());
        if (togSelect(tg, 1) == 0)
            dji.setData(SMinstance.GetGrid(), SMinstance.getStart().GetComponent<Square>(), SMinstance.getEnd().GetComponent<Square>(),tr);
        else if (togSelect(tg, 1) == 1)
            ast.setData(SMinstance.GetGrid(), SMinstance.getStart().GetComponent<Square>(), SMinstance.getEnd().GetComponent<Square>(), tr);
    }


    private int togSelect(Toggle tg, int poz)
    {
        char x = tg.gameObject.name[poz - 1];
        switch (x)
        {
            case 'D': { poz = 0; } break;
            case 'A': { poz = 1; } break;
            default: { poz = -1; } break;
        }
        return poz;
    }

    private void trybselect(Toggle tg)
    {
        char x = tg.gameObject.name[1];
        if (x.Equals('4'))
            tr = 4;
        else if (x.Equals('8'))
            tr = 8;
        else
            tr = 0;

    }
}
