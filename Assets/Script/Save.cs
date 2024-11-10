using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save
{
    public string path {  get; private set; }
    public string Name { get; private set; }
    public int Height {  get; private set; } 
    public int Width { get; private set; }

    public Save(string name, string height, string width)
    {
        this.path = name;
        this.Name = name.Substring(name.IndexOf('\\')+1, (name.IndexOf('.')) - (name.IndexOf('\\') + 1));
        this.Height = int.Parse(height.Substring(height.IndexOf(' ')));
        this.Width = int.Parse(width.Substring(width.IndexOf(' ')));
    }

}
