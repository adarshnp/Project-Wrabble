using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField]
    private float _width, _height;
    [SerializeField]
    private float _coloumn, _row;
    [SerializeField]
    private Tile _tilePrefab;
    [SerializeField]
    private Tile wallsPrefab;
    [SerializeField]
    private Transform wallsParent;
    [SerializeField]
    private Transform _cam;
    private Camera cam;
    private GameObject[] walls;
    public float tileWidth;
    public float tilePadding;
    public float wallsWidth;


    private void Start()
    {
        InitializeValues();

        Debug.Log("width and height = " + _width + ";" + _height);
        ArrangeWalls();
        GenerateGrid();
        SetCamPos(cam, _width, _height);
    }

    void InitializeValues()
    {
        cam = _cam.GetComponent<Camera>();
        _width = Screen.width / Screen.dpi * 2.54f;
        _height = Screen.height / Screen.dpi * 2.54f;
        tileWidth = _width / (4.1f + (1.1f * _coloumn));//total width = wall width(2w + 2w) + total number of cells (Cw) + total padding(0.1*(C+1)w) => w(2+2+C+0.1C+0.1) => w(4.1 + 1.1C)  
        wallsWidth = 2 * tileWidth;
        tilePadding = 0.1f * tileWidth;

    }
    void GenerateGrid()
    {
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                var spawnedTile = Instantiate(_tilePrefab, new Vector3(i, 0, j), Quaternion.identity);
                spawnedTile.name = $"Tile {i} {j}";
            }
        }
    }
    //Assuming wall width is 2 times coloumn width
    private void ArrangeWalls()
    {
        for (int i = 0; i < 8; i++)
        {
            walls[i] = Instantiate(wallsPrefab.gameObject, wallsParent);
            walls[i].name = $"WallSection_{i}";
        }
        float posX, posY, midScaleX, midScaleY;
        posX = ((_coloumn * tileWidth) + ((_coloumn + 1) * tilePadding) + wallsWidth) / 2.0f;
        posY = ((_row * tileWidth) + ((_row + 1) * tilePadding) + wallsWidth) / 2.0f;
        midScaleX = (_coloumn * tileWidth) + ((_coloumn + 1) * tilePadding);
        midScaleY = (_row * tileWidth) + ((_row + 1) * tilePadding);

        SetWallTransform(posX, posY, midScaleX, midScaleY);
    }
    void SetWallTransform(float posX, float posY, float midScaleX, float midScaleY)
    {
        //left mid wall
        walls[0].transform.localPosition = new Vector3(-posX, 1, 0);
        walls[0].transform.localScale = new Vector3(wallsWidth, 1, midScaleY);
        //right mid wall
        walls[1].transform.localPosition = new Vector3(posX, 1, 0);
        walls[1].transform.localScale = new Vector3(wallsWidth, 1, midScaleY);
        //top mid wall
        walls[2].transform.localPosition = new Vector3(0, 1, posY);
        walls[2].transform.localScale = new Vector3(midScaleX, 1, wallsWidth);
        //bottom mid wall
        walls[3].transform.localPosition = new Vector3(0, 1, -posY);
        walls[3].transform.localScale = new Vector3(midScaleX, 1, wallsWidth);
        //lefttop corner box
        walls[4].transform.localPosition = new Vector3(-posX, 1, posY);
        walls[4].transform.localScale = new Vector3(wallsWidth, 1, wallsWidth);
        //righttop corner box
        walls[5].transform.localPosition = new Vector3(posX, 1, posY);
        walls[5].transform.localScale = new Vector3(wallsWidth, 1, wallsWidth);
        //leftbottom corner box
        walls[6].transform.localPosition = new Vector3(-posX, 1, -posY);
        walls[6].transform.localScale = new Vector3(wallsWidth, 1, wallsWidth);
        //rightbottom corner box
        walls[7].transform.localPosition = new Vector3(posX, 1, -posY);
        walls[7].transform.localScale = new Vector3(wallsWidth, 1, wallsWidth);
    }
    void SetCamPos(Camera cam, float planeWidth, float planeHeight)
    {
        float angle = cam.fieldOfView / 2;
        angle = angle * (Mathf.PI / 180.0f);        //converting degree to radians
        Debug.Log("angle = " + angle);
        float cameraHeight = (planeHeight / 2.0f) / Mathf.Tan(angle);
        Debug.Log("cam height = " + cameraHeight);
        float offset = 0.25f;
        _cam.transform.position = new Vector3((float)planeWidth / 2 - offset, cameraHeight + 0.75f, (float)planeHeight / 2 - offset);         //fov of cam is its vertical fov(vertical axis)
    }
}
