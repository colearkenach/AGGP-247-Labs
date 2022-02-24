using System;
using System.Collections.Generic;
using UnityEngine;

public class LetterE : DrawingObject
{
    public override void Initalize()
    {
        Lines.Clear();
        gridObject = GameObject.FindObjectOfType<Grid2d>();     // Ensure Grid2d Script is being read

        Scale = new Vector3(30f, 60f);
        Location = new Vector3(gridObject.newOrigin.x + 8f, gridObject.newOrigin.y - 20.5f);
        Color colorChoice = Color.blue;

        Draw(gridObject);

        // Draw E on the grid
        Lines.Add(new Line(new Vector3(Location.x, Location.y), new Vector3(Location.x + Scale.x, Location.y), colorChoice));
        Lines.Add(new Line(new Vector3(Location.x, Location.y), new Vector3(Location.x, Location.y + Scale.x), colorChoice));
        Lines.Add(new Line(new Vector3(Location.x + Scale.x, Location.y + Scale.x), new Vector3(Location.x, Location.y + Scale.x), colorChoice));
        Lines.Add(new Line(new Vector3(Location.x, Location.y + Scale.x), new Vector3(Location.x, Location.y + Scale.y), colorChoice));
        Lines.Add(new Line(new Vector3(Location.x, Location.y + Scale.y), new Vector3(Location.x + Scale.x, Location.y + Scale.y), colorChoice));
    }
}