﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing.Glint;

public class DrawingObject
{
    // Default Variables
    public bool PerformDraw = true;
    // public float Roation = 0;
    public Vector3 Scale = Vector3.zero;
    public Vector3 Location = Vector3.zero;
    public List<Line> Lines = new List<Line>();

    /// <summary>
    /// Override to draw. It's really annoying.
    /// </summary>
    /// <param name="origin"></param>
    /// <param name="scale"></param>
    public virtual void Initalize(Vector3 origin, Vector3 scale)
    {
        // In most initialize scripts, this is where the drawing commands are placed.
    }

    public virtual void Update()
    {
        if (PerformDraw)
        {
            Draw(); // It auto-draws
        }
    }

    /// <summary>
    /// Draws the Drawing Object Instance
    /// </summary>
    /// <param name="grid">Optional, When a Grid2d is applied, object is drawn relative to the grid and location is in Grid space</param>
    public virtual void Draw(Grid2D grid = null)
    {
        if (Lines.Count != 0)
        {
            for (int i = 0; i < Lines.Count; i++)
            {
                Lines[i].Draw(grid);
            }
        }
    }
}
