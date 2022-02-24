﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing.Glint;

public class DrawingObject : MonoBehaviour
{
    // Default Variables
    public bool PerformDraw = true;
    public float Roation = 0;
    public Vector3 Scale = Vector3.zero;
    public Vector3 Location = Vector3.zero;
    public List<ICommandInstruction> Lines = new List<ICommandInstruction>();

    public Grid2d gridObject;

    public void Start()
    {
        Initalize();
    }

    public virtual void Initalize()
    {

    }

    public virtual void Update()
    {
        if (PerformDraw)
        {
            Draw();
        }
    }

    /// <summary>
    /// Draws the Drawing Object Instance
    /// </summary>
    /// <param name="grid">Optional, When a Grid2d is applied, object is drawn relative to the grid and location is in Grid space</param>
    public virtual void Draw(Grid2d grid = null)
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
