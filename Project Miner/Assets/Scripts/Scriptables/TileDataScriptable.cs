using UnityEngine;
[CreateAssetMenu(fileName = "BoardData", menuName = "ScriptableObjects/Tile", order = 3)]
public class TileDataScriptable : ScriptableObject
{
    public string Letter;
    public int score;
}