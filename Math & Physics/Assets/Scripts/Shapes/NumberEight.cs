using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberEight : DrawingObject
{
    public override void Initalize(Vector3 origin, Vector3 scale)
    {
        Lines.Clear();
        Location = origin;
        Scale = scale;
        Color colorChoice = Color.magenta;

        // 8 Segment Display (10,20)
        Lines.Add(new Line(new Vector2(0, 0), new Vector2(10, 0), colorChoice));
        Lines.Add(new Line(new Vector2(10, 0), new Vector2(10, 10), colorChoice));
        Lines.Add(new Line(new Vector2(0, 0), new Vector2(0, 10), colorChoice));
        Lines.Add(new Line(new Vector2(10, 10), new Vector2(0, 10), colorChoice));
        Lines.Add(new Line(new Vector2(0, 10), new Vector2(0, 20), colorChoice));
        Lines.Add(new Line(new Vector2(10, 10), new Vector2(10, 20), colorChoice));
        Lines.Add(new Line(new Vector2(0, 20), new Vector2(10, 20), colorChoice));
    }
}
