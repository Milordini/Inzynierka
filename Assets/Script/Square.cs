using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    [SerializeField]int x;
    [SerializeField]int y;

    public void setX(int x) { this.x = x; }
    public int getX() { return x; }
    public void setY(int y) { this.y = y;}
    public int getY() { return y; }

}
