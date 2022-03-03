using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : DrawingObject
{
    public override void Initalize(Vector3 origin, Vector3 scale)
    {
        Lines.Clear();
        Location = origin;
        Scale = scale;
        Color colorChoice = Color.cyan;

        Vector3 pA = Location + new Vector3(-6, 0, 0);
        Vector3 pB = DrawingTools.RotatePoint(Location, 60f, pA);
        Vector3 pC = DrawingTools.RotatePoint(Location, 60f, pB);
        Vector3 pD = DrawingTools.RotatePoint(Location, 60f, pC);
        Vector3 pE = DrawingTools.RotatePoint(Location, 60f, pD);
        Vector3 pF = DrawingTools.RotatePoint(Location, 60f, pE);

        Lines.Add(new Line(pA, pB, colorChoice));
        Lines.Add(new Line(pB, pC, colorChoice));
        Lines.Add(new Line(pC, pD, colorChoice));
        Lines.Add(new Line(pD, pE, colorChoice));
        Lines.Add(new Line(pE, pF, colorChoice));
        Lines.Add(new Line(pF, pA, colorChoice));
    }
}
