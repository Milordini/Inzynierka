using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hower : MonoBehaviour
{
    
    void Start()
    {
        
    }

    
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.current.transform.position, Vector2.down);
        if(hit.transform != null)
        {
            Debug.Log(hit.transform);
        }else
        {
            Debug.Log("ugh");
        }
    }
}
