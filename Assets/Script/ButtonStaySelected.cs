using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonStaySelected : MonoBehaviour
{
    [SerializeField] private GameObject but1;
    [SerializeField] private GameObject but2;
    int selected = 1;


    void Update()
    {
        if (selected == 1)
            EventSystem.current.SetSelectedGameObject(but1);
        else if (selected == 2)
            EventSystem.current.SetSelectedGameObject(but2);

    }

    public void select(int s) { selected =s; }
}
