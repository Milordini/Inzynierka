using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Info : MonoBehaviour
{
    [SerializeField] public int X;
    [SerializeField] public int Y ;
    [SerializeField] public bool canWalk ;
    [SerializeField] public Square parent ;
    [SerializeField] public int G ;
    [SerializeField] public int H ;
    [SerializeField] public float Distance ;
    private Square sq;

    void Update()
    {
        if( sq == null )
            sq = gameObject.GetComponent<Square>();
        X=sq.X; Y= sq.Y;
        canWalk= sq.canWalk;
        G=sq.G; H=sq.H;
        parent = sq.parent;
        Distance = sq.distance;
    }
}
