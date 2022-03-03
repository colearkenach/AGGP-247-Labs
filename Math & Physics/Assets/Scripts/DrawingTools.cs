using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This entire script is basically just a long string of math. Follow the comments carefully to not get lost.
/// </summary>
public class DrawingTools : MonoBehaviour
{
    /// <summary>
    /// Takes the potential grid space and outputs it into screen space. This was moved out of the "Grid2d.cs" file due to needing to be used in other scripts.
    /// </summary>
    /// <param name="gridSpace"></param>
    /// <param name="grid"></param>
    /// <returns></returns>
    public static Vector3 GridToScreen(Vector3 gridSpace, Grid2D grid)
    {
        float screenPosX = gridSpace.x * grid.gridSize + grid.origin.x;
        float screenPosY = gridSpace.y * grid.gridSize + grid.origin.y;

        return new Vector3(screenPosX, screenPosY);
    }

    /// <summary>
    /// Takes in screen space and outputs it as grid space. As above, copied from "Grid2d.cs"
    /// </summary>
    /// <param name="screenSpace"></param>
    /// <param name="grid">Added varaible so the grid.origin can be found. Oh well.</param>
    /// <returns></returns>
    public Vector3 ScreenToGrid(Vector3 screenSpace, Grid2D grid)
    {
        float gridPosX = (screenSpace.x - grid.origin.x) / grid.gridSize;
        float gridPosY = (screenSpace.y - grid.origin.y) / grid.gridSize;

        return new Vector3(gridPosX, gridPosY);
    }

    /// <summary>
    /// V3ToAngle
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    /// <returns></returns>
    public static float V3ToAngle(Vector3 start, Vector3 end)
    {
        float angle = Mathf.Atan2(end.y - start.y, end.x - start.x);    // Determine angle

        return angle * (180 / Mathf.PI);    // Angle value is multiplied by 180 * Pi (AKA ~565.48)
    }

    /// <summary>
    /// Converts line's angle to a quantifiable float value.
    /// </summary>
    /// <param name="line"></param>
    /// <returns></returns>
    public static float LineToAngle(Line line)
    {
        float lineValue = V3ToAngle(line.start, line.end);

        return lineValue;
    }

    /// <summary>
    /// Rotate a thing around another thing designated as the center. Straightforward name, not so straightforward execution.
    /// </summary>
    /// <param name="Center"></param>
    /// <param name="angle"></param>
    /// <param name="pointIN"></param>
    /// <returns></returns>
    public static Vector3 RotatePoint(Vector3 center, float angle, Vector3 centerPoint)
    {
        float radius = Mathf.Deg2Rad * angle;

        float rotX = (centerPoint.x - center.x) * Mathf.Cos(radius) - (centerPoint.y - center.y) * Mathf.Sin(radius);
        float rotY = (centerPoint.x - center.x) * Mathf.Sin(radius) + (centerPoint.y - center.y) * Mathf.Cos(radius);

        Vector3 outerPoint = new Vector3(center.x + rotX, center.y + rotY);

        return outerPoint;
    }

    /// <summary>
    /// Circle shit, calculate to degrees. Really small.
    /// </summary>
    /// <param name="radian"></param>
    /// <returns></returns>
    public static float ToDegrees(float radian)
    {
        float degrees = (radian * Mathf.Rad2Deg);
        return degrees;
    }

    /// <summary>
    /// Circle shit, calculate to radians. Really small, just like calculating to degrees.
    /// </summary>
    /// <param name="degree"></param>
    /// <returns></returns>
    public static float ToRadians(float degree)
    {
        float radians = (degree * Mathf.Deg2Rad);
        return radians;
    }

    /// <summary>
    /// Find a point on a circle given radius and angle. Used in the circle drawing script.
    /// </summary>
    /// <param name="Origin"></param>
    /// <param name="angle"></param>
    /// <param name="radius"></param>
    /// <returns></returns>
    public static Vector3 CircleRadiusPoint(Vector3 Origin, float angle, float radius)
    {
        Vector3 circRadPoint = new Vector3(Origin.x + radius * Mathf.Cos(ToRadians(angle)),
            Origin.y + radius * Mathf.Sin(ToRadians(angle)));
        return circRadPoint;
    }

    /// <summary>
    /// As the cricle radius finder above, and is used in the ellipse drawing script. Fun stuff.
    /// </summary>
    /// <param name="Origin"></param>
    /// <param name="angle"></param>
    /// <param name="Axis"></param>
    /// <returns></returns>
    public static Vector3 EllipseRadiusPoint(Vector3 Origin, float angle, Vector3 Axis)
    {
        Vector3 ellRadPoint = new Vector3(Origin.x + Axis.x * Mathf.Cos(ToRadians(angle)),
            Origin.y + Axis.y * Mathf.Sin(ToRadians(angle)));
        return ellRadPoint;
    }

    /// <summary>
    /// Draws a circle. It works okay-ish.
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Radius"></param>
    /// <param name="Sides"></param>
    /// <param name="color"></param>
    public static void DrawCircle(Vector3 Position, float Radius, int Sides, Color color)
    {
        int adjustedSides;

        // Ensure sides don't go below 3 for jank reasons. Don't wanna break things.
        if (Sides > 3)
            adjustedSides = Sides;
        else
            adjustedSides = 3;

        float angle = 360f / adjustedSides;

        Vector3 point = CircleRadiusPoint(Position, 0, Radius);

        for (int i = 1; i <= Sides; i++)
        {
            Vector3 nextPoint = CircleRadiusPoint(Position, angle * i, Radius);
            Line circleSide = new Line(point, nextPoint, color);

            Glint.AddCommand(circleSide);
            point = nextPoint;
        }
    }

    /// <summary>
    /// Draws an ellipse. Just like the circle function, works okay.
    /// </summary>
    /// <param name="Position"></param>
    /// <param name="Axis"></param>
    /// <param name="Sides"></param>
    /// <param name="color"></param>
    public static void DrawEllipse(Vector3 Position, Vector2 Axis, int Sides, Color color)
    {
        int adjustedSides;

        // Ensure sides don't go below 3 for jank reasons.
        if (Sides > 3)
            adjustedSides = Sides;
        else
            adjustedSides = 3;

        float angle = 360f / adjustedSides;

        Vector3 point = EllipseRadiusPoint(Position, 0, Axis);

        for (int i = 1; i <= Sides; i++)
        {
            Vector3 nextPoint = EllipseRadiusPoint(Position, angle * i, Axis);
            Line circleSide = new Line(point, nextPoint, color);

            Glint.AddCommand(circleSide);
            point = nextPoint;
        }
    }
}
