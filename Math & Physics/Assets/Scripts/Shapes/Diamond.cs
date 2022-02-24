using System;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : DrawingObject
{
    public override void Initalize()
    {
        Lines.Clear();
        gridObject = GameObject.FindObjectOfType<Grid2d>();     // Ensure Grid2d Script is being read

        Scale = new Vector3(5f, -5f);   // Controls size.
        Location = new Vector3(gridObject.newOrigin.x, gridObject.newOrigin.y);     // Something is very wrong with this.
        Color colorChoice = Color.green;

        Draw(gridObject);

        // Draw Diamond on grid -> HOLY SHIT, NOW FEATURING WORKING CODE!!!

        // Top
        Lines.Add(new Line(
            gridObject.GridToScreen(new Vector3(Scale.y, Scale.x * 2)),
            gridObject.GridToScreen(new Vector3(Scale.x + Scale.y, 3 * Scale.x)),
            colorChoice));

        // Left
        Lines.Add(new Line(
            gridObject.GridToScreen(new Vector3(Scale.x + Scale.y, 3 * Scale.x)),
            gridObject.GridToScreen(new Vector3(Scale.x, Scale.x * 2)),
            colorChoice));

        // Right
        Lines.Add(new Line(
            gridObject.GridToScreen(new Vector3(Scale.x, Scale.x * 2)),
            gridObject.GridToScreen(new Vector3(Scale.x + Scale.y, Scale.x)),
            colorChoice));

        // Bottom
        Lines.Add(new Line(
            gridObject.GridToScreen(new Vector3(Scale.x + Scale.y, Scale.x)),
            gridObject.GridToScreen(new Vector3(Scale.y, Scale.x * 2)),
            colorChoice));
    }
}
