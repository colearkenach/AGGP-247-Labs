using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Ellipse
{
    public float Radius
    {
        get { return Axis.x; }
        set { Axis = new Vector3(value, value); }
    }

    public override void Draw()
    {
        DrawingTools.DrawCircle(Center, Radius, Sides, color);
    }
}
