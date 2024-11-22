using NUnit.Framework.Internal.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Square : MonoBehaviour
{
    // general
    [SerializeField] public int X { get; set; }
    [SerializeField] public int Y { get; set; }
    [SerializeField] public bool canWalk { get; set; }
    [SerializeField] public Square parent { get; set; }

    // A*
    [SerializeField] public int G { get; set; }
    [SerializeField] public int H { get; set; }
    [SerializeField] public int F { get { return G + H; } }

    // Djikstra
    [SerializeField] public float distance { get; set; }

    public void resetSquare()
    {
        G = 0;
        H = 0;
        distance = 0;
    }

    //private void OnMouseOver()
    //{
    //    Debug.Log(X + " " + Y);
    //}
}
