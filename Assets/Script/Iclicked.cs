using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iclicked : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("kliknoles mnie: " + this.gameObject.name);
    }
}
