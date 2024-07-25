using NUnit.Framework.Internal.Execution;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] bool canWalk;
    public void setX(int x) { this.x = x; }
    public int getX() { return x; }
    public void setY(int y) { this.y = y;}
    public int getY() { return y; }
    public bool getCanWalk() {  return canWalk; }
    public void setCanWalk(bool w) { canWalk = w; }
}
