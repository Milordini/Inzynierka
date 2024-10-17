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
    [SerializeField] public int G { get; set; }
    [SerializeField] public int H { get; set; }
    [SerializeField] public int F { get { return G + H; } }

    [SerializeField] public float distance { get; set; }
