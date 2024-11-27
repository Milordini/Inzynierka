using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Kolorowanka : MonoBehaviour
{
    int tryb = 3;
    private void Start()
    {
        SelectMenager.GetInstance().tryb = tryb;
    }
    void Update()
    {
        if (tryb == 0 || tryb == 3)
            return;

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.transform == null)
                return;

            if (hit.transform.GetComponent<SpriteRenderer>() == null)
                return;

            if (tryb == 1 && hit.transform.GetComponent<SpriteRenderer>().color == Color.black)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.white;
                hit.transform.AddComponent<Selector>();
                hit.transform.GetComponent<Square>().canWalk = true;
            }
            else if (tryb == 2)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.black;
                Destroy(hit.transform.GetComponent<Selector>());
                hit.transform.GetComponent<Square>().canWalk = false;
            }
        }
    }

    public void mode(int x)
    {
        if(x != tryb)
        {
            tryb = x;
            SelectMenager.GetInstance().tryb = tryb;
        }else if (x == tryb)
        {
            tryb = 3;
            SelectMenager.GetInstance().tryb = tryb;
        }

    }
}
