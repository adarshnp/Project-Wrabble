using UnityEngine;
[CreateAssetMenu(fileName = "TilePoolData", menuName = "ScriptableObjects/TilePool", order = 4)]

public class TilePoolDataScriptable : ScriptableObject
{
    public Vector2Int GridSize;
    public float TileWidth;
    public float TilePadding;
    public float WallsWidth;
}
