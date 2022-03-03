using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing.Glint;

public class Grid2D
{
    public Vector3 screenSize;
    public Vector3 origin;

    public float gridSize = 10f;
    public float minGridSize = 2f;
    public float originSize = 20f;

    public int divisionCount = 5;
    public int minDivisionCount = 2;

    // Draws lines on the screen, assuming it works.
    public void DrawLine(Line line)
    {
        Vector3 start = DrawingTools.GridToScreen(line.start, this);
        Vector3 end = DrawingTools.GridToScreen(line.end, this);
        Line screenLine = new Line(start, end, line.color);

        Glint.AddCommand(screenLine);
    }

    // Drawn Object, use to draw objects on screen. Actually works somehow when in this class. Don't remove.
    public void DrawObject(DrawingObject drawObj)
    {
        drawObj.Draw(this);
    }
}

public class Grid2d : MonoBehaviour
{
    [Header("Default Variables")]
    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;
    Color the_;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;

    public Grid2D grid = new Grid2D();

    [Header("New Variables")]
    public float xStart;                // Line start (X)
    public float yStart;                // Line start (Y)
    public Vector3 originPoint;         // X: 0, Y: 0 (but not really)

    [Header("Switches")]
    public bool drawEight = false;
    public bool drawCircle = false;
    public bool drawEllipse = false;
    public bool drawFlyingDiamond = false;
    public bool drawHexagon = false;
    public bool parabolaOne = false;
    public bool parabolaTwo = false;
    public bool parabolaThree = false;
    public bool parabolaFour = false;

    [Header("Shapes")]
    public Circle circle = new Circle(); 
    public Ellipse ellipse = new Ellipse();
    public Diamond diamond = new Diamond();
    public Diamond rotateDiamond = new Diamond();
    public Hexagon hex = new Hexagon();
    public NumberEight eight = new NumberEight();


    public void Awake()
    {
        grid.origin = new Vector3(Screen.width / 2, Screen.height / 2);
        grid.screenSize = new Vector3(Screen.width, Screen.height);

        // Initialize objects and their attributes so they'll appear on the grid
        diamond.Initalize(originPoint, new Vector3(1, 1, 0) * grid.gridSize);
        rotateDiamond.Initalize(originPoint + new Vector3(0, 7.5f, 0) / (grid.gridSize / 2), new Vector3(1, 1, 0) * grid.gridSize);
        hex.Initalize(originPoint + new Vector3(20, -30, 0), new Vector3(1, 1, 0) * grid.gridSize);
        eight.Initalize(originPoint + new Vector3(48, -30, 0), new Vector3(1, 1, 0) * grid.gridSize);
    }

    public void Update()
    {
        originPoint = ScreenToGrid(grid.origin);

        xStart = (originPoint.x - grid.divisionCount) * grid.gridSize;
        yStart = (originPoint.y + grid.divisionCount) * grid.gridSize;

        TheGrid();          // Builds the grid space.
        GridMovement();     // Allows movement in the grid space.
        ScaleGrid();        // Allows you to zoom in and out of grid space.

        if (isDrawingOrigin)
            grid.DrawObject(diamond);

        if (drawCircle)
            DrawCircle();

        if (drawEllipse)
            DrawEllipse();

        if (drawHexagon)
            DrawHex();

        if (drawEight)
            DrawEight();

        if (drawFlyingDiamond)
            RotateDiamond();

        DrawParabolas();        // Don't worry, the if statements are inside this one. Nobody is escaping if statements today.
    }

    /// <summary>
    /// Creates the grid. Still very janky, work on updating this at a Soon(TM) date.
    /// </summary>
    public void TheGrid()
    {
        for (int i = 0; i <= grid.divisionCount * grid.gridSize; i++)
        {
            float xDraw = xStart + (i * 2);
            float yDraw = yStart - (i * 2);

            the_ = lineColor;   // Set the_ Color's default color.

            // Set X Axis Colors
            if (xDraw == originPoint.x && isDrawingAxis)
                the_ = axisColor;
            else if (xDraw % 5 == 0 && isDrawingDivisions)
                the_ = divisionColor;

            // Set Y Axis Colors
            if (yDraw == originPoint.x && isDrawingAxis)
                the_ = axisColor;
            else if (yDraw % 5 == 0 && isDrawingDivisions)
                the_ = divisionColor;


            // Draw X Axis
            grid.DrawLine(new Line(new Vector3(xDraw, yStart), new Vector3(xDraw, originPoint.y - grid.divisionCount * grid.gridSize), the_));

            // Draw Y Axis
            grid.DrawLine(new Line(new Vector3(xStart, yDraw), new Vector3(originPoint.x + grid.divisionCount * grid.gridSize, yDraw), the_));
        }
    }

    /// <summary>
    /// Draws a circle. Not an oval or a sphere, but a circle.
    /// </summary>
    public void DrawCircle()
    {
        DrawingTools.DrawCircle(DrawingTools.GridToScreen(originPoint + new Vector3(-5, -10), grid), 50, 20, Color.red);
        circle.Draw();
    }

    /// <summary>
    /// Draws an ellipse. Seriously, who uses these?
    /// </summary>
    public void DrawEllipse()
    {
        DrawingTools.DrawEllipse(DrawingTools.GridToScreen(originPoint + new Vector3(20, 25), grid), new Vector3(75, 135), 12, Color.black);
        ellipse.Draw();
    }

    /// <summary>
    /// Draws a hexagon. Y'know, a hexagon. Hexa. 5 sides.
    /// </summary>
    public void DrawHex()
    {
        grid.DrawObject(hex);
    }

    /// <summary>
    /// Draws an eight on the screen. Self-explanitory.
    /// </summary>
    public void DrawEight()
    {
        grid.DrawObject(eight);
    }

    // Only still here for ONE thing. Wish I could delete it.
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        float gridPosX = (screenSpace.x - grid.origin.x) / grid.gridSize;
        float gridPosY = (screenSpace.y - grid.origin.y) / grid.gridSize;

        return new Vector3(gridPosX, gridPosY);
    }

    // Stub code, didn't work previously. Gonna retry soon.
    public void RotateDiamond()
    {
        rotateDiamond.Initalize(DrawingTools.RotatePoint(originPoint, -200f * Time.deltaTime, rotateDiamond.Location), Vector3.one * grid.gridSize);
        grid.DrawObject(rotateDiamond);
        Debug.Log(rotateDiamond.Location);
    }

    /// Draws the given line
    public void DrawLine(Line line, bool DrawOnGrid = true)
    {
        // May need to delete later? Said in instructions to use later.
        Line drawLine = new Line(DrawingTools.GridToScreen(line.start, grid), DrawingTools.GridToScreen(line.end, grid), line.color);

        Glint.AddCommand(drawLine);
    }

    // Individually finds if switch is on, then draws parabola. Yay.
    public void DrawParabolas()
    {
        if (parabolaOne)
            Parabola1(xStart);

        if (parabolaTwo)
            Parabola2(xStart);
        
        if (parabolaThree)
            Parabola3(xStart);
        
        if (parabolaFour)
            Parabola4(yStart);
    }

    public float ScaleGrid2Screen(float value)
    {
        return (value * grid.gridSize);
    }

    public float ScaleScreen2Grid(float value)
    {
        return (value / grid.gridSize);
    }

    public void GridMovement()
    {
        // Toggle Draw Origin
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (isDrawingOrigin == true)
            {
                isDrawingOrigin = false;
            }
            else
            {
                isDrawingOrigin = true;
            }
        }

        // Toggle Draw Axis
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (isDrawingAxis == true)
            {
                isDrawingAxis = false;
            }
            else
            {
                isDrawingAxis = true;
            }
        }

        // Toggle Draw Division
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (isDrawingDivisions == true)
            {
                isDrawingDivisions = false;
            }
            else
            {
                isDrawingDivisions = true;
            }
        }

        // Movement Controls
        if (Input.GetKeyDown(KeyCode.UpArrow))
            grid.origin = new Vector3(grid.origin.x, grid.origin.y + 30 * 0.2f);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            grid.origin = new Vector3(grid.origin.x - 30 * 0.2f, grid.origin.y);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            grid.origin = new Vector3(grid.origin.x + 30 * 0.2f, grid.origin.y);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            grid.origin = new Vector3(grid.origin.x, grid.origin.y - 30 * 0.2f);
    }

    public void ScaleGrid()
    {
        switch (Input.mouseScrollDelta.y)
        {
            case 1:
                grid.gridSize += 2f;

                break;
            case 0:
                break;
            case -1:
                grid.gridSize -= 2f;

                if (grid.gridSize < grid.minGridSize)
                    grid.gridSize = grid.minGridSize;
                break;
        }

        if (Input.GetKeyDown(KeyCode.Comma))
        {
            grid.divisionCount += 1;
        }

        if (Input.GetKeyDown(KeyCode.Period))
        {
            grid.divisionCount -= 1;

            if (grid.divisionCount < grid.minDivisionCount)
                grid.divisionCount = grid.minDivisionCount;
        }
    }

    // Parabola ONE
    public void Parabola1(float gridPoint)
    {
        Vector3 origin = new Vector3(gridPoint, Mathf.Pow(gridPoint, 2));

        for (int i = 1; i <= grid.divisionCount * grid.gridSize * 2; i++)
        {
            float xPos = gridPoint + (i);
            float yPos = Mathf.Pow(xPos, 2);

            Vector3 destination = new Vector3(xPos, yPos);
            grid.DrawLine(new Line(origin, destination, Color.red));

            origin = destination;
        }
    }

    // Parabola TWO
    public void Parabola2(float gridPoint)
    {
        Vector3 origin = new Vector3(gridPoint, Mathf.Pow(gridPoint, 2) + (gridPoint * 2) + 1);

        for (int i = 1; i <= grid.divisionCount * grid.gridSize * 2; i++)
        {
            float xPos = gridPoint + (i);
            float yPos = Mathf.Pow(xPos, 2) + (xPos * 5) + 1;

            Vector3 destination = new Vector3(xPos, yPos);
            grid.DrawLine(new Line(origin, destination, Color.black));

            origin = destination;
        }
    }

    // PARABOLA three
    public void Parabola3(float gridPoint)
    {
        Vector3 origin = new Vector3(gridPoint, (-2 * Mathf.Pow(gridPoint, 2)) + (10 * gridPoint) + 12);

        for (int i = 1; i <= grid.divisionCount * grid.gridSize * 2; i++)
        {
            float xPos = gridPoint + (i);
            float yPos = (-2 * Mathf.Pow(xPos,2)) + (10 * xPos) + 12;

            Vector3 destination = new Vector3(xPos, yPos);
            grid.DrawLine(new Line(origin, destination, Color.black));

            origin = destination;
        }
    }

    // Parabola four
    public void Parabola4(float gridPoint)
    {
        Vector3 origin = new Vector3(Mathf.Pow(-gridPoint, 3), gridPoint);

        for (int i = 1; i <= grid.divisionCount * grid.gridSize * 2; i++)
        {
            float yPos = gridPoint - (i);
            float xPos = Mathf.Pow(-yPos, 3);

            Vector3 destination = new Vector3(xPos, yPos);
            grid.DrawLine(new Line(origin, destination, Color.red));

            origin = destination;
        }
    }
}
