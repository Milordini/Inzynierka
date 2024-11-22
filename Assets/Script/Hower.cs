using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hower : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI txt;
    [SerializeField] public RectTransform pan;

    void Start()
    {
    }


    void Update()
    {
        Vector2 mousePosition = Input.mousePosition;
        pan.position = mousePosition + new Vector2(0,30);

        if (EventSystem.current.IsPointerOverGameObject())
        { pan.gameObject.SetActive(false); return; }

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            Square sq = hit.transform.GetComponent<Square>();
            txt.text = "(" + sq.X + "," + sq.Y + ")";
            pan.gameObject.SetActive(true);
        }
    }
}
