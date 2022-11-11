using UnityEngine;

public class TilePoolGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject BoxPrefab;
    [SerializeField]
    private GameObject TilePrefab;
    [SerializeField]
    private Transform tilesParent;
    [SerializeField]
    private Transform wallsParent;
    [SerializeField]
    private TilePoolDataScriptable tilePoolData;


    private Vector2Int GridSize;
    private float TileWidth;
    private float TilePadding;
    private float WallsWidth;

    private GameObject[] walls;
    private GameObject[,] tiles;
    private GameObject ground;


    private void Awake()
    {
        //assignment of data from scriptable object
        GridSize = tilePoolData.GridSize;
        TileWidth = tilePoolData.TileWidth;
        TilePadding = tilePoolData.TilePadding;
        WallsWidth = tilePoolData.WallsWidth;

        //initialization of arrays
        walls = new GameObject[8];
        tiles = new GameObject[GridSize.x, GridSize.y];
    }
    [ContextMenu("generate Tile Pool")]
    private void Start()
    {
        ClearPool();
        SetGround();
        SetWalls();
        SetTiles();
    }

    private void SetTiles()
    {
        float posX, posY;
        if (GridSize.x % 2 == 0)
        {
            posX = (GridSize.x - 1) * (TileWidth + TilePadding) / 2;
        }
        else
        {
            posX = (((GridSize.x - 1) / 2) * TilePadding) + ((GridSize.x - 1) * TileWidth / 2);
        }
        if (GridSize.y % 2 == 0)
        {
            posY = (GridSize.y - 1) * (TileWidth + TilePadding) / 2;
        }
        else
        {
            posY = (((GridSize.y - 1) / 2) * TilePadding) + ((GridSize.y - 1) * TileWidth / 2);
        }
        tilesParent.transform.localPosition = new Vector3(-posX, 0, -posY);

        for (int i = 0; i < GridSize.x; i++)
        {
            for (int j = 0; j < GridSize.y; j++)
            {
                tiles[i, j] = Instantiate(TilePrefab, tilesParent);
                tiles[i, j].transform.localPosition = new Vector3(i * (TilePadding + TileWidth), 1, j * (TilePadding + TileWidth));
                //cells[i, j].transform.localScale = new Vector3(cellWidth, cellWidth, cellWidth);
            }
        }
    }

    private void SetWalls()
    {
        for (int i = 0; i < 4; i++)
        {
            walls[i] = Instantiate(BoxPrefab, wallsParent);
            walls[i].name = $"MidWall_{i}";
        }
        for (int i = 4; i < 8; i++)
        {
            walls[i] = Instantiate(BoxPrefab, wallsParent);
            walls[i].name = $"CornerWall_{i}";
        }
        float posX, posY, midScaleX, midScaleY;
        posX = ((GridSize.x * TileWidth) + ((GridSize.x + 1) * TilePadding) + WallsWidth) / 2.0f ;
        posY = ((GridSize.y * TileWidth) + ((GridSize.y + 1) * TilePadding) + WallsWidth) / 2.0f ;
        midScaleX = (GridSize.x * TileWidth) + ((GridSize.x + 1) * TilePadding);
        midScaleY = (GridSize.y * TileWidth) + ((GridSize.y + 1) * TilePadding);

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

    private void SetGround()
    {
        ground = Instantiate(BoxPrefab, transform);
        ground.name = "Ground";
        var groundScaleX = (GridSize.x * TileWidth) + ((GridSize.x + 1) * TilePadding) + (2 * WallsWidth);
        var groundScaleY = (GridSize.y * TileWidth) + ((GridSize.y + 1) * TilePadding) + (2 * WallsWidth);
        ground.transform.localScale = new Vector3(groundScaleX, 1, groundScaleY);
        ground.transform.localPosition = Vector3.zero;
    }

    private void ClearPool()
    {
        foreach (Transform child in tilesParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in wallsParent)
        {
            Destroy(child.gameObject);
        }
        Destroy(ground);
        walls = new GameObject[8];
        tiles = new GameObject[GridSize.x, GridSize.y];
    }
}
