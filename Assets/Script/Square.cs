using NUnit.Framework.Internal.Execution;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] public int X { get; set; }
    [SerializeField] public int Y { get; set; }
    [SerializeField] public bool canWalk { get; set; }
    [SerializeField] public Square parent { get; set; }
    [SerializeField] public int G { get; set; }
    [SerializeField] public int H { get; set; }
    [SerializeField] public int F { get { return G + H; } }

  

    //public static bool operator == (Square a, Square b)
    //{
    //    if(b!=null)
    //        return (a.X == b.X && a.Y == b.Y);
    //}
    //public static bool operator !=(Square a, Square b)
    //{
    //    return (a.X != b.X || a.Y != b.Y);
    //}
    //public override bool Equals(object obj)
    //{
    //    return obj is Square square &&
    //           base.Equals(obj) &&
    //           name == square.name &&
    //           hideFlags == square.hideFlags &&
    //           EqualityComparer<Transform>.Default.Equals(transform, square.transform) &&
    //           EqualityComparer<GameObject>.Default.Equals(gameObject, square.gameObject) &&
    //           tag == square.tag &&
    //           enabled == square.enabled &&
    //           isActiveAndEnabled == square.isActiveAndEnabled &&
    //           EqualityComparer<CancellationToken>.Default.Equals(destroyCancellationToken, square.destroyCancellationToken) &&
    //           useGUILayout == square.useGUILayout &&
    //           didStart == square.didStart &&
    //           didAwake == square.didAwake &&
    //           runInEditMode == square.runInEditMode &&
    //           X == square.X &&
    //           Y == square.Y &&
    //           canWalk == square.canWalk &&
    //           EqualityComparer<Square>.Default.Equals(parent, square.parent) &&
    //           G == square.G &&
    //           H == square.H &&
    //           F == square.F;
    //}

    //public override int GetHashCode()
    //{
    //    HashCode hash = new HashCode();
    //    hash.Add(base.GetHashCode());
    //    hash.Add(name);
    //    hash.Add(hideFlags);
    //    hash.Add(transform);
    //    hash.Add(gameObject);
    //    hash.Add(tag);
    //    hash.Add(enabled);
    //    hash.Add(isActiveAndEnabled);
    //    hash.Add(destroyCancellationToken);
    //    hash.Add(useGUILayout);
    //    hash.Add(didStart);
    //    hash.Add(didAwake);
    //    hash.Add(runInEditMode);
    //    hash.Add(X);
    //    hash.Add(Y);
    //    hash.Add(canWalk);
    //    hash.Add(parent);
    //    hash.Add(G);
    //    hash.Add(H);
    //    hash.Add(F);
    //    return hash.ToHashCode();
    //}
}
