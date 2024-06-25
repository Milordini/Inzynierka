using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Sprites;
using UnityEngine;

public class Selector : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SelectMenager SLinstance;
    [SerializeField] Color st;
    [SerializeField] Color sl;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        st = spriteRenderer.color;
        sl = Color.blue;

    }

    private void OnMouseDown()
    {
        SLinstance = SelectMenager.GetInstance();
        SLinstance.SetSelected(this.gameObject,spriteRenderer);
    }
}
