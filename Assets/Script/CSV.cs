using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV
{
    public string Algorithm;
    public int Tryb;
    public Square Start;
    public Square End;
    public int Traced;
    public int pathLenght;
    public double Time;

    public CSV(string algorithm, int tryb, Square start, Square end, int traced, int pathLenght, long time)
    {
        Algorithm = algorithm;
        Tryb = tryb;
        Start = start;
        End = end;
        Traced = traced;
        this.pathLenght = pathLenght;
        Time = time / 1000f;
    }

    public string toCSV()
    {
        return Algorithm + ";" + Tryb + ";" + Start.X + "," + Start.Y + ";" + End.X + "," + End.Y + ";" + Traced + ";" + pathLenght + ";" + Time;
    }
}
