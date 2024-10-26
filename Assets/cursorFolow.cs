using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursorFolow : MonoBehaviour
{

    void Update()
    {
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        Vector3 facing = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        this.transform.position = facing;
    }
}
