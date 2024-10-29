using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.Image;

public class camSlider : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera _cam;
    [SerializeField] Vector3 difrence;
    [SerializeField] Vector3 origin;
    [SerializeField] bool drag;
    private void Start()
    {
      
    }


    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            difrence = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.transform.position;
            if(!drag)
            {
                drag = true;
                origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }else
        {
            drag =false;
        }

        if(drag)
        {
           _cam.transform.position = origin - difrence;
        }
    }


    

    

}
