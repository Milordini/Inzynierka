using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camSlider : MonoBehaviour
{
    [SerializeField] public BoxCollider2D confiner;


    void Update()
    {

    }


    public void setConfiner(int w,int h)
    {
        confiner.size = new Vector2(w,h);
        confiner.transform.position = new Vector3(w/2f,-h/2f,0.1f);
    }
}
