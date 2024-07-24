using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AStar : MonoBehaviour
{

    private List<GameObject> closedSet;
    private List<GameObject> openSet;
    public bool Astar(Square start, Square goal)
    {
        openSet.Add(start.gameObject);

        
    }
        
}



