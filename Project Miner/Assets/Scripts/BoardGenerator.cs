using System.Collections.Generic;
using UnityEngine;
public class BoardGenerator : MonoBehaviour
{
    #region Private Variables
    [Header("Board References")]
    [SerializeField]
    private GameObject BoxPrefab;
    [SerializeField]
    private GameObject CellPrefab;
    [SerializeField]
    private BoardDataScriptable boardData;
    [SerializeField]
    private Transform cellsParent;
    [SerializeField]
    private Transform wallsParent;


    private int cellCount;
    private float cellWidth;
    private float wallsWidth;
    private float cellPadding;
    private GameObject[] walls;
    private GameObject[,] cells;
    //private List<List<Cells>> cells = new List<List<Cells>>();
    private GameObject ground;

    #endregion

    #region Unity Methods
    private void Awake()
    {
        //assignment of data from scriptable object
        cellCount = boardData.CellCount;
        cellWidth = boardData.CellWidth;
        cellPadding = boardData.CellPadding;
        wallsWidth = boardData.WallsWidth;

        //initialization of arrays
        walls = new GameObject[4];
        cells = new GameObject[cellCount, cellCount];
    }
    [ContextMenu("generate board")]
    private void Start()
    {
        ClearBoard();
        SetGround();
        SetWalls();
        SetCells();
    }
    #endregion
    #region Private Methods
    /// <summary>
    /// Function used for clearing existing board objects.
    /// </summary>
    private void ClearBoard()
    {
        foreach(Transform child in cellsParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wallsParent)
        {
            Destroy(child.gameObject);
        }
        Destroy(ground);
        walls = new GameObject[4];
        cells = new GameObject[cellCount, cellCount];
        //cells = new List<List<Cells>>();
    }
    /// <summary>
    /// Find position of cellsparent. Then spawns cells and set localpositions.
    /// </summary>
    private void SetCells()
    {
        float pos = 0;
        if (cellCount % 2 == 0)
        {
            pos = (cellCount - 1) * (cellWidth + cellPadding) / 2;
        }
        else
        {
            pos = (((cellCount - 1) / 2) * cellPadding) + ((cellCount - 1) * cellWidth / 2);
        }

        cellsParent.transform.localPosition = new Vector3(-pos, 0, -pos);

        for (int i = 0; i < cellCount; i++)
        {
            for (int j = 0; j < cellCount; j++)
            {
                cells[i, j] = Instantiate(CellPrefab, cellsParent);
                cells[i, j].transform.localPosition = new Vector3(i * (cellPadding + cellWidth), 1, j * (cellPadding + cellWidth));
                //cells[i, j].transform.localScale = new Vector3(cellWidth, cellWidth, cellWidth);
            }
        }
    }
    /// <summary>
    /// Spawn and arrange Walls in WallsParent
    /// </summary>
    private void SetWalls()
    {
        for (int i = 0; i < 4; i++)
        {
            walls[i] = Instantiate(BoxPrefab, wallsParent);
            walls[i].name = $"wall_{i}";
        }
        float wallDimension1, wallDimension2;
        //wallDimension1 & 2 are repeating values to be used in  X,Y coordinates of wall positions
        wallDimension1 = ((cellCount * cellWidth) + ((cellCount + 1) * cellPadding) + (wallsWidth)) / 2.0f;
        wallDimension2 = wallsWidth / 2.0f;

        //left wall
        walls[0].transform.localPosition = new Vector3(-wallDimension1, 1, wallDimension2);
        walls[0].transform.localScale = new Vector3(wallsWidth, 1, wallDimension1 * 2);

        //right wall
        walls[1].transform.localPosition = new Vector3(wallDimension1, 1, -wallDimension2);
        walls[1].transform.localScale = new Vector3(wallsWidth, 1, wallDimension1 * 2);

        //top wall
        walls[2].transform.localPosition = new Vector3(wallDimension2, 1, wallDimension1);
        walls[2].transform.localScale = new Vector3(wallDimension1 * 2, 1, wallsWidth);

        //bottom wall
        walls[3].transform.localPosition = new Vector3(-wallDimension2, 1, -wallDimension1);
        walls[3].transform.localScale = new Vector3(wallDimension1 * 2, 1, wallsWidth);
    }
    /// <summary>
    /// Spawn and Arrange Ground
    /// </summary>
    private void SetGround()
    {
        ground = Instantiate(BoxPrefab, transform);
        ground.name = "Ground";
        var groundLength = (cellCount * cellWidth) + ((cellCount + 1) * cellPadding) + (2 * wallsWidth);
        ground.transform.localScale = new Vector3(groundLength, 1, groundLength);
        ground.transform.localPosition = Vector3.zero;
    }
    #endregion
}
