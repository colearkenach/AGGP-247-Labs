using System;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : DrawingObject
{
    public override void Initalize(Vector3 origin, Vector3 scale)
    {
        Lines.Clear();
        Scale = scale;          // Controls size.
        Location = origin;      // Something is very wrong with this.
        Color colorChoice = Color.green;

        // Draw Diamond on grid -> HOLY SHIT, NOW FEATURING WORKING CODE!!!
        Lines.Add(new Line(new Vector3(Location.x * Scale.x, (Location.y + 0.2f) * Scale.y), new Vector3((Location.x - 0.2f) * Scale.x, Location.y * Scale.y), colorChoice));
        Lines.Add(new Line(new Vector3((Location.x - 0.2f) * Scale.x, Location.y * Scale.y), new Vector3(Location.x * Scale.x, (Location.y - 0.2f) * Scale.y), colorChoice));
        Lines.Add(new Line(new Vector3(Location.x * Scale.x, (Location.y - 0.2f) * Scale.y), new Vector3((Location.x + 0.2f) * Scale.x, Location.y * Scale.y), colorChoice));
        Lines.Add(new Line(new Vector3((Location.x + 0.2f) * Scale.x, Location.y * Scale.y), new Vector3(Location.x * Scale.x, (Location.y + 0.2f) * Scale.y), colorChoice));
    }
}
