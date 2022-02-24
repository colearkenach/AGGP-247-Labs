using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipse : MonoBehaviour
{
    public Vector3 Position = Vector2.zero;
    public Vector3 Center = Vector2.zero;
    public Vector3 Axis = Vector2.one;
    public float Roation = 0;   // Prof. Wallek misspelled "rotation", keeping it for the funny
    public int Sides = 32;
    public float Width = 2.0f;
    public Color color = Color.white;

    public Ellipse() { }

    
    public Ellipse(Vector3 cent, Vector3 ax, float rot, int side, float wid, Color col)
    {
        Center = cent;
        Axis = ax;
        Roation = rot;
        Sides = side;
        Width = wid;
        color = col;
    }

    public virtual void Draw()
    {
        DrawingTools.DrawEllipse(Center, Axis, Sides, color);
    }
}
