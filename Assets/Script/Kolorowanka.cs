using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Kolorowanka : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dp;
    List<TMP_Dropdown.OptionData> opt;
    int tryb = 0;
    private void Start()
    {
        opt = dp.options;
        dp.onValueChanged.AddListener(onValueChange);
    }
    void Update()
    {
        if (tryb == 0)
            return;

        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (tryb == 1)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.white;
                hit.transform.GetComponent<Square>().canWalk = true;
            }
            else if (tryb == 2)
            {
                hit.transform.GetComponent<SpriteRenderer>().color = Color.black;
                hit.transform.GetComponent<Square>().canWalk = false;
            }
        }
    }

    void onValueChange(int v)
    {
        tryb = v;
        SelectMenager.GetInstance().tryb = v;
    }

}
