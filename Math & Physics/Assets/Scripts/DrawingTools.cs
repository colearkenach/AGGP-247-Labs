using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingTools : MonoBehaviour
{
    public static float V3ToAngle(Vector3 startPoint, Vector3 endPoint)
    {
        float anglRadians;
        
        anglRadians = Mathf.Atan2(endPoint.y - startPoint.y, endPoint.x - startPoint.x);

        return anglRadians * (180 / Mathf.PI);
    }

    public static float LineToAngle(Line line)
    {
        float lineAngleDeg = V3ToAngle(line.start, line.end);

        return lineAngleDeg;
    }

    public static Vector3 RotatePoint(Vector3 Center, float angle, Vector3 pointIN)
    {
        Vector3 outPoint;
        float rotX;
        float rotY;

        rotX = (pointIN.x * Mathf.Cos(angle)) - (pointIN.y * Mathf.Sin(angle));
        rotY = (pointIN.x * Mathf.Sin(angle)) + (pointIN.y * Mathf.Cos(angle));

        outPoint = new Vector3(Center.x + rotX, Center.y + rotY);

        return outPoint;
    }

    public static float ToDegrees(float radian)
    {
        float degreeResult = radian * Mathf.Rad2Deg;

        return degreeResult;
    }

    public static float ToRadians(float degree)
    {
        float radianResult = degree * Mathf.Deg2Rad;

        return radianResult;
    }

    public static Vector3 CircleRadiusPoint(Vector3 Origin, float angle, float radius)
    {
        Vector3 circlePoint = new Vector3(Origin.x + radius * Mathf.Cos(ToRadians(angle)), Origin.y + radius * Mathf.Sin(ToRadians(angle)));

        return circlePoint;
    }

    public static void DrawCircle(Vector3 Position, float Radius, int Sides, Color color)
    {
        int adjustedSides;
        if (Sides > 3)
        {
            adjustedSides = Sides;
        }
        else
        {
            adjustedSides = 3;
        }

        float incrementAngle = 360f / adjustedSides;

        Vector3 lastPoint = CircleRadiusPoint(Position, 0, Radius);

        for (int i = 1; i <= Sides; i++)
        {
            Vector3 nextPoint = CircleRadiusPoint(Position, incrementAngle * i, Radius);

            Line circleSide = new Line(lastPoint, nextPoint, color);

            Glint.AddCommand(circleSide);

            lastPoint = nextPoint;
        }
    }

    public static Vector3 EllipseRadiusPoint(Vector3 Origin, float angle, Vector3 Axis)
    {
        Vector3 ellipsePoint = new Vector3(Origin.x + Axis.x * Mathf.Cos(ToRadians(angle)), Origin.y + Axis.y * Mathf.Sin(ToRadians(angle)));

        return ellipsePoint;
    }

    public static void DrawEllipse(Vector3 Position, Vector2 Axis, int Sides, Color color)
    {
        int adjustedSides;
        if (Sides > 3)
        {
            adjustedSides = Sides;
        }
        else
        {
            adjustedSides = 3;
        }

        float incrementAngle = 360f / adjustedSides;

        Vector3 lastPoint = EllipseRadiusPoint(Position, 0, Axis);

        for (int i = 1; i <= Sides; i++)
        {
            Vector3 nextPoint = EllipseRadiusPoint(Position, incrementAngle * i, Axis);

            Line circleSide = new Line(lastPoint, nextPoint, color);

            Glint.AddCommand(circleSide);

            lastPoint = nextPoint;
        }
    }
}
