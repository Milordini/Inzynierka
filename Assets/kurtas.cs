using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class kurtas : MonoBehaviour
{

    [SerializeField] Button but;
    [SerializeField] GameObject img;
    void Start()
    {
        but.onClick.AddListener(delegate { img.SetActive(!img.activeInHierarchy);  });
    }

  
}
