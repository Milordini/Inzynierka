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
    [SerializeField] private GameObject Spoint;
    [SerializeField] private GameObject Terrain;
    [SerializeField] AStar ast;
    [SerializeField] Djikstra dji;
    private SelectMenager SMinstance;
    public void MakePath()
    {
        SMinstance = SelectMenager.GetInstance();

        Toggle tg = algorytm.ActiveToggles().FirstOrDefault();
        if(togSelect(tg,1)==0)
            SMinstance.makePathDjikstra();
        else if(togSelect(tg, 1) == 1)
           ast.setData(SMinstance.GetGrid(), SMinstance.getStart().GetComponent<Square>(), SMinstance.getEnd().GetComponent<Square>());
    }

    public void clearPath()
    {

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
}
