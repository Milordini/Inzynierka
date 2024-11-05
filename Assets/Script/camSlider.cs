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

        if(Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            _cam.m_Lens.OrthographicSize += -Input.GetAxis("Mouse ScrollWheel")*4;
            if (_cam.m_Lens.OrthographicSize > 30)
                _cam.m_Lens.OrthographicSize = 30;
            if (_cam.m_Lens.OrthographicSize < 10)
                _cam.m_Lens.OrthographicSize = 10;
        }

    }

    

    

}
