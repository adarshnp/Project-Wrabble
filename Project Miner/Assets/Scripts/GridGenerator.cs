using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject wallsPrefab;  //expecting unit cube
    public GameObject groundPrefab; //expecting unit cube
    public GameObject tilePrefab;   //expecting unit cube

    public Transform wallsParent;
    public Transform groundParent;
    public Transform tileParent;

    public Vector2Int GridSize;
    public float TileWidth;
    public float TilePadding;
    public float WallsWidth;

    private int rows;
    private int columns;
    private const int WALL_SECTION_COUNT = 8;

    private GameObject[] walls;
    private GameObject[,] tiles;
    private GameObject ground;
    private void Start()
    {
        GenerateGrid();
    }
    private void ValuesInitialization()
    {
        //clearing spawned values
        if (tileParent.childCount != 0)
        {
            foreach (Transform child in tileParent)
            {
                Destroy(child.gameObject);
            }
        }
        if (wallsParent.childCount != 0)
        {
            foreach (Transform child in wallsParent)
            {
                Destroy(child.gameObject);
            }
        }
        if (ground != null)
        {
            Destroy(ground);
        }

        //initialization of arrays
        rows = GridSize.y;
        columns = GridSize.x;
        walls = new GameObject[WALL_SECTION_COUNT];
        tiles = new GameObject[columns, rows];
    }

    [ContextMenu("Generate Grid")]
    private void GenerateGrid()
    {
        ValuesInitialization();
        SetGround();
        ArrangeWalls();
        SpawnTiles();
    }
    private void SetGround()
    {
        ground = Instantiate(groundPrefab, groundParent);
        ground.name = "Ground";
        var groundScaleX = (columns * TileWidth) + ((columns + 1) * TilePadding) + (2 * WallsWidth);
        var groundScaleY = (rows * TileWidth) + ((rows + 1) * TilePadding) + (2 * WallsWidth);
        ground.transform.localScale = new Vector3(groundScaleX, 1, groundScaleY);
        ground.transform.localPosition = Vector3.zero;
    }
    private void ArrangeWalls()
    {
        for (int i = 0; i < 8; i++)
        {
            walls[i] = Instantiate(wallsPrefab, wallsParent);
            walls[i].name = $"WallSection_{i}";
        }
        float posX, posY, midScaleX, midScaleY;
        posX = ((columns * TileWidth) + ((columns + 1) * TilePadding) + WallsWidth) / 2.0f;
        posY = ((rows * TileWidth) + ((rows + 1) * TilePadding) + WallsWidth) / 2.0f;
        midScaleX = (columns * TileWidth) + ((columns + 1) * TilePadding);
        midScaleY = (rows * TileWidth) + ((rows + 1) * TilePadding);

        SetWallTransform(posX, posY, midScaleX, midScaleY);
    }
    void SetWallTransform(float posX,float posY,float midScaleX,float midScaleY)
    {
        //left mid wall
        walls[0].transform.localPosition = new Vector3(-posX, 1, 0);
        walls[0].transform.localScale = new Vector3(WallsWidth, 1, midScaleY);
        //right mid wall
        walls[1].transform.localPosition = new Vector3(posX, 1, 0);
        walls[1].transform.localScale = new Vector3(WallsWidth, 1, midScaleY);
        //top mid wall
        walls[2].transform.localPosition = new Vector3(0, 1, posY);
        walls[2].transform.localScale = new Vector3(midScaleX, 1, WallsWidth);
        //bottom mid wall
        walls[3].transform.localPosition = new Vector3(0, 1, -posY);
        walls[3].transform.localScale = new Vector3(midScaleX, 1, WallsWidth);
        //lefttop corner box
        walls[4].transform.localPosition = new Vector3(-posX, 1, posY);
        walls[4].transform.localScale = new Vector3(WallsWidth, 1, WallsWidth);
        //righttop corner box
        walls[5].transform.localPosition = new Vector3(posX, 1, posY);
        walls[5].transform.localScale = new Vector3(WallsWidth, 1, WallsWidth);
        //leftbottom corner box
        walls[6].transform.localPosition = new Vector3(-posX, 1, -posY);
        walls[6].transform.localScale = new Vector3(WallsWidth, 1, WallsWidth);
        //rightbottom corner box
        walls[7].transform.localPosition = new Vector3(posX, 1, -posY);
        walls[7].transform.localScale = new Vector3(WallsWidth, 1, WallsWidth);
    }
    private void SpawnTiles()
    {
        float posX, posY;
        if (columns % 2 == 0)
        {
            posX = (columns - 1) * (TileWidth + TilePadding) / 2;
        }
        else
        {
            posX = (((columns - 1) / 2) * TilePadding) + ((columns - 1) * TileWidth / 2);
        }
        if (rows % 2 == 0)
        {
            posY = (rows - 1) * (TileWidth + TilePadding) / 2;
        }
        else
        {
            posY = (((rows - 1) / 2) * TilePadding) + ((rows - 1) * TileWidth / 2);
        }
        tileParent.transform.localPosition = new Vector3(-posX, 0, -posY);

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                tiles[i, j] = Instantiate(tilePrefab, tileParent);
                tiles[i, j].transform.localPosition = new Vector3(i * (TilePadding + TileWidth), 0.5f, j * (TilePadding + TileWidth));
                tiles[i, j].transform.localScale = new Vector3(TileWidth, TileWidth, TileWidth);
            }
        }
    }
}
