using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lab01 : MonoBehaviour
{
    public class Grid2D
    {
        public Vector3 screenSize;
        public Vector3 origin;

        public float gridSize = 10f;
        public float minGridSize = 2f;
        public float originSize = 20f;

        public int divisionCount = 5;
        public int minDivisionCount = 2;
    }

    [Header("Default Variables")]
    public Color axisColor = Color.white;
    public Color lineColor = Color.gray;
    public Color divisionColor = Color.yellow;

    public bool isDrawingOrigin = false;
    public bool isDrawingAxis = true;
    public bool isDrawingDivisions = true;

    Grid2D grid = new Grid2D();

    [Header("New Variables")]
    public float xStart;                // Line start (X)
    public float yStart;                // Line start (Y)
    public Vector3 originPoint;         // X: 0, Y: 0 (but not really)

    public int divisionEditor;          // Editable division count.
    public float gridSizeEditor;        // Editable grid size.


    void Start()
    {
        grid.origin = new Vector3(Screen.width / 2, Screen.height / 2);
        grid.screenSize = new Vector3(Screen.width, Screen.height);

        divisionEditor = grid.divisionCount;
        gridSizeEditor = grid.gridSize;
    }

    void Update()
    {
        originPoint = ScreenToGrid(grid.origin);

        xStart = (originPoint.x - grid.divisionCount) * grid.gridSize;
        yStart = (originPoint.y + grid.divisionCount) * grid.gridSize;

        GridMovement();
        ScaleGrid();

        //X Axis
        for (int i = 0; i <= divisionEditor * gridSizeEditor * 2; i++)
        {
            Color newColor = lineColor;

            float xPos = (xStart + i) * 2;

            if (xPos == originPoint.x && isDrawingAxis)
            {
                newColor = axisColor;
            }
            else if (xPos % 5 == 0 && isDrawingDivisions)
            {
                newColor = divisionColor;
            }

            DrawLine(new Line(new Vector3(xPos, yStart), new Vector3(xPos, originPoint.y - divisionEditor * gridSizeEditor), newColor));
        }

        //Y Axis
        for (int i = 0; i <= divisionEditor * gridSizeEditor * 2; i++)
        {
            Color newColor = lineColor;

            float yPos = (yStart - i) * 2;

            if (yPos == originPoint.x && isDrawingAxis)
            {
                newColor = axisColor;
            }
            else if (yPos % 5 == 0 && isDrawingDivisions)
            {
                newColor = divisionColor;
            }

            DrawLine(new Line (new Vector3(xStart, yPos), new Vector3(originPoint.x + divisionEditor * gridSizeEditor, yPos), newColor));
        }

        if (isDrawingOrigin)
        {
            DrawOrigin();
        }
    }


    /// Takes the potential grid space and outputs it into screen space
    public Vector3 GridToScreen(Vector3 gridSpace)
    {
        float screenPosX = gridSpace.x * gridSizeEditor + grid.origin.x;
        float screenPosY = gridSpace.y * gridSizeEditor + grid.origin.y;

        return new Vector3(screenPosX, screenPosY);
    }

    /// Takes in screen space and outputs it as grid space
    public Vector3 ScreenToGrid(Vector3 screenSpace)
    {
        float gridPosX = (screenSpace.x - grid.origin.x) / gridSizeEditor;
        float gridPosY = (screenSpace.y - grid.origin.y) / gridSizeEditor;

        return new Vector3(gridPosX, gridPosY);
    }

    /// Draws the given line
    public void DrawLine(Line line)
    {
        // May need to delete later? Said in instructions to use later.
        Line drawLine = new Line(GridToScreen(line.start), GridToScreen(line.end), line.color);

        Glint.AddCommand(drawLine);
    }

    /// Draws the Origin Point (or Symbol)
    public void DrawOrigin()
    {
        Vector3 up = new Vector3(originPoint.x, (originPoint.y + 0.2f)) * gridSizeEditor;
        Vector3 down = new Vector3(originPoint.x, (originPoint.y - 0.2f)) * gridSizeEditor;
        Vector3 left = new Vector3((originPoint.x - 0.2f), originPoint.y) * gridSizeEditor;
        Vector3 right = new Vector3((originPoint.x + 0.2f), originPoint.y) * gridSizeEditor;

        DrawLine(new Line(up, left, Color.blue));
        DrawLine(new Line(left, down, Color.blue));
        DrawLine(new Line(down, right, Color.blue));
        DrawLine(new Line(right, up, Color.blue));
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
        {
            originPoint += new Vector3(originPoint.x, originPoint.y + 5);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            originPoint += new Vector3(originPoint.x - 5, originPoint.y);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            originPoint += new Vector3(originPoint.x + 5, originPoint.y);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            originPoint += new Vector3(originPoint.x, originPoint.y - 5);
        }
    }

    public void ScaleGrid()
    {
        switch (Input.mouseScrollDelta.y)
        {
            case 1:

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    divisionEditor += 1;
                }
                else
                {
                    gridSizeEditor += 1;
                }
                break;
            case 0:
                break;
            case -1:
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    if (divisionEditor <= grid.minDivisionCount)
                    {
                        divisionEditor = grid.minDivisionCount;
                    }
                    else
                    {
                        divisionEditor -= 1;
                    }
                }
                else
                {
                    if (gridSizeEditor <= grid.minGridSize)
                    {
                        gridSizeEditor = grid.minGridSize;
                    }
                    else
                    {
                        gridSizeEditor -= 1;
                    }
                }

                break;
        }
    }
}
